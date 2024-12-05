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
        }

        // �X�e�[�W�I�u�W�F�N�g(����)�̃^�C�v
        private enum S_OBjectType
        {
            Main,
            Player = -1,
            Goal = -2,
            Gimmick = -3,
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
        const int maxRows = 25;
        const int maxCols = 38;

        // �s��
        int Row = -1;
        int Col = -1;

        // �s��̃J�E���^�[
        int RowConter = maxRows - 1;
        int ColConter = 0;

        // �f�[�^�p�z��
        int[,] colorArray = new int[maxRows, maxCols];
        int[,] powerArray = new int[maxRows, maxCols];

        // �v���C���[�̐����t���O
        private bool IsPlayerCreate = false;

        #region -------- StageData�Ǘ��p�N���X --------

        // �X�e�[�W�f�[�^��Item
        [System.Serializable]
        public class Item
        {
            public int color;  // �I�u�W�F�N�g����
            public int power;  // ���͂̋���
        }

        // �A�C�e�����b�p�[
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
                    // ���݂̃C���f�b�N�X����ɍs�Ɨ���v�Z
                    Row = RowConter;
                    Col = ColConter;

                    // �z��Ƀf�[�^���i�[
                    colorArray[Row, Col] = itemWrapper.value.color;
                    powerArray[Row, Col] = itemWrapper.value.power;

                    // ���̃C���f�b�N�X�ւ̈ړ�
                    ColConter++;

                    if (ColConter >= maxCols)
                    {
                        ColConter = 0;
                        RowConter--;
                    }
                }
                catch (Exception ex)
                {
                    DebugManager.LogMessage($"��O���������܂��� {itemWrapper.key}: {ex.Message}", DebugManager.MessageType.Error);
                }
            }
        }

        // ��������ۂ̈ʒu�ƃT�C�Y
        const float INIT_X = 1.0f;
        const float INIT_Y = 1.0f;
        const float INIT_Z = 0.0f;
        const float INIT_REDUCE = 1.0f;

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
        /// �I�u�W�F�N�g����
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

                    // ��Ɠr���̂���
                    if (color >= 8) return null;
                    
                    GameObject obj = Instantiate(Objects[color + ONE_BEROW]);
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
            if (IsPlayerCreate)
            {
                IsPlayerCreate = false;
                return true;
            }

            return false;
        }
    }
}