using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Game.GameSystem
{
    public class InputManager : MonoBehaviour
    {
        // シングルトンインスタンス
        public static InputManager Instance { get; private set; }

        // InputActionアセット
        [SerializeField] private InputActionAsset inputActions;

        // アクション状態の辞書
        private Dictionary<string, bool> actionStates = new Dictionary<string, bool>();

        // Unity標準の入力状態
        private Dictionary<string, KeyCode[]> unityInputBindings = new Dictionary<string, KeyCode[]>();

        private void Awake()
        {
            // シングルトンの設定
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Input Systemの初期化
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
            unityInputBindings[GameConstants.INPUT_ACTION] = new[] { KeyCode.F, KeyCode.JoystickButton0 };        // □ボタン or Fキー
            unityInputBindings[GameConstants.INPUT_JUMP] = new[] { KeyCode.Space, KeyCode.JoystickButton1 };    // ×ボタン or スペースキー
            unityInputBindings[GameConstants.INPUT_SELECT] = new[] { KeyCode.JoystickButton2 };                   // 〇ボタン
            unityInputBindings[GameConstants.INPUT_MAGNET_RESET] = new[] { KeyCode.R, KeyCode.JoystickButton3 };  // △ボタン or Rキー
            unityInputBindings[GameConstants.INPUT_POLE_SWITCHING] = new[] { KeyCode.C, KeyCode.JoystickButton4 }; // L1ボタン or Cキー
            unityInputBindings[GameConstants.INPUT_NONE] = new[] { KeyCode.JoystickButton5 };                     // R1ボタン or
            unityInputBindings[GameConstants.INPUT_MANGET_BOOT] = new[] { KeyCode.B, KeyCode.JoystickButton6 };   // L2ボタン or Bキー
            unityInputBindings[GameConstants.INPUT_SHOOT] = new[] { KeyCode.Mouse0, KeyCode.JoystickButton7 };   // R2ボタン or 左クリック
            unityInputBindings[GameConstants.INPUT_NONE] = new[] { KeyCode.JoystickButton8 };                     // Shareボタン
            unityInputBindings[GameConstants.INPUT_MENU_CHANGE] = new[] { KeyCode.M, KeyCode.JoystickButton9 };    // optionボタン or Mキー
            unityInputBindings[GameConstants.INPUT_NONE] = new[] { KeyCode.JoystickButton10 };                    // Lスティック押し込み or
            unityInputBindings[GameConstants.INPUT_VIEWMODE] = new[] { KeyCode.V, KeyCode.JoystickButton11 };     // Rスティック押し込み or Vキー
            unityInputBindings[GameConstants.INPUT_MOVE] = new[] { KeyCode.A, KeyCode.D, KeyCode.JoystickButton0 };   //左スティックで移動させたいがInput.GetAxis("Horizontal");を使うらしくどうやって入れるか悩み中 
        }

        /// <summary>
        /// Input System経由でアクションが押されているかを取得
        /// </summary>
        public bool IsActionPressed(string actionName)
        {
            return actionStates.ContainsKey(actionName) && actionStates[actionName];
        }

        /// <summary>
        /// Unity標準のInputManager経由でアクションが押されているかを取得
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