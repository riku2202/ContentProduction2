using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler1 : MonoBehaviour
{
    private InputAction moveAction;  // 移動アクション
    private InputAction jumpAction; // ジャンプアクション
    private InputAction actionAction; // アクションボタン

    private float moveSpeed = 5f;

    private void Awake()
    {
        // 移動アクションの設定（左スティックと WASD キー）
        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.AddBinding("<Gamepad>/leftStick"); // 左スティックも追加

        // ジャンプアクションの設定（スペースキーと South ボタン）
        jumpAction = new InputAction("Jump", InputActionType.Button);
        jumpAction.AddBinding("<Keyboard>/space");
        jumpAction.AddBinding("<Gamepad>/buttonSouth"); // Aボタン（一般的に South ボタン）

        // アクションボタンの設定（Fキーと West ボタン）
        actionAction = new InputAction("Action", InputActionType.Button);
        actionAction.AddBinding("<Keyboard>/f");
        actionAction.AddBinding("<Gamepad>/buttonWest"); // □ボタン（一般的に West ボタン）

        // アクションを有効化
        moveAction.Enable();
        jumpAction.Enable();
        actionAction.Enable();
    }

    private void OnDestroy()
    {
        // アクションを無効化
        moveAction.Disable();
        jumpAction.Disable();
        actionAction.Disable();
    }

    private void Update()
    {
        // 移動入力を取得
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        // 横方向の移動値
        float horizontal = moveInput.x;

        // 横移動処理
        transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);

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
    }
}