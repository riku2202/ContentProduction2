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
        GameDataManager manager = GameDataManager.Instance;

        // ���͊Ǘ��N���X�̌Ăяo��
        InputManager input;

        /// <summary>
        /// ����������
        /// </summary>
        private void Awake()
        {
            // �Q�[���f�[�^�̐���
            manager.NewGameData();
            SaveSystem.NewGameData();

            // �Q�[���f�[�^�̃��[�h
            SaveSystem.LoadManager();

            input = GameObject.Find(GameConstants.INPUT_MANAGER_OBJ).GetComponent<InputManager>();
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            if (input.IsActionPressed(GameConstants.INPUT_SELECT) || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(GameConstants.Scene.StageSelect.ToString());
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Title");
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