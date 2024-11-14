using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System 
{
    /// <summary>
    /// �Q�[���f�[�^�̊Ǘ��N���X
    /// </summary>
    public class GameDataManager
    {
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

        // �Q�[���f�[�^
        private GameData data;

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
    }
}