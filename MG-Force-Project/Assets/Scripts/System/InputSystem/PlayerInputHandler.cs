using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputAction moveAction;  // 移動アクション
    private InputAction jumpAction;  // ジャンプアクション
    private InputAction actionAction; // アクションアクション

    private float moveSpeed = 5f;
    private Animator animator; // Animator コンポーネント用

    private void Awake()
    {
        // Animator コンポーネントを取得
        animator = GetComponent<Animator>();

        // 移動アクションの設定（WASDキーと左スティック）
        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.AddBinding("<Gamepad>/leftStick");

        // ジャンプアクションの設定（スペースキーとSouthボタン）
        jumpAction = new InputAction("Jump", InputActionType.Button);
        jumpAction.AddBinding("<Keyboard>/space");
        jumpAction.AddBinding("<Gamepad>/buttonSouth");

        // アクションアクションの設定（FキーとWestボタン）
        actionAction = new InputAction("Action", InputActionType.Button);
        actionAction.AddBinding("<Keyboard>/f");
        actionAction.AddBinding("<Gamepad>/buttonWest");

        // アクションを有効化
        moveAction.Enable();
        jumpAction.Enable();
        actionAction.Enable();
    }

    private void Update()
    {
        // 移動入力を取得して移動処理
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        float horizontal = moveInput.x;

        transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);

        // 移動アニメーションの制御
        animator.SetFloat("MoveSpeed", Mathf.Abs(horizontal));

        // ジャンプ入力の処理
        if (jumpAction.triggered)
        {
            Debug.Log("ジャンプボタンが押されました！");
            animator.SetTrigger("Jump");
        }

        // アクション入力の処理
        if (actionAction.triggered)
        {
            Debug.Log("アクションボタンが押されました！");
            animator.SetTrigger("Action");
        }
    }

    private void OnDestroy()
    {
        // アクションを無効化
        moveAction.Disable();
        jumpAction.Disable();
        actionAction.Disable();
    }
}