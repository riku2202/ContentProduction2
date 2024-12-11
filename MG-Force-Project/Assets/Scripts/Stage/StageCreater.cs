using Newtonsoft.Json;
using UnityEngine;
using Game.Stage.Magnet;
using System;
using System.Collections.Generic;

using Game.GameSystem;
using System.Drawing;
using Unity.VisualScripting;
using static UnityEngine.Rendering.DebugUI.Table;

namespace Game.Stage
{
    public class StageCreater : MonoBehaviour
    {
        #region -------- 定数の宣言 --------

        // ステージオブジェクトのタイプ
        private enum ObjectType
        {
            NotObject,
            NotFixed,
            NFixed,
            SFixed,
            CanFixed,
            NotMoving_1,
            NotMoving_2,
            NotMoving_3,
            CanMoving,
            NMoving,
            SMoving,
        }

        // ステージオブジェクト(特殊)のタイプ
        private enum S_OBjectType
        {
            Main,
            Player = -1,
            Goal = -2,
            Gimmick = -3,
            Moving_Floor = -4,
            CanUp = -5,
        }

        #endregion

        // ゲームデータ管理クラスの変数
        private GameDataManager gameDataManager;

        // ステージオブジェクト
        [SerializeField]
        private GameObject[] Objects;

        // ステージオブジェクト(特殊)
        [SerializeField]
        private GameObject[] SpecialObjects;

        // 行列の最大数
        private const int maxRows = 25;
        private const int maxCols = 38;

        // 行列のカウンター
        private int row = maxRows - 1;
        private int col = 0;

        // データ用配列
        private int[,] colorArray = new int[maxRows, maxCols];
        private int[,] powerArray = new int[maxRows, maxCols];

        public struct Scale
        {
            public int row;
            public int col;

            public Scale(int _row, int _col)
            {
                row = _row;
                col = _col;
            }
        }

        private Scale[,] scaleArray = new Scale[maxRows, maxCols];

        // プレイヤーの生成フラグ
        private bool isPlayerCreate = false;

        private Scale zero = new Scale(0, 0);

        #region -------- StageData管理用クラス --------

        // ステージデータのItem
        [System.Serializable]
        public class Item
        {
            public int color;  // オブジェクト判別
            public int power;  // 磁力の強さ
        }

        [System.Serializable]
        public class ItemWrapper
        {
            public string key;
            public Item value;
        }

        // JSON用
        [System.Serializable]
        public class RootObject
        {
            [JsonProperty("items")]
            public List<ItemWrapper> items;
        }

        #endregion

        /// <summary>
        /// JSONデータの取得
        /// </summary>
        /// <returns></returns>
        private string GetJSONData()
        {
            // ゲームデータ管理クラスの呼び出し
            gameDataManager = GameDataManager.Instance;

            // 現在のインデックス番号
            int current_index = gameDataManager.GetCurrentStageIndex();

            // ステージデータ(JSON)
            string json = StageDataLoader.CellStageData(current_index);

            // nullチェック
            if (string.IsNullOrEmpty(json))
            {
                DebugManager.LogMessage("JSONファイルを正常に取得できませんでした", DebugManager.MessageType.Error);
                return null;
            }

            return json;
        }

        /// <summary>
        /// ステージデータの取得
        /// </summary>
        private void GetStageData()
        {
            string json = GetJSONData();

            if (json == null) return;

            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);

            // JSONのデータをもとに、colorArray と powerArray に値を設定
            foreach (var itemWrapper in rootObject.items)
            {
                // nullチェック
                if (string.IsNullOrEmpty(itemWrapper.key))
                {
                    continue;
                }

                try
                {
                    // 配列にデータを格納
                    colorArray[row, col] = itemWrapper.value.color;
                    powerArray[row, col] = itemWrapper.value.power;
                    scaleArray[row, col] = new Scale(1, 1);

                    // 次のインデックスへの移動
                    col++;

                    if (col >= maxCols)
                    {
                        col = 0;
                        row--;
                    }
                }
                catch (Exception ex)
                {
                    DebugManager.LogMessage($"例外が発生しました {itemWrapper.key}: {ex.Message}", DebugManager.MessageType.Error);
                }
            }
        }

        // 生成する際の位置とサイズ
        private const float INIT_X = 1.0f;
        private const float INIT_Y = 1.0f;
        private const float INIT_Z = 0.0f;

        /// <summary>
        /// ステージ生成
        /// </summary>
        public void StageCreate()
        {
            // ステージデータの取得
            GetStageData();

            /* -------- 親オブジェクトの生成 -------- */

            // 初期値
            float init_main_x = -6.5f;
            float init_main_y = -3.0f;
            float init_main_z = 0.0f;

            // 生成
            GameObject main_object = Instantiate(
                SpecialObjects[(int)S_OBjectType.Main],
                new Vector3(init_main_x, init_main_y, init_main_z),
                Quaternion.identity
                );

            // プレイヤーの生成フラグをリセット
            isPlayerCreate = true;

            CheckObjectScale();

            for (int i = 0; i < maxRows; i++)
            {
                for (int j = 0; j < maxCols; j++)
                {
                    if (scaleArray[i, j].col == zero.col && scaleArray[i, j].row == zero.row) continue;

                    GameObject obj = ObjectCreater(colorArray[i, j], powerArray[i, j]);

                    if (obj != null)
                    {
                        obj.transform.localScale = new Vector3(scaleArray[i, j].col, scaleArray[i, j].row, 1.0f);
                        obj.transform.position = new Vector3(
                            INIT_X * j + ((obj.transform.localScale.x - 1) * 0.5f),
                            INIT_Y * i + ((obj.transform.localScale.y - 1) * 0.5f),
                            INIT_Z
                            );

                        obj.transform.SetParent(main_object.transform, false);
                    }
                }
            }
        }

        #region -------- 大きさの設定 --------

        private int currentColor = 0;
        private int rowConter = 0;
        private int colConter = 0;

        /// <summary>
        /// 大きさのチェック
        /// </summary>
        private void CheckObjectScale()
        {
            for (int i = 0; i < maxRows; i++)
            {
                for (int j = 0; j < maxCols; j++)
                {
                    currentColor = colorArray[i, j];

                    rowConter = 0;
                    colConter = 0;

                    // 空白と特殊はスキップ
                    if (currentColor <= 0 && currentColor != -4 && currentColor != -5)
                    {
                        DebugManager.LogMessage($"空白・特殊ブロック = {i},{j}, color = {colorArray[i, j]}, scale = {scaleArray[i, j].row},{scaleArray[i, j].col}");
                        continue;
                    }

                    // 大きさが0ならスキップ
                    if (scaleArray[i, j].row == zero.row && scaleArray[i, j].col == zero.col)
                    {
                        DebugManager.LogMessage($"0ブロック = {i},{j}, color = {colorArray[i,j]}, scale = {scaleArray[i,j].row},{scaleArray[i,j].col}");
                        continue;
                    }

                    // 一番右上のブロックはスキップ
                    if (j == maxCols - 1 && i == maxRows - 1)
                    {
                        DebugManager.LogMessage($"最後のブロック = {i},{j}, color = {colorArray[i, j]}, scale = {scaleArray[i, j].row},{scaleArray[i, j].col}");
                        continue;
                    }

                    bool row_flag = CheckRowPiece(i, j);
                    bool col_flag = CheckColPiece(i, j);

                    // 右と上のブロックが同じブロックではなければスキップ
                    if (!row_flag && !col_flag)
                    {
                        DebugManager.LogMessage($"1ブロック = {i},{j}, color = {colorArray[i, j]}, scale = {scaleArray[i, j].row},{scaleArray[i, j].col}");
                        continue;
                    }

                    while (true)
                    {
                        if (CheckScale(i, j))
                        {
                            SetObjectScale(i, j);
                            break;
                        }
                        else
                        {
                            rowConter--;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 縦の範囲チェック
        /// </summary>
        /// <param name="_row"></param>
        /// <param name="_col"></param>
        /// <returns></returns>
        private bool CheckRowPiece(int _row, int _col)
        {
            for (int i = _row; i < maxRows; i++)
            {
                if (currentColor != colorArray[i, _col]) break;

                rowConter++;
            }

            return rowConter != 1;
        }

        /// <summary>
        /// 横の範囲チェック
        /// </summary>
        /// <param name="_row"></param>
        /// <param name="_col"></param>
        /// <returns></returns>
        private bool CheckColPiece(int _row, int _col)
        {
            for (int i = _col; i < maxCols; i++)
            {
                if (currentColor != colorArray[_row, i]) break;

                colConter++;
            }

            return colConter != 1;
        }

        /// <summary>
        /// 範囲のチェック
        /// </summary>
        /// <param name="_row"></param>
        /// <param name="_col"></param>
        /// <returns></returns>
        private bool CheckScale(int _row, int _col)
        {
            for (int i = _row; i < _row + rowConter; i++)
            {
                for (int j = _col; j < _col + colConter; j++)
                {
                    if (currentColor != colorArray[i, j]) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 大きさの設定
        /// </summary>
        /// <param name="_row"></param>
        /// <param name="_col"></param>
        private void SetObjectScale(int _row, int _col)
        {
            for (int i = _row; i < _row + rowConter; i++)
            {
                for (int j = _col; j < _col + colConter; j++)
                {
                    scaleArray[i, j] = new Scale(0, 0);
                }
            }

            scaleArray[_row, _col] = new Scale(rowConter, colConter);
        }

        #endregion

        /// <summary>
        /// オブジェクト生成
        /// </summary>
        /// <param name="color"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        private GameObject ObjectCreater(int color, int power)
        {
            if (color == (int)ObjectType.NotObject) return null;

            if (color < (int)S_OBjectType.CanUp || color > (int)ObjectType.SMoving) return null;

            switch (color)
            {
                case (int)ObjectType.NFixed:

                    GameObject n_fixed = Instantiate(Objects[color - 1]);
                    PowerSet(n_fixed, power);
                    return n_fixed;

                case (int)ObjectType.SFixed:

                    GameObject s_fixed = Instantiate(Objects[color - 1]);
                    PowerSet(s_fixed, power);
                    return s_fixed;

                case (int)S_OBjectType.Player:

                    if (CanPlayerCreate())
                    {
                        int player_value = (int)S_OBjectType.Player * (int)GameConstants.INVERSION;
                        GameObject player = Instantiate(SpecialObjects[player_value]);
                        PowerSet(player, power);
                        return player;
                    }

                    return null;

                case (int)S_OBjectType.Goal:

                    int goal_value = (int)S_OBjectType.Goal * (int)GameConstants.INVERSION;
                    GameObject goal = Instantiate(SpecialObjects[goal_value]);
                    return goal;

                case (int)S_OBjectType.Gimmick:

                    int gimmick_value = (int)S_OBjectType.Goal * (int)GameConstants.INVERSION;
                    GameObject gimmick = Instantiate(SpecialObjects[gimmick_value]);
                    return gimmick;

                case (int)S_OBjectType.Moving_Floor:

                    int moving_floor_value = (int)S_OBjectType.Moving_Floor * (int)GameConstants.INVERSION;
                    GameObject moving_floor = Instantiate(SpecialObjects[moving_floor_value]);
                    return moving_floor;

                case (int)S_OBjectType.CanUp:

                    int canup_value = (int)S_OBjectType.CanUp * (int)GameConstants.INVERSION;
                    GameObject canup = Instantiate(SpecialObjects[canup_value]);
                    return canup;

                case (int)ObjectType.NotObject:

                    return null;

                default:
                    
                    GameObject obj = Instantiate(Objects[color - 1]);
                    return obj;
            }
        }

        /// <summary>
        /// 磁力のセット
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="power"></param>
        private void PowerSet(GameObject obj, int power)
        {
            MagnetObjectManager magnet = obj.GetComponent<MagnetObjectManager>();

            if (magnet == null) return;

            magnet.SetObjectPower(power);
        }

        /// <summary>
        /// プレイヤーが生成できるかを返す
        /// </summary>
        /// <returns></returns>
        private bool CanPlayerCreate()
        {
            if (isPlayerCreate)
            {
                isPlayerCreate = false;
                return true;
            }

            return false;
        }
    }
}