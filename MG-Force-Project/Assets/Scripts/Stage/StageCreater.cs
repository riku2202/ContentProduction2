using Newtonsoft.Json;
using UnityEngine;
using Game.Stage.Magnet;
using System;
using System.Collections.Generic;

using Game.GameSystem;
using System.Drawing;

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
        }

        // ステージオブジェクト(特殊)のタイプ
        private enum S_OBjectType
        {
            Main,
            Player = -1,
            Goal = -2,
            Gimmick = -3,
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
        const int maxRows = 25;
        const int maxCols = 38;

        // 行列数
        int Row = -1;
        int Col = -1;

        // 行列のカウンター
        int RowConter = maxRows - 1;
        int ColConter = 0;

        // データ用配列
        int[,] colorArray = new int[maxRows, maxCols];
        int[,] powerArray = new int[maxRows, maxCols];

        // プレイヤーの生成フラグ
        private bool IsPlayerCreate = false;

        #region -------- StageData管理用クラス --------

        // ステージデータのItem
        [System.Serializable]
        public class Item
        {
            public int color;  // オブジェクト判別
            public int power;  // 磁力の強さ
        }

        // アイテムラッパー
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
                    // 現在のインデックスを基に行と列を計算
                    Row = RowConter;
                    Col = ColConter;

                    // 配列にデータを格納
                    colorArray[Row, Col] = itemWrapper.value.color;
                    powerArray[Row, Col] = itemWrapper.value.power;

                    // 次のインデックスへの移動
                    ColConter++;

                    if (ColConter >= maxCols)
                    {
                        ColConter = 0;
                        RowConter--;
                    }
                }
                catch (Exception ex)
                {
                    DebugManager.LogMessage($"例外が発生しました {itemWrapper.key}: {ex.Message}", DebugManager.MessageType.Error);
                }
            }
        }

        // 生成する際の位置とサイズ
        const float INIT_X = 1.0f;
        const float INIT_Y = 1.0f;
        const float INIT_Z = 0.0f;
        const float INIT_REDUCE = 1.0f;

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
            IsPlayerCreate = true;

            for (int i = 0; i <= maxRows - 1; i++)
            {
                for (int j = 0; j <= maxCols - 1; j++)
                {
                    GameObject obj = ObjectCreater(colorArray[i, j], powerArray[i, j]);

                    if (obj != null)
                    {
                        obj.transform.position = new Vector3(
                            INIT_X * j - INIT_REDUCE,
                            INIT_Y * i - INIT_REDUCE,
                            INIT_Z
                            ); ;

                        obj.transform.SetParent(main_object.transform, false);
                    }
                }
            }
        }

        const int ONE_BEROW = -1;

        /// <summary>
        /// オブジェクト生成
        /// </summary>
        /// <param name="color"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        private GameObject ObjectCreater(int color, int power)
        {
            if (color == (int)ObjectType.NotObject) return null;

            if (color < (int)S_OBjectType.Gimmick || color > (int)ObjectType.CanMoving) return null;

            switch (color)
            {
                case (int)ObjectType.NFixed:

                    GameObject n_fixed = Instantiate(Objects[color + ONE_BEROW]);
                    PowerSet(n_fixed, power);
                    return n_fixed;

                case (int)ObjectType.SFixed:

                    GameObject s_fixed = Instantiate(Objects[color + ONE_BEROW]);
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

                case (int)ObjectType.NotObject:

                    return null;

                default:

                    // 作業途中のため
                    if (color >= 8) return null;
                    
                    GameObject obj = Instantiate(Objects[color + ONE_BEROW]);
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
            if (IsPlayerCreate)
            {
                IsPlayerCreate = false;
                return true;
            }

            return false;
        }
    }
}