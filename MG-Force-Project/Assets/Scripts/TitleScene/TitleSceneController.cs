using UnityEngine;
using Game.GameSystem;
using UnityEngine.UI;

namespace Game.Title
{
    /// <summary>
    /// �^�C�g���V�[���̊Ǘ��N���X
    /// </summary>
    public class TitleSceneController : MonoBehaviour
    {
        // ���͊Ǘ��N���X�̌Ăяo��
        private InputHandler _input;

        private DeviceManager _deviceManager = null;

        private SceneLoader _sceneLoader = SceneLoader.Instance;

        // ���[�h�Ǘ��t���O
        private static bool isLoadGameData = false;

        private SEManager _seManager;

        #region -------- �X�e�b�v�Ǘ��p�萔 --------

        // �^�C�g���V�[���̃X�e�b�v
        public enum TitleStep
        {
            TITLE,
            GAME_MENU,
            START_MENU,
            CONFIG_MENU,
            GAMEDATA_ERASE,
            MAX_STEP,
        }

        // �Q�[�����j���[�̃X�e�b�v
        private enum GameMenu
        {
            CONFIG,
            START,
            GAME_FINISH,
            MAX_BUTTON
        }

        // �X�^�[�g���j���[�̃X�e�b�v
        private enum StartMenu
        {
            NEW_START,
            RE_START,
            MAX_BUTTON,
        }

        // �ݒ胁�j���[�̃X�e�b�v
        private enum ConfigMenu
        {
            BGM,
            SE,
            KEY,
            HELP,
            DATA,
            BACK,
            MAX_BUTTON,
        }

        #endregion

        // �T�E���h�Ǘ��p
        private enum SoundSlider
        {
            BGM,
            SE,
            MAX_SLIDER,
        }

        private const int INIT_BUTTON = -1;

        // ���݂̃��j���[
        private TitleStep _currentStep;

        [SerializeField] private GameObject[] _menuObjects = new GameObject[(int)TitleStep.MAX_STEP];

        [SerializeField] private GameObject[] _gameMenu = new GameObject[(int)GameMenu.MAX_BUTTON];

        [SerializeField] private GameObject[] _startMenu = new GameObject[(int)StartMenu.MAX_BUTTON];

        [SerializeField] private GameObject[] _configMenu = new GameObject[(int)ConfigMenu.MAX_BUTTON];

        [SerializeField] private Slider[] _soundSlider = new Slider[(int)SoundSlider.MAX_SLIDER];

        [SerializeField] private Toggle _keyToggle;

        [SerializeField] private GameDataEraseController _eraseContrller;

        private int _currentButton = INIT_BUTTON;

        private bool _isExistGameData;

        private Vector3 _targetButton = new Vector3(1.2f, 1.2f, 1.2f);

        private Vector3 _offTargetButton = new Vector3(1.0f, 1.0f, 1.0f);

        /// <summary>
        /// ����������
        /// </summary>
        private void Awake()
        {
            // �O���f�[�^�̓ǂݍ���
            StageDataLoader.LoadStageData();

            // ���s���Ĉ�x�̂݃��[�h����
            if (!isLoadGameData)
            {
                // �Q�[���f�[�^�̃��[�h
                _isExistGameData = SaveSystem.LoadManager();

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

                case TitleStep.GAMEDATA_ERASE:

                    if (!_menuObjects[(int)TitleStep.GAMEDATA_ERASE].activeSelf)
                    {
                        _menuObjects[(int)TitleStep.GAMEDATA_ERASE].SetActive(true);
                    }
                    else
                    {
                        if (!_eraseContrller.isActive)
                        {
                            SetStep(TitleStep.CONFIG_MENU);
                        }
                    }

                    break;
            }
        }

        #region -------- �^�C�g���̃X�e�b�v���� --------

        public void TitleUpdate(bool is_push_button = false)
        {
            if (_input.IsActionPressed(InputConstants.Action.MENU_DECISION) || is_push_button)
            {
                SetStep(TitleStep.GAME_MENU);
            }
        }

        #endregion // �^�C�g���̃X�e�b�v�̏���

        #region -------- �Q�[�����j���[�̃X�e�b�v���� --------

        private void GameMenuUpdate()
        {
            if (_input.IsActionPressed(InputConstants.Action.MENU_DECISION))
            {
                GameMenuDecision(_currentButton);
            }
            else if (_input.IsActionPressed(InputConstants.Action.MENU_LEFT_SELECT))
            {
                if (_currentButton == INIT_BUTTON)
                {
                    _currentButton = (int)GameMenu.START;
                }
                else if (_currentButton != (int)GameMenu.CONFIG)
                {
                    _currentButton--;
                }
            }
            else if (_input.IsActionPressed(InputConstants.Action.MENU_RIGHT_SELECT))
            {
                if (_currentButton == INIT_BUTTON)
                {
                    _currentButton = (int)GameMenu.START;
                }
                else if (_currentButton != (int)GameMenu.GAME_FINISH)
                {
                    _currentButton++;
                }
            }
            else if (_input.IsActionPressed(InputConstants.Action.MENU_BACK))
            {
                SetStep(TitleStep.TITLE);
            }

            GameMenuButtonUpdate();
        }

        /// <summary>
        /// �{�^���̍X�V����
        /// </summary>
        private void GameMenuButtonUpdate()
        {
            for (int i = (int)GameMenu.CONFIG; i < (int)GameMenu.MAX_BUTTON; i++)
            {
                if (_currentButton == i)
                {
                    _gameMenu[i].transform.localScale = _targetButton;
                }
                else
                {
                    _gameMenu[i].transform.localScale = _offTargetButton;
                }
            }
        }

        /// <summary>
        /// ���莞�̏���
        /// </summary>
        /// <param name="button_index"></param>
        public void GameMenuDecision(int button_index)
        {
            switch (button_index)
            {
                case (int)GameMenu.CONFIG:
                    SetStep(TitleStep.CONFIG_MENU);
                    break;

                case (int)GameMenu.START:
                    SetStep(TitleStep.START_MENU);
                    break;

                case (int)GameMenu.GAME_FINISH:
                    GameFinish();
                    break;

                case INIT_BUTTON:
                    _currentButton = (int)GameMenu.START;
                    break;
            }
        }

        /// <summary>
        /// �Q�[���I��
        /// </summary>
        public void GameFinish()
        {
#if UNITY_EDITOR

            // �G�f�B�^�[�̏I��
            UnityEditor.EditorApplication.isPlaying = false;

#else // ���ۂ̃r���h�Ŏ��s��
      
            // �A�v���P�[�V�����̏I��
            Application.Quit();
#endif
        }

        #endregion // �Q�[�����j���[�̃X�e�b�v����

        #region -------- �X�^�[�g���j���[�̃X�e�b�v���� --------

        private void StartMenuUpdate()
        {
            if (_input.IsActionPressed(InputConstants.Action.MENU_DECISION))
            {
                StartMenuDecision(_currentButton);
            }
            else if (_input.IsActionPressed(InputConstants.Action.MENU_LEFT_SELECT))
            {
                if (_currentButton == INIT_BUTTON)
                {
                    _currentButton = (_isExistGameData) ? (int)StartMenu.RE_START : (int)StartMenu.NEW_START;
                }
                else if (_currentButton != (int)StartMenu.NEW_START)
                {
                    _currentButton--;
                }
            }
            else if (_input.IsActionPressed(InputConstants.Action.MENU_RIGHT_SELECT))
            {
                if (_currentButton == INIT_BUTTON)
                {
                    _currentButton = (_isExistGameData) ? (int)StartMenu.RE_START : (int)StartMenu.NEW_START;
                }
                else if (_currentButton != (int)StartMenu.RE_START)
                {
                    _currentButton++;
                }
            }
            else if (_input.IsActionPressed(InputConstants.Action.MENU_BACK))
            {
                SetStep(TitleStep.GAME_MENU);
            }

            StartMenuButtonUpdate();
        }

        /// <summary>
        /// �{�^���̍X�V����
        /// </summary>
        private void StartMenuButtonUpdate()
        {            
            for (int i = (int)StartMenu.NEW_START; i < (int)StartMenu.MAX_BUTTON; i++)
            {
                if (_currentButton == i)
                {
                    _startMenu[i].transform.localScale = _targetButton;
                }
                else
                {
                    _startMenu[i].transform.localScale = _offTargetButton;
                }
            }
        }

        /// <summary>
        /// ���莞�̏���
        /// </summary>
        /// <param name="button_index"></param>
        public void StartMenuDecision(int button_index)
        {
            switch (button_index)
            {
                case (int)StartMenu.NEW_START:
                    break;

                case (int)StartMenu.RE_START:
                    break;

                case INIT_BUTTON:

                    if (_isExistGameData)
                    {
                        _currentButton = (int)StartMenu.RE_START;
                    }
                    else
                    {
                        _currentButton = (int)StartMenu.NEW_START;
                    }

                    return;
            }

            _sceneLoader.LoadScene(GameConstants.Scene.StageSelect.ToString());
        }

        #endregion // �X�^�[�g���j���[�̃X�e�b�v����

        #region -------- �ݒ胁�j���[�̃X�e�b�v���� --------

        private void ConfigMenuUpdate()
        {
            if (_input.IsActionPressed(InputConstants.Action.MENU_DECISION))
            {
                ConfigMenuDecisioin(_currentButton);
            }
            else if (_input.IsActionPressed(InputConstants.Action.MENU_UP_SELECT))
            {
                if (_currentButton == INIT_BUTTON)
                {
                    _currentButton = (int)ConfigMenu.BGM;
                }
                else if (_currentButton != (int)ConfigMenu.BGM)
                {
                    _currentButton--;
                }
            }
            else if (_input.IsActionPressed(InputConstants.Action.MENU_DOWN_SELECT))
            {
                if (_currentButton == INIT_BUTTON)
                {
                    _currentButton = (int)ConfigMenu.BGM;
                }
                else if (_currentButton != (int)ConfigMenu.BACK)
                {
                    _currentButton++;
                }
            }
            else if (_input.IsActionPressed(InputConstants.Action.MENU_BACK))
            {
                SetStep(TitleStep.GAME_MENU);
            }

            SoundVolumeUpdate();

            ConfigMenuButtonUpdate();
        }

        private void ConfigMenuButtonUpdate()
        {
            for (int i = (int)ConfigMenu.BGM; i < (int)ConfigMenu.MAX_BUTTON; i++)
            {
                if (_currentButton == i)
                {
                    _configMenu[i].transform.localScale = _targetButton;
                }
                else
                {
                    _configMenu[i].transform.localScale = _offTargetButton;
                }
            }
        }

        public void ConfigMenuDecisioin(int button_index)
        {
            switch (button_index) 
            {
                case (int)ConfigMenu.BGM:
                    AudioSource bgm_audio = GameObject.Find(GameConstants.Object.BGM_MANAGER).GetComponent<AudioSource>();
                    bgm_audio.mute = !bgm_audio.mute;
                    break;

                case (int)ConfigMenu.SE:
                    AudioSource se_audio = GameObject.Find(GameConstants.Object.SE_MANAGER).GetComponent<AudioSource>();
                    se_audio.mute = !se_audio.mute;
                    break;

                case (int)ConfigMenu.KEY:
                    _keyToggle.isOn = !_keyToggle.isOn;
                    break;

                case (int)ConfigMenu.HELP:
                    break;

                case (int)ConfigMenu.DATA:
                    SetStep(TitleStep.GAMEDATA_ERASE);
                    break;

                case (int)ConfigMenu.BACK:
                    SetStep(TitleStep.GAME_MENU);
                    break;
            }
        }

        #region ------------ �T�E���h�ݒ� ------------

        /// <summary>
        /// ���ʍX�V����
        /// </summary>
        private void SoundVolumeUpdate()
        {
            if (_currentButton == (int)ConfigMenu.BGM || _currentButton == INIT_BUTTON)
            {
                // BGM�̉��ʕύX
                ChangeBGMVolume();
            }
            
            if (_currentButton == (int)ConfigMenu.SE || _currentButton == INIT_BUTTON)
            {
                // SE�̉��ʕύX
                ChangeSEVolume();
            }
        }


        /// <summary>
        /// BGM�̉��ʕύX
        /// </summary>
        private void ChangeBGMVolume()
        {
            BGMManager bgm_manager = GameObject.Find(GameConstants.Object.BGM_MANAGER).GetComponent<BGMManager>();

            float sound = _soundSlider[(int)SoundSlider.BGM].value;
            _soundSlider[(int)SoundSlider.BGM].value = bgm_manager.VolumeChange(sound);
        }

        /// <summary>
        /// SE�̉��ʕύX
        /// </summary>
        private void ChangeSEVolume()
        {
            SEManager se_manager = GameObject.Find(GameConstants.Object.SE_MANAGER).GetComponent<SEManager>();

            float sound = _soundSlider[(int)SoundSlider.SE].value;
            _soundSlider[(int)SoundSlider.SE].value = se_manager.VolumeChange(sound);
        }

        #endregion // �T�E���h�ݒ�

        /// <summary>
        /// GamePad�̃L�[�؂�ւ�
        /// </summary>
        public void GamePadKeyChange()
        {
            _input.GamePadKeyChange();
        }

        #endregion // �ݒ胁�j���[�̃X�e�b�v����

        /// <summary>
        /// �X�e�b�v�̐ݒ�
        /// </summary>
        /// <param name="step"></param>
        private void SetStep(TitleStep step)
        {
            _menuObjects[(int)_currentStep].SetActive(false);

            _currentStep = step;

            _currentButton = INIT_BUTTON;

            _menuObjects[(int)_currentStep].SetActive(true);
        }

        /// <summary>
        /// �X�e�b�v�̐ݒ�(Button�Ăяo���p)
        /// </summary>
        /// <param name="step"></param>
        public void SetStep(int step)
        {
            SetStep((TitleStep)step);
        }
    }
}