using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private InputAction moveAction;
    private InputAction actionAction;
    private InputAction jumpAction;

    private float moveSpeed = 5f;

    // Awakeで初期化
    private void Awake()
    {
        // PlayerInputActions をインスタンス化
        inputActions = new PlayerInputActions();

        // アクションを設定
        moveAction = inputActions.Player.Move;
        actionAction = inputActions.Player.Action;
        jumpAction = inputActions.Player.Jump;
    }

    // OnEnableでアクションを有効にし、OnDisableで無効化
    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Updateで移動などの処理を実行
    private void Update()
    {
        // 移動入力を取得
        Vector2 moveInput = moveAction.ReadValue<Vector2>();  // 左スティックの入力とキーボードの入力をまとめて取得

        // ジャンプ入力の処理
        if (jumpAction.triggered)
        {
            Debug.Log("ジャンプボタンが押されました！");
        }

        // アクション入力の処理
        if (actionAction.triggered)
        {
            Debug.Log("アクションボタンが押されました！");
        }

        // 横移動処理（左スティックのX軸とA/Dキーで移動）
        float horizontal = moveInput.x;

        // AキーとDキーでの移動（キーボード入力）
        if (Keyboard.current.aKey.isPressed)
        {
            horizontal = -1f; // Aキーで左移動
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            horizontal = 1f;  // Dキーで右移動
        }

        // 横移動処理
        transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);
    }
}