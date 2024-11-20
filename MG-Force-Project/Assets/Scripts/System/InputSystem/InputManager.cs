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
        unityInputBindings["Action"] = new[] { KeyCode.F, KeyCode.JoystickButton0 };       // □ボタン or Fキー
        unityInputBindings["Jump"] = new[]   { KeyCode.Space, KeyCode.JoystickButton1 };   // ×ボタン or スペースキー
        unityInputBindings["Select"] = new[] { KeyCode.JoystickButton2 };                  // 〇ボタン
        unityInputBindings["Shoot"] = new[]  { KeyCode.Mouse0, KeyCode.JoystickButton7 };  // ゲームパッドR2ボタン or 左クリック
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
