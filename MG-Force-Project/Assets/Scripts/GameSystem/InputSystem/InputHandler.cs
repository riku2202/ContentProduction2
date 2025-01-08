using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game.GameSystem
{
    public class InputHandler : MonoBehaviour
    {
        private const float THRES_HOLD = 0.1f;

        private PlayerInput _playerInput;

        private DeviceManager _deviceManager;

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

            _deviceManager = GameObject.Find(GameConstants.Object.DEVICE_MANAGER).GetComponent<DeviceManager>();

            InitializeInputSystem();

            _playerInput.SwitchCurrentControlScheme(InputConstants.ActionDevice.KEY_MOUSE, InputSystem.GetDevice<Keyboard>(), InputSystem.GetDevice<Mouse>());
            _playerInput.SwitchCurrentActionMap(InputConstants.ActionMaps.MENU_MAPS);
        }

        #endregion

        // InputAction�A�Z�b�g
        private InputActionAsset _inputActions;

        // �A�N�V������Ԃ̎���
        private Dictionary<string, bool> _actionStates = new Dictionary<string, bool>();

        private bool _isKeyChange;

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
            InputDeviceUpdate();

            InputEnableUpdate();
        }

        private void InputDeviceUpdate()
        {
            if (_deviceManager.isGamepad && _playerInput.currentControlScheme == InputConstants.ActionDevice.KEY_MOUSE)
            {
                _playerInput.SwitchCurrentControlScheme(
                    (!_isKeyChange) ? InputConstants.ActionDevice.GAMEPAD : InputConstants.ActionDevice.GAMEPAD_2, InputSystem.GetDevice<Gamepad>());
            }
            else if (!_deviceManager.isGamepad && _playerInput.currentControlScheme != InputConstants.ActionDevice.KEY_MOUSE)
            {
                _playerInput.SwitchCurrentControlScheme(InputConstants.ActionDevice.KEY_MOUSE, InputSystem.GetDevice<Keyboard>(), InputSystem.GetDevice<Mouse>());
            }
        }

        /// <summary>
        /// Input System�o�R�ŃA�N�V������������Ă��邩���擾
        /// </summary>
        public bool IsActionPressed(string action_name)
        {
            bool is_action_pressed = _playerInput.actions[action_name].triggered;

            return is_action_pressed;
        }

        public bool IsActionPressing(string action_name)
        {
            float current_action_pressed = _playerInput.actions[action_name].ReadValue<float>();

            return Mathf.Abs(current_action_pressed) >= THRES_HOLD;
        }

        public bool IsActionPressing(string action_name, Vector2 type)
        {
            Vector2 current_action_vector = _playerInput.actions[action_name].ReadValue<Vector2>();

            return Vector2.Distance(current_action_vector, type) >= THRES_HOLD;
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

            _playerInput.actions.FindActionMap(InputConstants.ActionMaps.SHORTCUT_MAPS).Enable();

#if UNITY_EDITOR

            _playerInput.actions.FindActionMap(InputConstants.ActionMaps.DEBUG_MAPS).Enable();

#endif
        }

        #region -------- �L�[�̗L�������� --------

        private bool isMenuOpen = false;
        private bool isViewModeStart = false;

        private bool isMagnetBoot = false;

        private void InputSetMenuMap()
        {
            if (_playerInput.currentActionMap.name != InputConstants.ActionMaps.MENU_MAPS)
            {
                _playerInput.SwitchCurrentActionMap(InputConstants.ActionMaps.MENU_MAPS);
            }
        }

        private void InputStageSelectScene()
        {
            // ���j���[���J����Ă��Ȃ��Ƃ��Ƀv���C���[�̓��͂�L��������
            if (_playerInput.currentActionMap.name != InputConstants.ActionMaps.PLAYER_MAPS && !isMenuOpen)
            {
                _playerInput.SwitchCurrentActionMap(InputConstants.ActionMaps.PLAYER_MAPS);
            }

            // ���j���[���J����Ă��Ȃ��Ƃ��Ƀ��j���[���J��
            if (_playerInput.actions[InputConstants.Action.MENU_OPEN].triggered && !isMenuOpen)
            {
                _playerInput.SwitchCurrentActionMap(InputConstants.ActionMaps.MENU_MAPS);
                isMenuOpen = true;
            }
            // ���j���[���J����Ă���Ƃ��Ƀ��j���[�����
            else if (_playerInput.actions[InputConstants.Action.MENU_CLOSE].triggered && isMenuOpen)
            {
                isMenuOpen = false;
            }

            // ���j���[���J����Ă��Ȃ������͂��N�������Ƃ��A���͊֘A�̃t���O��ON�ɂ���
            if (_playerInput.actions[InputConstants.Action.MAGNET_BOOT].triggered && !isMagnetBoot && !isMenuOpen)
            {
                isMagnetBoot = true;
            }
            // ���j���[���J����Ă��Ȃ������͂��~�����Ƃ��A���͊֘A�̃t���O��OFF�ɂ���
            else if (_playerInput.actions[InputConstants.Action.MAGNET_BOOT].triggered && isMagnetBoot)
            {
                isMagnetBoot = false;
            }

            if (!isMagnetBoot && !isMenuOpen)
            {
                _playerInput.actions.FindActionMap(InputConstants.ActionMaps.MAGNET_MAPS).Enable();
            }
        }

        private void InputStageScene()
        {
            if (_playerInput.currentActionMap.name != InputConstants.ActionMaps.PLAYER_MAPS && !isMenuOpen && !isViewModeStart)
            {
                _playerInput.SwitchCurrentActionMap(InputConstants.ActionMaps.PLAYER_MAPS);
            }

            if (_playerInput.actions[InputConstants.Action.MENU_OPEN].triggered && !isMenuOpen && !isViewModeStart)
            {
                _playerInput.SwitchCurrentActionMap(InputConstants.ActionMaps.MENU_MAPS);
                isMenuOpen = true;
            }
            else if (_playerInput.actions[InputConstants.Action.MENU_CLOSE].triggered && isMenuOpen)
            {
                _playerInput.SwitchCurrentActionMap(InputConstants.ActionMaps.PLAYER_MAPS);
                isMenuOpen = false;
            }

            if (_playerInput.actions[InputConstants.Action.VIEW_MODE_START].triggered && !isViewModeStart && !isMenuOpen && !isMagnetBoot)
            {
                _playerInput.SwitchCurrentActionMap(InputConstants.ActionMaps.CAMERA_MAPS);
                isViewModeStart = true;
            }
            else if (_playerInput.actions[InputConstants.Action.VIEW_MODE_END].triggered && isViewModeStart)
            {
                _playerInput.SwitchCurrentActionMap(InputConstants.ActionMaps.PLAYER_MAPS);
                isViewModeStart = false;
            }

            if (_playerInput.actions[InputConstants.Action.MAGNET_BOOT].triggered && !isMagnetBoot && !isMenuOpen && !isViewModeStart)
            {
                isMagnetBoot = true;
            }
            else if (_playerInput.actions[InputConstants.Action.MAGNET_BOOT].triggered && isMagnetBoot)
            {
                isMagnetBoot = false;
            }

            if (!isMagnetBoot)
            {
                _playerInput.actions.FindActionMap(InputConstants.ActionMaps.MAGNET_MAPS).Enable();
            }
        }

        #endregion

        public void KeyChange()
        {
            _isKeyChange = !_isKeyChange;

            if (_playerInput.currentControlScheme == InputConstants.ActionDevice.KEY_MOUSE) return;

            _playerInput.SwitchCurrentControlScheme(
                (!_isKeyChange) ? InputConstants.ActionDevice.GAMEPAD : InputConstants.ActionDevice.GAMEPAD_2, InputSystem.GetDevice<Gamepad>());
        }

        public string GetControlScheme()
        {
            return _playerInput.currentControlScheme;
        }
    }
}