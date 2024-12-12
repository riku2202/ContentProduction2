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

    // Awake�ŏ�����
    private void Awake()
    {
        // PlayerInputActions ���C���X�^���X��
        inputActions = new PlayerInputActions();

        // �A�N�V������ݒ�
        moveAction = inputActions.Player.Move;
        actionAction = inputActions.Player.Action;
        jumpAction = inputActions.Player.Jump;
    }

    // OnEnable�ŃA�N�V������L���ɂ��AOnDisable�Ŗ�����
    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Update�ňړ��Ȃǂ̏��������s
    private void Update()
    {
        // �ړ����͂��擾
        Vector2 moveInput = moveAction.ReadValue<Vector2>();  // ���X�e�B�b�N�̓��͂ƃL�[�{�[�h�̓��͂��܂Ƃ߂Ď擾

        // �W�����v���͂̏���
        if (jumpAction.triggered)
        {
            Debug.Log("�W�����v�{�^����������܂����I");
        }

        // �A�N�V�������͂̏���
        if (actionAction.triggered)
        {
            Debug.Log("�A�N�V�����{�^����������܂����I");
        }

        // ���ړ������i���X�e�B�b�N��X����A/D�L�[�ňړ��j
        float horizontal = moveInput.x;

        // A�L�[��D�L�[�ł̈ړ��i�L�[�{�[�h���́j
        if (Keyboard.current.aKey.isPressed)
        {
            horizontal = -1f; // A�L�[�ō��ړ�
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            horizontal = 1f;  // D�L�[�ŉE�ړ�
        }

        // ���ړ�����
        transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);
    }
}