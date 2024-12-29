using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Game.GameSystem;

namespace Game.Title
{
    /// <summary>
    /// �^�C�g���V�[���̊Ǘ��N���X
    /// </summary>
    public class TitleSceneController : MonoBehaviour
    {
        // �Q�[���f�[�^�Ǘ��N���X�̌Ăяo��
        private GameDataManager manager = GameDataManager.Instance;

        // ���͊Ǘ��N���X�̌Ăяo��
        private InputHandler input;

        // ���[�h�Ǘ��t���O
        private static bool isLoadGameData = false;

        /// <summary>
        /// ����������
        /// </summary>
        private void Awake()
        {
            // �O���f�[�^�̓ǂݍ���
            StageDataLoader.LoadStageData();

            // �Q�[���f�[�^�̐���
            manager.NewGameData();

            // ���s���Ĉ�x�̂݃��[�h����
            if (!isLoadGameData)
            {
                // �Q�[���f�[�^�̃��[�h
                SaveSystem.LoadManager();

                isLoadGameData = true;
            }

            input = GameObject.Find(GameConstants.Object.INPUT_OBJ).GetComponent<InputHandler>();
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            if (input.IsActionPressed(GameConstants.Input.Action.MENU_DECISION))
            {
                SceneManager.LoadScene(GameConstants.Scene.StageSelect.ToString());
            }
        }

        /// <summary>
        /// �Q�[���f�[�^�̍폜
        /// </summary>
        public void GameDataErase()
        {
            // �Q�[���f�[�^�̃��Z�b�g
            manager.ReSetGameData();

#if UNITY_EDITOR // UnityEditor�ł̎��s��(�f�o�b�N�p)

            // �Q�[���f�[�^�̃Z�[�u(�f�o�b�N���̓f�[�^���㏑������)
            SaveSystem.SaveManager();

#else // ���ۂ̃r���h�Ŏ��s��

            // �Q�[���f�[�^�̍폜
            SaveSystem.DeleteGameData();

#endif
        }
    }
}