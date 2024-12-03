using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem 
{
    /// <summary>
    /// �Q�[���f�[�^�̊Ǘ��N���X
    /// </summary>
    public class GameDataManager
    {
        #region -------- �V���O���g���̐ݒ� --------

        // �Q�[���f�[�^�Ǘ��N���X�̃V���O���g���C���X�^���X
        private static GameDataManager instance;

        /// <summary>
        /// �R���X�g���N�^
        /// �V���O���g���p�^�[�����������邽�߁A�O������̃C���X�^���X������h��
        /// </summary>
        private GameDataManager() { }

        /// <summary>
        /// �V���O���g���C���X�^���X��Ԃ�
        /// </summary>
        /// <returns>instance</returns>
        public static GameDataManager Instance
        {
            get
            {
                // �C���X�^���X�����݂��Ȃ��ꍇ
                if (instance == null)
                {
                    // �C���X�^���X�𐶐�
                    instance = new GameDataManager();
                }

                // �C���X�^���X��Ԃ�
                return instance;
            }
        }

        #endregion


        // �Q�[���f�[�^
        private GameData data;

        /// <summary>
        /// �Q�[���f�[�^�̐���
        /// </summary>
        public void NewGameData()
        {
            data = new GameData();
        }

        #region -------- �Q�[���f�[�^�̐ݒ� --------

        /// <summary>
        /// �Q�[���f�[�^��ݒ肷��
        /// </summary>
        /// <param name="newdata"></param>
        public void SetGameData(GameData newdata)
        {
            data = newdata;
        }

        /// <summary>
        /// �Q�[���f�[�^��Ԃ�
        /// </summary>
        /// <returns>GameData</returns>
        public GameData GetGameData()
        {
            return data;
        }

        /// <summary>
        /// �Q�[���f�[�^�̃��Z�b�g
        /// </summary>
        public void ReSetGameData()
        {
            if (data.ReSetData() == GameConstants.NORMAL)
            {
                DebugManager.LogMessage("�f�[�^���폜���܂���");
            }
            else
            {
                DebugManager.LogMessage("����Ƀf�[�^���폜�ł��܂���ł���", DebugManager.MessageType.Error);
            }
        }

        #endregion


        // ���݂̃X�e�[�W�C���f�b�N�X
        private int CurrentStageIndex;

        public void SetCurrentStageIndex(int stage_index)
        {
            if (stage_index >= GameConstants.STAGE_MAX_NUM) { return; }

            CurrentStageIndex = stage_index;
        }

        public int GetCurrentStageIndex()
        {
            return CurrentStageIndex;
        }
    }
}