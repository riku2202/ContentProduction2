using UnityEngine;
using Game.GameSystem;
using UnityEngine.UI;

namespace Game.Title
{
    /// <summary>
    /// タイトルシーンの管理クラス
    /// </summary>
    public class TitleSceneController : MonoBehaviour
    {
        // 入力管理クラスの呼び出し
        private InputHandler _input;

        private DeviceManager _deviceManager = null;

        private SceneLoader _sceneLoader = SceneLoader.Instance;

        // ロード管理フラグ
        private static bool isLoadGameData = false;

        private SEManager _seManager;

        #region -------- ステップ管理用定数 --------

        // タイトルシーンのステップ
        public enum TitleStep
        {
            TITLE,
            GAME_MENU,
            START_MENU,
            CONFIG_MENU,
            GAMEDATA_ERASE,
            MAX_STEP,
        }

        // ゲームメニューのステップ
        private enum GameMenu
        {
            CONFIG,
            START,
            GAME_FINISH,
            MAX_BUTTON
        }

        // スタートメニューのステップ
        private enum StartMenu
        {
            NEW_START,
            RE_START,
            MAX_BUTTON,
        }

        // 設定メニューのステップ
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

        // サウンド管理用
        private enum SoundSlider
        {
            BGM,
            SE,
            MAX_SLIDER,
        }

        private const int INIT_BUTTON = -1;

        // 現在のメニュー
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
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            // 外部データの読み込み
            StageDataLoader.LoadStageData();

            // 実行して一度のみロードする
            if (!isLoadGameData)
            {
                // ゲームデータのロード
                _isExistGameData = SaveSystem.LoadManager();

                isLoadGameData = true;
            }

            // 入力管理クラスの呼び出し
            _input = GameObject.Find(GameConstants.Object.INPUT).GetComponent<InputHandler>();

            _deviceManager = GameObject.Find(GameConstants.Object.DEVICE_MANAGER).GetComponent<DeviceManager>();

            _sceneLoader = SceneLoader.Instance;

            // ステップを初期化
            _currentStep = TitleStep.TITLE;
        }

        /// <summary>
        /// 更新処理
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

        #region -------- タイトルのステップ処理 --------

        public void TitleUpdate(bool is_push_button = false)
        {
            if (_input.IsActionPressed(InputConstants.Action.MENU_DECISION) || is_push_button)
            {
                SetStep(TitleStep.GAME_MENU);
            }
        }

        #endregion // タイトルのステップの処理

        #region -------- ゲームメニューのステップ処理 --------

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
        /// ボタンの更新処理
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
        /// 決定時の処理
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
        /// ゲーム終了
        /// </summary>
        public void GameFinish()
        {
#if UNITY_EDITOR

            // エディターの終了
            UnityEditor.EditorApplication.isPlaying = false;

#else // 実際のビルド版実行時
      
            // アプリケーションの終了
            Application.Quit();
#endif
        }

        #endregion // ゲームメニューのステップ処理

        #region -------- スタートメニューのステップ処理 --------

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
        /// ボタンの更新処理
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
        /// 決定時の処理
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

        #endregion // スタートメニューのステップ処理

        #region -------- 設定メニューのステップ処理 --------

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

        #region ------------ サウンド設定 ------------

        /// <summary>
        /// 音量更新処理
        /// </summary>
        private void SoundVolumeUpdate()
        {
            if (_currentButton == (int)ConfigMenu.BGM || _currentButton == INIT_BUTTON)
            {
                // BGMの音量変更
                ChangeBGMVolume();
            }
            
            if (_currentButton == (int)ConfigMenu.SE || _currentButton == INIT_BUTTON)
            {
                // SEの音量変更
                ChangeSEVolume();
            }
        }


        /// <summary>
        /// BGMの音量変更
        /// </summary>
        private void ChangeBGMVolume()
        {
            BGMManager bgm_manager = GameObject.Find(GameConstants.Object.BGM_MANAGER).GetComponent<BGMManager>();

            float sound = _soundSlider[(int)SoundSlider.BGM].value;
            _soundSlider[(int)SoundSlider.BGM].value = bgm_manager.VolumeChange(sound);
        }

        /// <summary>
        /// SEの音量変更
        /// </summary>
        private void ChangeSEVolume()
        {
            SEManager se_manager = GameObject.Find(GameConstants.Object.SE_MANAGER).GetComponent<SEManager>();

            float sound = _soundSlider[(int)SoundSlider.SE].value;
            _soundSlider[(int)SoundSlider.SE].value = se_manager.VolumeChange(sound);
        }

        #endregion // サウンド設定

        /// <summary>
        /// GamePadのキー切り替え
        /// </summary>
        public void GamePadKeyChange()
        {
            _input.GamePadKeyChange();
        }

        #endregion // 設定メニューのステップ処理

        /// <summary>
        /// ステップの設定
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
        /// ステップの設定(Button呼び出し用)
        /// </summary>
        /// <param name="step"></param>
        public void SetStep(int step)
        {
            SetStep((TitleStep)step);
        }
    }
}