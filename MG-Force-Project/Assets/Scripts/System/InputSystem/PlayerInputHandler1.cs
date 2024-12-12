using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler1 : MonoBehaviour
{
    private InputAction moveAction;  // �ړ��A�N�V����
    private InputAction jumpAction; // �W�����v�A�N�V����
    private InputAction actionAction; // �A�N�V�����{�^��

    private float moveSpeed = 5f;

    private void Awake()
    {
        // �ړ��A�N�V�����̐ݒ�i���X�e�B�b�N�� WASD �L�[�j
        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.AddBinding("<Gamepad>/leftStick"); // ���X�e�B�b�N���ǉ�

        // �W�����v�A�N�V�����̐ݒ�i�X�y�[�X�L�[�� South �{�^���j
        jumpAction = new InputAction("Jump", InputActionType.Button);
        jumpAction.AddBinding("<Keyboard>/space");
        jumpAction.AddBinding("<Gamepad>/buttonSouth"); // A�{�^���i��ʓI�� South �{�^���j

        // �A�N�V�����{�^���̐ݒ�iF�L�[�� West �{�^���j
        actionAction = new InputAction("Action", InputActionType.Button);
        actionAction.AddBinding("<Keyboard>/f");
        actionAction.AddBinding("<Gamepad>/buttonWest"); // ���{�^���i��ʓI�� West �{�^���j

        // �A�N�V������L����
        moveAction.Enable();
        jumpAction.Enable();
        actionAction.Enable();
    }

    private void OnDestroy()
    {
        // �A�N�V�����𖳌���
        moveAction.Disable();
        jumpAction.Disable();
        actionAction.Disable();
    }

    private void Update()
    {
        // �ړ����͂��擾
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        // �������̈ړ��l
        float horizontal = moveInput.x;

        // ���ړ�����
        transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);

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
    }
}