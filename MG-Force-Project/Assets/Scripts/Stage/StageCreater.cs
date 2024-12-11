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
        #region -------- �萔�̐錾 --------

        // �X�e�[�W�I�u�W�F�N�g�̃^�C�v
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

        // �X�e�[�W�I�u�W�F�N�g(����)�̃^�C�v
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

        // �Q�[���f�[�^�Ǘ��N���X�̕ϐ�
        private GameDataManager gameDataManager;

        // �X�e�[�W�I�u�W�F�N�g
        [SerializeField]
        private GameObject[] Objects;

        // �X�e�[�W�I�u�W�F�N�g(����)
        [SerializeField]
        private GameObject[] SpecialObjects;

        // �s��̍ő吔
        private const int maxRows = 25;
        private const int maxCols = 38;

        // �s��̃J�E���^�[
        private int row = maxRows - 1;
        private int col = 0;

        // �f�[�^�p�z��
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

        // �v���C���[�̐����t���O
        private bool isPlayerCreate = false;

        private Scale zero = new Scale(0, 0);

        #region -------- StageData�Ǘ��p�N���X --------

        // �X�e�[�W�f�[�^��Item
        [System.Serializable]
        public class Item
        {
            public int color;  // �I�u�W�F�N�g����
            public int power;  // ���͂̋���
        }

        [System.Serializable]
        public class ItemWrapper
        {
            public string key;
            public Item value;
        }

        // JSON�p
        [System.Serializable]
        public class RootObject
        {
            [JsonProperty("items")]
            public List<ItemWrapper> items;
        }

        #endregion

        /// <summary>
        /// JSON�f�[�^�̎擾
        /// </summary>
        /// <returns></returns>
        private string GetJSONData()
        {
            // �Q�[���f�[�^�Ǘ��N���X�̌Ăяo��
            gameDataManager = GameDataManager.Instance;

            // ���݂̃C���f�b�N�X�ԍ�
            int current_index = gameDataManager.GetCurrentStageIndex();

            // �X�e�[�W�f�[�^(JSON)
            string json = StageDataLoader.CellStageData(current_index);

            // null�`�F�b�N
            if (string.IsNullOrEmpty(json))
            {
                DebugManager.LogMessage("JSON�t�@�C���𐳏�Ɏ擾�ł��܂���ł���", DebugManager.MessageType.Error);
                return null;
            }

            return json;
        }

        /// <summary>
        /// �X�e�[�W�f�[�^�̎擾
        /// </summary>
        private void GetStageData()
        {
            string json = GetJSONData();

            if (json == null) return;

            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);

            // JSON�̃f�[�^�����ƂɁAcolorArray �� powerArray �ɒl��ݒ�
            foreach (var itemWrapper in rootObject.items)
            {
                // null�`�F�b�N
                if (string.IsNullOrEmpty(itemWrapper.key))
                {
                    continue;
                }

                try
                {
                    // �z��Ƀf�[�^���i�[
                    colorArray[row, col] = itemWrapper.value.color;
                    powerArray[row, col] = itemWrapper.value.power;
                    scaleArray[row, col] = new Scale(1, 1);

                    // ���̃C���f�b�N�X�ւ̈ړ�
                    col++;

                    if (col >= maxCols)
                    {
                        col = 0;
                        row--;
                    }
                }
                catch (Exception ex)
                {
                    DebugManager.LogMessage($"��O���������܂��� {itemWrapper.key}: {ex.Message}", DebugManager.MessageType.Error);
                }
            }
        }

        // ��������ۂ̈ʒu�ƃT�C�Y
        private const float INIT_X = 1.0f;
        private const float INIT_Y = 1.0f;
        private const float INIT_Z = 0.0f;

        /// <summary>
        /// �X�e�[�W����
        /// </summary>
        public void StageCreate()
        {
            // �X�e�[�W�f�[�^�̎擾
            GetStageData();

            /* -------- �e�I�u�W�F�N�g�̐��� -------- */

            // �����l
            float init_main_x = -6.5f;
            float init_main_y = -3.0f;
            float init_main_z = 0.0f;

            // ����
            GameObject main_object = Instantiate(
                SpecialObjects[(int)S_OBjectType.Main],
                new Vector3(init_main_x, init_main_y, init_main_z),
                Quaternion.identity
                );

            // �v���C���[�̐����t���O�����Z�b�g
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

        #region -------- �傫���̐ݒ� --------

        private int currentColor = 0;
        private int rowConter = 0;
        private int colConter = 0;

        /// <summary>
        /// �傫���̃`�F�b�N
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

                    // �󔒂Ɠ���̓X�L�b�v
                    if (currentColor <= 0 && currentColor != -4 && currentColor != -5)
                    {
                        DebugManager.LogMessage($"�󔒁E����u���b�N = {i},{j}, color = {colorArray[i, j]}, scale = {scaleArray[i, j].row},{scaleArray[i, j].col}");
                        continue;
                    }

                    // �傫����0�Ȃ�X�L�b�v
                    if (scaleArray[i, j].row == zero.row && scaleArray[i, j].col == zero.col)
                    {
                        DebugManager.LogMessage($"0�u���b�N = {i},{j}, color = {colorArray[i,j]}, scale = {scaleArray[i,j].row},{scaleArray[i,j].col}");
                        continue;
                    }

                    // ��ԉE��̃u���b�N�̓X�L�b�v
                    if (j == maxCols - 1 && i == maxRows - 1)
                    {
                        DebugManager.LogMessage($"�Ō�̃u���b�N = {i},{j}, color = {colorArray[i, j]}, scale = {scaleArray[i, j].row},{scaleArray[i, j].col}");
                        continue;
                    }

                    bool row_flag = CheckRowPiece(i, j);
                    bool col_flag = CheckColPiece(i, j);

                    // �E�Ə�̃u���b�N�������u���b�N�ł͂Ȃ���΃X�L�b�v
                    if (!row_flag && !col_flag)
                    {
                        DebugManager.LogMessage($"1�u���b�N = {i},{j}, color = {colorArray[i, j]}, scale = {scaleArray[i, j].row},{scaleArray[i, j].col}");
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
        /// �c�͈̔̓`�F�b�N
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
        /// ���͈̔̓`�F�b�N
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
        /// �͈͂̃`�F�b�N
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
        /// �傫���̐ݒ�
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
        /// �I�u�W�F�N�g����
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
        /// ���͂̃Z�b�g
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
        /// �v���C���[�������ł��邩��Ԃ�
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