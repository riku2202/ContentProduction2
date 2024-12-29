using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game.GameSystem
{
    public class InputHandler : MonoBehaviour
    {
        private const string GAMEPAD_IN_MESSAGE = "�R���g���[���[���ڑ�����܂���";
        private const string GAMEPAD_OUT_MESSAGE = "�R���g���[���[���ؒf����܂���";

        private PlayerInput _playerInput;

        private SystemMessageManager _systemMessage = SystemMessageManager.instance;

        #region -------- �V���O���g���̐ݒ� --------

        // �V���O���g���C���X�^���X
        public static InputHandler Instance { get; private set; }

        private void Awake()
        {
            // �V���O���g���̐ݒ�
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _playerInput = GetComponent<PlayerInput>();

            InitializeInputSystem();

            InputConnectionUpdate();

            _playerInput.SwitchCurrentActionMap(GameConstants.Input.ActionMaps.MENU_MAPS);
        }

        #endregion

        // InputAction�A�Z�b�g
        private InputActionAsset _inputActions;

        // �A�N�V������Ԃ̎���
        private Dictionary<string, bool> _actionStates = new Dictionary<string, bool>();

        private void InitializeInputSystem()
        {
            _inputActions = _playerInput.actions;

            foreach (var map in _inputActions.actionMaps)
            {
                foreach (var action in map.actions)
                {
                    _actionStates[action.name] = false;
                    action.performed += ctx => _actionStates[action.name] = true;
                    action.canceled += ctx => _actionStates[action.name] = false;
                }
            }
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            // ���̓f�o�C�X�̍X�V
            InputConnectionUpdate();

            InputEnableUpdate();
        }

        /// <summary>
        /// Input System�o�R�ŃA�N�V������������Ă��邩���擾
        /// </summary>
        public bool IsActionPressed(string action_name)
        {
            return _playerInput.actions[action_name].triggered;
        }

        public bool IsActionPressing(string action_name)
        {
            return _actionStates[action_name];
        }

        /// <summary>
        /// ���̓f�o�C�X�̍X�V
        /// </summary>
        private void InputConnectionUpdate()
        {
            if (Gamepad.current != null && _playerInput.currentControlScheme != GameConstants.Input.ActionDevice.GAMEPAD)
            {
                _playerInput.SwitchCurrentControlScheme(GameConstants.Input.ActionDevice.GAMEPAD);

                _systemMessage.DrawMessage($"{Gamepad.current.name}, {GAMEPAD_IN_MESSAGE}");
            }
            else if (_playerInput.currentControlScheme != GameConstants.Input.ActionDevice.KEY_MOUSE)
            {
                _playerInput.SwitchCurrentControlScheme(GameConstants.Input.ActionDevice.KEY_MOUSE);

                _systemMessage.DrawMessage($"{Gamepad.current.name}, {GAMEPAD_OUT_MESSAGE}");
            }
        }

        private void InputEnableUpdate()
        {
            Scene scene = SceneManager.GetActiveScene();

            switch ((GameConstants.Scene)scene.buildIndex) 
            {
                case GameConstants.Scene.StageSelect:
                    InputStageSelectScene();
                    break;

                case GameConstants.Scene.Stage:
                    InputStageScene();
                    break;

                default:
                    InputSetMenuMap();
                    break;
            }
        }

        #region -------- �L�[�̗L�������� --------

        private bool isMenuOpen = false;
        private bool isViewModeStart = false;

        private bool isMagnetBoot = false;

        private void InputSetMenuMap()
        {
            if (_playerInput.currentActionMap.name != GameConstants.Input.ActionMaps.MENU_MAPS)
            {
                _playerInput.SwitchCurrentActionMap(GameConstants.Input.ActionMaps.MENU_MAPS);
            }
        }

        private void InputStageSelectScene()
        {
            if (_playerInput.currentActionMap.name != GameConstants.Input.ActionMaps.PLAYER_MAPS && !isMenuOpen)
            {
                _playerInput.SwitchCurrentActionMap(GameConstants.Input.ActionMaps.PLAYER_MAPS);
            }

            if (_playerInput.actions[GameConstants.Input.Action.MENU_OPEN].triggered && !isMenuOpen)
            {
                _playerInput.SwitchCurrentActionMap(GameConstants.Input.ActionMaps.MENU_MAPS);
                isMenuOpen = true;
            }
            else if (_playerInput.actions[GameConstants.Input.Action.MENU_CLOSE].triggered && isMenuOpen)
            {
                isMenuOpen = false;
            }

            if (_playerInput.actions[GameConstants.Input.Action.MAGNET_BOOT].triggered && !isMagnetBoot && !isMenuOpen)
            {
                isMagnetBoot = true;
            }
            else if (_playerInput.actions[GameConstants.Input.Action.MAGNET_BOOT].triggered && isMagnetBoot)
            {
                isMagnetBoot = false;
            }

            if (!isMagnetBoot)
            {
                _playerInput.actions.FindActionMap(GameConstants.Input.ActionMaps.MAGNET_MAPS).Enable();
            }
        }

        private void InputStageScene()
        {
            if (_playerInput.currentActionMap.name != GameConstants.Input.ActionMaps.PLAYER_MAPS && !isMenuOpen && !isViewModeStart)
            {
                _playerInput.SwitchCurrentActionMap(GameConstants.Input.ActionMaps.PLAYER_MAPS);
            }

            if (_playerInput.actions[GameConstants.Input.Action.MENU_OPEN].triggered && !isMenuOpen && !isViewModeStart)
            {
                _playerInput.SwitchCurrentActionMap(GameConstants.Input.ActionMaps.MENU_MAPS);
                isMenuOpen = true;
            }
            else if (_playerInput.actions[GameConstants.Input.Action.MENU_CLOSE].triggered && isMenuOpen)
            {
                _playerInput.SwitchCurrentActionMap(GameConstants.Input.ActionMaps.PLAYER_MAPS);
                isMenuOpen = false;
            }

            if (_playerInput.actions[GameConstants.Input.Action.VIEW_MODE_START].triggered && !isViewModeStart && !isMenuOpen && !isMagnetBoot)
            {
                _playerInput.SwitchCurrentActionMap(GameConstants.Input.ActionMaps.CAMERA_MAPS);
                isViewModeStart = true;
            }
            else if (_playerInput.actions[GameConstants.Input.Action.VIEW_MODE_END].triggered && isViewModeStart)
            {
                _playerInput.SwitchCurrentActionMap(GameConstants.Input.ActionMaps.PLAYER_MAPS);
                isViewModeStart = false;
            }

            if (_playerInput.actions[GameConstants.Input.Action.MAGNET_BOOT].triggered && !isMagnetBoot && !isMenuOpen && !isViewModeStart)
            {
                isMagnetBoot = true;
            }
            else if (_playerInput.actions[GameConstants.Input.Action.MAGNET_BOOT].triggered && isMagnetBoot)
            {
                isMagnetBoot = false;
            }

            if (!isMagnetBoot)
            {
                _playerInput.actions.FindActionMap(GameConstants.Input.ActionMaps.MAGNET_MAPS).Enable();
            }
        }

        #endregion
    }
}