using Game.Stage.Magnet;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;   // 移動速度
    [SerializeField] private float jumpForce = 7f;   // ジャンプの力

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab; // 弾のプレハブ
    [SerializeField] private Transform firePoint;     // 弾の発射位置
    [SerializeField] private float bulletSpeed = 10f; // 弾の速度

    private InputAction moveAction;        // 移動アクション
    private InputAction jumpAction;        // ジャンプアクション
    private InputAction actionAction;      // アクションボタンアクション
    private InputAction shootAction;       // 弾の発射アクション
    private InputAction shootAngleAction;  // 弾の角度調整アクション

    private Vector2 moveInput;             // 移動入力値
    private float shootAngle = 0f;         // 発射角度（上下方向）

    private Rigidbody rigidbodyComponent;  // Rigidbody コンポーネント
    private Animator animator;             // Animator コンポーネント
    private bool isGrounded = false;       // プレイヤーが地面にいるかの判定

    private void Awake()
    {
        // Rigidbody と Animator を取得
        rigidbodyComponent = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (rigidbodyComponent == null)
        {
            Debug.LogError("Rigidbody コンポーネントが見つかりません！");
        }

        if (animator == null)
        {
            Debug.LogError("Animator コンポーネントが見つかりません！");
        }

        // Input Actions の初期化
        InitializeInputActions();
    }

    /// <summary>
    /// Input Actions の初期化
    /// </summary>
    private void InitializeInputActions()
    {
        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/a")
            .With("Positive", "<Keyboard>/d");
        moveAction.AddBinding("<Gamepad>/leftStick/x");

        // ジャンプアクションの設定（スペースキーとSouthボタン）
        jumpAction = new InputAction("Jump", InputActionType.Button);
        jumpAction.AddBinding("<Keyboard>/space");
        jumpAction.AddBinding("<Gamepad>/buttonSouth");

        // アクションアクションの設定（FキーとWestボタン）
        actionAction = new InputAction("Action", InputActionType.Button);
        actionAction.AddBinding("<Keyboard>/f");
        actionAction.AddBinding("<Gamepad>/buttonWest");
        
        // ショットアングルの設定 (左クリックとR2ボタン)
        shootAction = new InputAction("Shoot", InputActionType.Button);
        shootAction.AddBinding("<Mouse>/leftButton");
        shootAction.AddBinding("<Gamepad>/rightTrigger");

        shootAngleAction = new InputAction("ShootAngle", InputActionType.Value);
        shootAngleAction.AddBinding("<Gamepad>/rightStick/y"); // 右スティックY軸のみ取得

        // アクションを有効化
        moveAction.Enable();
        jumpAction.Enable();
        actionAction.Enable();
        shootAction.Enable();
        shootAngleAction.Enable();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleActionInput();
        HandleShootInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 移動入力とアニメーションの制御
    /// </summary>
    private void HandleMovementInput()
    {
        moveInput.x = moveAction.ReadValue<float>();

        // 移動アニメーションの制御
        animator.SetFloat("MoveSpeed", Mathf.Abs(moveInput.x));
    }

    /// <summary>
    /// ジャンプ入力とアニメーションの制御
    /// </summary>
    private void HandleJumpInput()
    {
        if (jumpAction.triggered && isGrounded)
        {
            Jump();
            animator.SetTrigger("Jump");
        }
    }

    /// <summary>
    /// アクション入力とアニメーションの制御
    /// </summary>
    private void HandleActionInput()
    {
        if (actionAction.triggered)
        {
            Debug.Log("アクションボタンが押されました！");
            animator.SetTrigger("Action");
        }
    }

    /// <summary>
    /// 射撃入力と角度調整の制御
    /// </summary>
    private void HandleShootInput()
    {
        // 射撃角度をY軸（上下）に制限
        float angleInput = shootAngleAction.ReadValue<float>();
        shootAngle = Mathf.Clamp(angleInput, -1f, 1f); // -1（下）～ 1（上）に制限

        if (shootAction.triggered)
        {
            Shoot();
            animator.SetTrigger("Shoot");
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        Vector3 moveVector = new Vector3(moveInput.x * moveSpeed, rigidbodyComponent.velocity.y, 0);
        rigidbodyComponent.velocity = moveVector;
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    private void Jump()
    {
        rigidbodyComponent.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        Debug.Log("ジャンプしました！");
    }

    /// <summary>
    /// 弾の発射処理（上下方向の角度のみ反映）
    /// </summary>
    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("弾のPrefabまたは発射位置が設定されていません！");
            return;
        }

        // 弾の発射方向（X軸固定で上下に調整）
        Vector3 shootDirection = new Vector3(1, shootAngle, 0).normalized;

        // 弾を生成
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.Fire(shootDirection, bulletSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnDestroy()
    {
        // アクションを無効化
        moveAction.Disable();
        jumpAction.Disable();
        actionAction.Disable();
        shootAction.Disable();
        shootAngleAction.Disable();
    }
}