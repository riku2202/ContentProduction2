using Newtonsoft.Json;
using UnityEngine;
using System;
using System.Collections.Generic;
using Game.StageScene.Magnet;
using Game.GameSystem;

namespace Game.StageScene
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
        private enum S_ObjectType
        {
            Main,
            Player = -1,
            Goal = -2,
            Gimmick = -3,
            P_Gimmick = -4,
            Moving_Floor = -11,
            CanUp = -12,
        }

        #endregion

        // �Q�[���f�[�^�Ǘ��N���X�̕ϐ�
        private GameDataManager gameDataManager = GameDataManager.Instance;

        // �X�e�[�W�I�u�W�F�N�g
        [NamedSerializeField(
            new string[]
            {
                "NotFixed",
                "NFixed",
                "SFixed",
                "CanFixed",
                "NotMoving_1",
                "NotMoving_2",
                "NotMoving_3",
                "CanMoving",
                "NMoving",
                "SMoving",
            }
        )]
        [SerializeField]
        private GameObject[] Objects;

        // �X�e�[�W�I�u�W�F�N�g(����)
        [SerializeField] private GameObject[] _specialObjects;

        // �w�i�I�u�W�F�N�g
        [SerializeField] private GameObject[] _bgObjects;

        // �s��̍ő吔
        private const int MAX_ROWS = 25;
        private const int MAX_COLS = 38;

        // �s��̃J�E���^�[
        private int _row = MAX_ROWS - 1;
        private int _col = 0;

        // �f�[�^�p�z��
        private int[,] colorArray = new int[MAX_ROWS, MAX_COLS];
        private int[,] powerArray = new int[MAX_ROWS, MAX_COLS];

        public struct Scale
        {
            public int _row;
            public int _col;

            public Scale(int row, int col)
            {
                _row = row;
                _col = col;
            }
        }

        private Scale[,] scaleArray = new Scale[MAX_ROWS, MAX_COLS];

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
                    colorArray[_row, _col] = itemWrapper.value.color;
                    powerArray[_row, _col] = itemWrapper.value.power;
                    scaleArray[_row, _col] = new Scale(1, 1);

                    // ���̃C���f�b�N�X�ւ̈ړ�
                    _col++;

                    if (_col >= MAX_COLS)
                    {
                        _col = 0;
                        _row--;
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

            Vector3 init_pos = GameConstants.LowerLeft;

            // ����
            GameObject main_object = Instantiate(
                _specialObjects[(int)S_ObjectType.Main],
                init_pos,
                Quaternion.identity
                );

            // �v���C���[�̐����t���O�����Z�b�g
            isPlayerCreate = true;

            //CheckObjectScale();

            // #yu-ki-rohi�ǉ���
            Transform[,] transforms = new Transform[MAX_ROWS, MAX_COLS];

            for (int i = 0; i < MAX_ROWS; i++)
            {
                for (int j = 0; j < MAX_COLS; j++)
                {
                    if (scaleArray[i, j]._col == zero._col && scaleArray[i, j]._row == zero._row) continue;

                    GameObject obj = ObjectCreater(colorArray[i, j], powerArray[i, j]);

                    if (obj != null)
                    {
                        obj.transform.localScale = new Vector3(scaleArray[i, j]._col, scaleArray[i, j]._row, 1.0f);
                        obj.transform.position = new Vector3(
                            INIT_X * j + ((obj.transform.localScale.x - 1) * 0.5f),
                            INIT_Y * i + ((obj.transform.localScale.y - 1) * 0.5f),
                            INIT_Z
                            );
                        // @yu-ki-rohi
                        // Player��MainStage�̉��ɓ���Ȃ������AEditor��Ŋm�F���₷���Ǝv���B
                        obj.transform.SetParent(main_object.transform, false);

                        // #yu-ki-rohi�ǉ���
                        transforms[i, j] = obj.transform;
                    }

                    // @yu-ki-rohi
                    // �r���I�֌W�̏����Ȃ̂ŁAelse if �ł����������B
                    // Goal����null�`�F�b�N���s���Ă��Ȃ��̂́A�������R������̂��ȁB
                    // ����null�`�F�b�N��������A��Ɋm�F����
                    // null�Ȃ��continue����A�Ƃ����������h���悳�����B

                    if (colorArray[i, j] == (int)S_ObjectType.Goal)
                    {
                        Vector3 obj_pos = obj.transform.position;
                        obj_pos.y += 0.5f;
                        obj.transform.position = obj_pos;
                    }

                    if ((colorArray[i, j] == (int)S_ObjectType.Player) && obj != null)
                    {
                        obj.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                        obj.transform.localScale = new Vector3(20.0f, 20.0f, 20.0f);
                    }

                    if ((colorArray[i, j] == (int)S_ObjectType.Gimmick) && obj != null)
                    {
                        obj.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
                    }
                }
            }

            // #yu-ki-rohi�ǉ���
            GroupingBlocks(ref transforms, main_object.transform);
        }

        #region -------- �u���b�N�̑傫���̐ݒ� --------

        private int currentColor = 0;
        private int rowConter = 0;
        private int colConter = 0;

        /// <summary>
        /// �傫���̃`�F�b�N
        /// </summary>
        private void CheckObjectScale()
        {
            for (int i = 0; i < MAX_ROWS; i++)
            {
                for (int j = 0; j < MAX_COLS; j++)
                {
                    currentColor = colorArray[i, j];

                    rowConter = 0;
                    colConter = 0;

                    // �󔒂Ɠ���I�u�W�F�N�g�̓X�L�b�v
                    if (currentColor == (int)ObjectType.NotObject || currentColor <= (int)S_ObjectType.Main) continue;

                    // �I�u�W�F�N�g�̑傫����0�Ȃ�X�L�b�v
                    if (scaleArray[i, j]._row == zero._row && scaleArray[i, j]._col == zero._col) continue;

                    // ��ԉE��̃u���b�N�̓X�L�b�v
                    if (j == MAX_COLS - 1 && i == MAX_ROWS - 1) continue;

                    // �c���̈�v���̃`�F�b�N
                    bool row_flag = CheckRowPiece(i, j);
                    bool col_flag = CheckColPiece(i, j);

                    // �E�Ə�̃u���b�N�������u���b�N�ł͂Ȃ���΃X�L�b�v
                    if (!row_flag && !col_flag) continue;

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
            for (int i = _row; i < MAX_ROWS; i++)
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
            for (int i = _col; i < MAX_COLS; i++)
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

            if (color <= -4 || color > (int)ObjectType.SMoving) return null;

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

                case (int)S_ObjectType.Player:

                    if (CanPlayerCreate())
                    {
                        int player_value = (int)S_ObjectType.Player * (int)GameConstants.INVERSION;
                        GameObject player = Instantiate(_specialObjects[player_value]);
                        PowerSet(player, power);
                        return player;
                    }

                    return null;

                case (int)S_ObjectType.Goal:

                    int goal_value = (int)S_ObjectType.Goal * (int)GameConstants.INVERSION;
                    GameObject goal = Instantiate(_specialObjects[goal_value]);
                    return goal;

                case (int)S_ObjectType.Gimmick:

                    int gimmick_value = (int)S_ObjectType.Gimmick * (int)GameConstants.INVERSION;
                    GameObject gimmick = Instantiate(_specialObjects[gimmick_value]);
                    return gimmick;

                case (int)S_ObjectType.Moving_Floor:

                    int moving_floor_value = (int)S_ObjectType.Moving_Floor * (int)GameConstants.INVERSION;
                    GameObject moving_floor = Instantiate(_specialObjects[moving_floor_value]);
                    return moving_floor;

                case (int)S_ObjectType.CanUp:

                    int canup_value = (int)S_ObjectType.CanUp * (int)GameConstants.INVERSION;
                    GameObject canup = Instantiate(_specialObjects[canup_value]);
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


        // @yu-ki-rohi
        // ���J���\�b�h������J�̌�ɗ��Ă�͈̂�a�����邩��
        public void BGCreate()
        {
            int current_index = gameDataManager.GetCurrentStageIndex();

            Transform transform = GameObject.Find(GameConstants.MAIN_CAMERA).transform;

            Vector3 bg_position = new Vector3(transform.position.x, transform.position.y, 1.0f);

            Instantiate(_bgObjects[current_index], bg_position, Quaternion.identity, transform);
        }

        // @yu-kirohi
        // �u���b�N�̂܂Ƃ܂�̍����ɂ��Ă̒��
        #region -------- �u���b�N�̃O���[�v����� --------
        private void GroupingBlocks(ref Transform[,] transforms, Transform main_object)
        {
            for (int i = 0; i < MAX_ROWS; i++)
            {
                for (int j = 0; j < MAX_COLS; j++)
                {
                    if (transforms[i, j] != null)
                    {
                        GameObject parent_object = Instantiate(_specialObjects[(int)S_ObjectType.Main], main_object);
                        transforms[i, j].SetParent(parent_object.transform, false);
                        transforms[i, j] = null;

                        int num = AddRightBlockToGroup(ref transforms, parent_object.transform, i, j + 1) - j + 1;

                        AddUpBlocksToGroup(ref transforms, parent_object.transform, i + 1, j, num);
                    }
                }
            }
        }

        private int AddRightBlockToGroup(ref Transform[,] transforms, Transform parent_transform, int row, int col)
        {

            if (col >= MAX_COLS || transforms[row, col] == null)
            {
                return col - 1;
            }

            if (colorArray[row, col] == colorArray[row, col - 1])
            {
                transforms[row, col].SetParent(parent_transform, false);
                transforms[row, col] = null;
                if (col + 1 < MAX_COLS)
                {
                    return AddRightBlockToGroup(ref transforms, parent_transform, row, col + 1);
                }
                return col;
            }
            return col - 1;
        }

        private void AddUpBlocksToGroup(ref Transform[,] transforms, Transform parent_transform, int row, int col, int num)
        {
            if (row >= MAX_ROWS)
            {
                return;
            }

            for (int i = 0; i < num; i++)
            {
                if (colorArray[row, col + i] != colorArray[row - 1, col + i])
                {
                    return;
                }
            }
            for (int i = 0; i < num; i++)
            {
                if (transforms[row, col + i] == null)
                {
                    continue;
                }
                transforms[row, col + i].SetParent(parent_transform, false);
                transforms[row, col + i] = null;
            }


            AddUpBlocksToGroup(ref transforms, parent_transform, row + 1, col, num);
        }
        #endregion
    }
}