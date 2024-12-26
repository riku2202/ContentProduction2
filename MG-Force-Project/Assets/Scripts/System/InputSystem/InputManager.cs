using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Game.GameSystem
{
    public class InputManager : MonoBehaviour
    {
        // �V���O���g���C���X�^���X
        public static InputManager Instance { get; private set; }

        // InputAction�A�Z�b�g
        [SerializeField] private InputActionAsset inputActions;

        // �A�N�V������Ԃ̎���
        private Dictionary<string, bool> actionStates = new Dictionary<string, bool>();

        // Unity�W���̓��͏��
        private Dictionary<string, KeyCode[]> unityInputBindings = new Dictionary<string, KeyCode[]>();

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

            // Input System�̏�����
            InitializeInputSystem();
            InitializeUnityInputBindings();
        }

        private void InitializeInputSystem()
        {
            foreach (var map in inputActions.actionMaps)
            {
                foreach (var action in map.actions)
                {
                    actionStates[action.name] = false;
                    action.performed += ctx => actionStates[action.name] = true;
                    action.canceled += ctx => actionStates[action.name] = false;
                    action.Enable();
                }
            }
        }

        private void InitializeUnityInputBindings()
        {
            unityInputBindings[GameConstants.INPUT_ACTION] = new[] { KeyCode.F, KeyCode.JoystickButton0 };        // ���{�^�� or F�L�[
            unityInputBindings[GameConstants.INPUT_JUMP] = new[] { KeyCode.Space, KeyCode.JoystickButton1 };    // �~�{�^�� or �X�y�[�X�L�[
            unityInputBindings[GameConstants.INPUT_SELECT] = new[] { KeyCode.JoystickButton2 };                   // �Z�{�^��
            unityInputBindings[GameConstants.INPUT_MAGNET_RESET] = new[] { KeyCode.R, KeyCode.JoystickButton3 };  // ���{�^�� or R�L�[
            unityInputBindings[GameConstants.INPUT_POLE_SWITCHING] = new[] { KeyCode.C, KeyCode.JoystickButton4 }; // L1�{�^�� or C�L�[
            unityInputBindings[GameConstants.INPUT_NONE] = new[] { KeyCode.JoystickButton5 };                     // R1�{�^�� or
            unityInputBindings[GameConstants.INPUT_MANGET_BOOT] = new[] { KeyCode.B, KeyCode.JoystickButton6 };   // L2�{�^�� or B�L�[
            unityInputBindings[GameConstants.INPUT_SHOOT] = new[] { KeyCode.Mouse0, KeyCode.JoystickButton7 };   // R2�{�^�� or ���N���b�N
            unityInputBindings[GameConstants.INPUT_NONE] = new[] { KeyCode.JoystickButton8 };                     // Share�{�^��
            unityInputBindings[GameConstants.INPUT_MENU_CHANGE] = new[] { KeyCode.M, KeyCode.JoystickButton9 };    // option�{�^�� or M�L�[
            unityInputBindings[GameConstants.INPUT_NONE] = new[] { KeyCode.JoystickButton10 };                    // L�X�e�B�b�N�������� or
            unityInputBindings[GameConstants.INPUT_VIEWMODE] = new[] { KeyCode.V, KeyCode.JoystickButton11 };     // R�X�e�B�b�N�������� or V�L�[
            unityInputBindings[GameConstants.INPUT_MOVE] = new[] { KeyCode.A, KeyCode.D, KeyCode.JoystickButton0 };   //���X�e�B�b�N�ňړ�����������Input.GetAxis("Horizontal");���g���炵���ǂ�����ē���邩�Y�ݒ� 
        }

        /// <summary>
        /// Input System�o�R�ŃA�N�V������������Ă��邩���擾
        /// </summary>
        public bool IsActionPressed(string actionName)
        {
            return actionStates.ContainsKey(actionName) && actionStates[actionName];
        }

        /// <summary>
        /// Unity�W����InputManager�o�R�ŃA�N�V������������Ă��邩���擾
        /// </summary>
        public bool IsUnityInputPressed(string actionName)
        {
            if (unityInputBindings.TryGetValue(actionName, out var keys))
            {
                foreach (var key in keys)
                {
                    if (Input.GetKey(key)) return true;
                }
            }
            return false;
        }
    }
}