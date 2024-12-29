using UnityEngine;
using Game.GameSystem;

namespace Game.Title
{
    /// <summary>
    /// �^�C�g���V�[���̊Ǘ��N���X
    /// </summary>
    public class TitleSceneController : MonoBehaviour
    {
        // �Q�[���f�[�^�Ǘ��N���X�̌Ăяo��
        private GameDataManager _manager = GameDataManager.Instance;

        // ���͊Ǘ��N���X�̌Ăяo��
        private InputHandler _input;

        private DeviceManager _deviceManager = null;

        private SceneLoader _sceneLoader = SceneLoader.Instance;

        // ���[�h�Ǘ��t���O
        private static bool isLoadGameData = false;

        // �^�C�g���V�[���̃X�e�b�v
        private enum TitleStep
        {
            TITLE,
            GAME_MENU,
            START_MENU,
            CONFIG_MENU,
            MAX_STEP,
        }

        // ���݂̃��j���[
        private TitleStep _currentStep;

        [SerializeField]
        private GameObject[] _menuObjects = new GameObject[(int)TitleStep.MAX_STEP];

        /// <summary>
        /// ����������
        /// </summary>
        private void Awake()
        {
            // �O���f�[�^�̓ǂݍ���
            StageDataLoader.LoadStageData();

            // �Q�[���f�[�^�̐���
            _manager.NewGameData();

            // ���s���Ĉ�x�̂݃��[�h����
            if (!isLoadGameData)
            {
                // �Q�[���f�[�^�̃��[�h
                SaveSystem.LoadManager();

                isLoadGameData = true;
            }

            // ���͊Ǘ��N���X�̌Ăяo��
            _input = GameObject.Find(GameConstants.Object.INPUT).GetComponent<InputHandler>();

            _deviceManager = GameObject.Find(GameConstants.Object.DEVICE_MANAGER).GetComponent<DeviceManager>();

            _sceneLoader = SceneLoader.Instance;

            // �X�e�b�v��������
            _currentStep = TitleStep.TITLE;
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            if (_input.IsActionPressed(GameConstants.Input.Action.MENU_DECISION))
            {
                _sceneLoader.LoadScene(GameConstants.Scene.StageSelect.ToString());
            }

            switch(_currentStep)
            {
                case TitleStep.TITLE:
                    TitleUpdate();
                    break;

                case TitleStep.GAME_MENU:
                    GameMenuUpdate();
                    break;

                case TitleStep.START_MENU:
                    StartMenuUpdate(); 
                    break;

                case TitleStep.CONFIG_MENU:
                    ConfigMenuUpdate(); 
                    break;
            }
        }

        private void TitleUpdate()
        {

        }

        private void GameMenuUpdate()
        {

        }

        private void StartMenuUpdate()
        {

        }

        private void ConfigMenuUpdate()
        {

        }

        /// <summary>
        /// �Q�[���f�[�^�̍폜
        /// </summary>
        public void GameDataErase()
        {
            // �Q�[���f�[�^�̃��Z�b�g
            _manager.ReSetGameData();

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