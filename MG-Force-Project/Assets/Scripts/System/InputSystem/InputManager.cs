using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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
        unityInputBindings["Action"] = new[] { KeyCode.F, KeyCode.JoystickButton0 };        // □ボタン or Fキー
        unityInputBindings["Jump"] = new[]   { KeyCode.Space, KeyCode.JoystickButton1 };    // ×ボタン or スペースキー
        unityInputBindings["Select"] = new[] { KeyCode.JoystickButton2 };                   // 〇ボタン
        unityInputBindings["Magnet Reset"] = new[] { KeyCode.R, KeyCode.JoystickButton3 };  // △ボタン or Rキー
        unityInputBindings["PoleSwitching"] = new[] { KeyCode.C, KeyCode.JoystickButton4 }; // L1ボタン or Cキー
        unityInputBindings["None"] = new[] { KeyCode.JoystickButton5 };                     // R1ボタン or
        unityInputBindings["Magnet Boot"] = new[] { KeyCode.B, KeyCode.JoystickButton6 };   // L2ボタン or Bキー
        unityInputBindings["Shoot"] = new[]  { KeyCode.Mouse0, KeyCode.JoystickButton7 };   // R2ボタン or 左クリック
        unityInputBindings["None"] = new[] { KeyCode.JoystickButton8 };                     // Shareボタン
        unityInputBindings["MenuChange"] = new[] { KeyCode.M, KeyCode.JoystickButton9 };    // optionボタン or Mキー
        unityInputBindings["None"] = new[] { KeyCode.JoystickButton10 };                    // Lスティック押し込み or
        unityInputBindings["ViewMode"] = new[] { KeyCode.V, KeyCode.JoystickButton11 };     // Rスティック押し込み or Vキー
        unityInputBindings["Move"] = new[] { KeyCode.A, KeyCode.D, KeyCode.JoystickButton0 };   //左スティックで移動させたいがInput.GetAxis("Horizontal");を使うらしくどうやって入れるか悩み中 
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
