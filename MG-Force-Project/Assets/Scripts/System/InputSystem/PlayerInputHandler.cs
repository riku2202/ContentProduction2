using Game.Stage.Magnet;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;   // �ړ����x
    [SerializeField] private float jumpForce = 7f;   // �W�����v�̗�

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab; // �e�̃v���n�u
    [SerializeField] private Transform firePoint;     // �e�̔��ˈʒu
    [SerializeField] private float bulletSpeed = 10f; // �e�̑��x

    private InputAction moveAction;        // �ړ��A�N�V����
    private InputAction jumpAction;        // �W�����v�A�N�V����
    private InputAction actionAction;      // �A�N�V�����{�^���A�N�V����
    private InputAction shootAction;       // �e�̔��˃A�N�V����
    private InputAction shootAngleAction;  // �e�̊p�x�����A�N�V����

    private Vector2 moveInput;             // �ړ����͒l
    private float shootAngle = 0f;         // ���ˊp�x�i�㉺�����j

    private Rigidbody rigidbodyComponent;  // Rigidbody �R���|�[�l���g
    private Animator animator;             // Animator �R���|�[�l���g
    private bool isGrounded = false;       // �v���C���[���n�ʂɂ��邩�̔���

    private void Awake()
    {
        // Rigidbody �� Animator ���擾
        rigidbodyComponent = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (rigidbodyComponent == null)
        {
            Debug.LogError("Rigidbody �R���|�[�l���g��������܂���I");
        }

        if (animator == null)
        {
            Debug.LogError("Animator �R���|�[�l���g��������܂���I");
        }

        // Input Actions �̏�����
        InitializeInputActions();
    }

    /// <summary>
    /// Input Actions �̏�����
    /// </summary>
    private void InitializeInputActions()
    {
        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/a")
            .With("Positive", "<Keyboard>/d");
        moveAction.AddBinding("<Gamepad>/leftStick/x");

        // �W�����v�A�N�V�����̐ݒ�i�X�y�[�X�L�[��South�{�^���j
        jumpAction = new InputAction("Jump", InputActionType.Button);
        jumpAction.AddBinding("<Keyboard>/space");
        jumpAction.AddBinding("<Gamepad>/buttonSouth");

        // �A�N�V�����A�N�V�����̐ݒ�iF�L�[��West�{�^���j
        actionAction = new InputAction("Action", InputActionType.Button);
        actionAction.AddBinding("<Keyboard>/f");
        actionAction.AddBinding("<Gamepad>/buttonWest");
        
        // �V���b�g�A���O���̐ݒ� (���N���b�N��R2�{�^��)
        shootAction = new InputAction("Shoot", InputActionType.Button);
        shootAction.AddBinding("<Mouse>/leftButton");
        shootAction.AddBinding("<Gamepad>/rightTrigger");

        shootAngleAction = new InputAction("ShootAngle", InputActionType.Value);
        shootAngleAction.AddBinding("<Gamepad>/rightStick/y"); // �E�X�e�B�b�NY���̂ݎ擾

        // �A�N�V������L����
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
    /// �ړ����͂ƃA�j���[�V�����̐���
    /// </summary>
    private void HandleMovementInput()
    {
        moveInput.x = moveAction.ReadValue<float>();

        // �ړ��A�j���[�V�����̐���
        animator.SetFloat("MoveSpeed", Mathf.Abs(moveInput.x));
    }

    /// <summary>
    /// �W�����v���͂ƃA�j���[�V�����̐���
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
    /// �A�N�V�������͂ƃA�j���[�V�����̐���
    /// </summary>
    private void HandleActionInput()
    {
        if (actionAction.triggered)
        {
            Debug.Log("�A�N�V�����{�^����������܂����I");
            animator.SetTrigger("Action");
        }
    }

    /// <summary>
    /// �ˌ����͂Ɗp�x�����̐���
    /// </summary>
    private void HandleShootInput()
    {
        // �ˌ��p�x��Y���i�㉺�j�ɐ���
        float angleInput = shootAngleAction.ReadValue<float>();
        shootAngle = Mathf.Clamp(angleInput, -1f, 1f); // -1�i���j�` 1�i��j�ɐ���

        if (shootAction.triggered)
        {
            Shoot();
            animator.SetTrigger("Shoot");
        }
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void Move()
    {
        Vector3 moveVector = new Vector3(moveInput.x * moveSpeed, rigidbodyComponent.velocity.y, 0);
        rigidbodyComponent.velocity = moveVector;
    }

    /// <summary>
    /// �W�����v����
    /// </summary>
    private void Jump()
    {
        rigidbodyComponent.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        Debug.Log("�W�����v���܂����I");
    }

    /// <summary>
    /// �e�̔��ˏ����i�㉺�����̊p�x�̂ݔ��f�j
    /// </summary>
    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("�e��Prefab�܂��͔��ˈʒu���ݒ肳��Ă��܂���I");
            return;
        }

        // �e�̔��˕����iX���Œ�ŏ㉺�ɒ����j
        Vector3 shootDirection = new Vector3(1, shootAngle, 0).normalized;

        // �e�𐶐�
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
        // �A�N�V�����𖳌���
        moveAction.Disable();
        jumpAction.Disable();
        actionAction.Disable();
        shootAction.Disable();
        shootAngleAction.Disable();
    }
}