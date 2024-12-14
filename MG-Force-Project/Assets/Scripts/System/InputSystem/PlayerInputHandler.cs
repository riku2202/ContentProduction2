using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputAction moveAction;  // �ړ��A�N�V����
    private InputAction jumpAction;  // �W�����v�A�N�V����
    private InputAction actionAction; // �A�N�V�����A�N�V����

    private float moveSpeed = 5f;
    private Animator animator; // Animator �R���|�[�l���g�p

    private void Awake()
    {
        // Animator �R���|�[�l���g���擾
        animator = GetComponent<Animator>();

        // �ړ��A�N�V�����̐ݒ�iWASD�L�[�ƍ��X�e�B�b�N�j
        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.AddBinding("<Gamepad>/leftStick");

        // �W�����v�A�N�V�����̐ݒ�i�X�y�[�X�L�[��South�{�^���j
        jumpAction = new InputAction("Jump", InputActionType.Button);
        jumpAction.AddBinding("<Keyboard>/space");
        jumpAction.AddBinding("<Gamepad>/buttonSouth");

        // �A�N�V�����A�N�V�����̐ݒ�iF�L�[��West�{�^���j
        actionAction = new InputAction("Action", InputActionType.Button);
        actionAction.AddBinding("<Keyboard>/f");
        actionAction.AddBinding("<Gamepad>/buttonWest");

        // �A�N�V������L����
        moveAction.Enable();
        jumpAction.Enable();
        actionAction.Enable();
    }

    private void Update()
    {
        // �ړ����͂��擾���Ĉړ�����
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        float horizontal = moveInput.x;

        transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);

        // �ړ��A�j���[�V�����̐���
        animator.SetFloat("MoveSpeed", Mathf.Abs(horizontal));

        // �W�����v���͂̏���
        if (jumpAction.triggered)
        {
            Debug.Log("�W�����v�{�^����������܂����I");
            animator.SetTrigger("Jump");
        }

        // �A�N�V�������͂̏���
        if (actionAction.triggered)
        {
            Debug.Log("�A�N�V�����{�^����������܂����I");
            animator.SetTrigger("Action");
        }
    }

    private void OnDestroy()
    {
        // �A�N�V�����𖳌���
        moveAction.Disable();
        jumpAction.Disable();
        actionAction.Disable();
    }
}