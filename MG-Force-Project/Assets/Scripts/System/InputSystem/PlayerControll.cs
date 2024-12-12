using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControll : MonoBehaviour
{
    private const float JumpForce = 5f;        // �W�����v�� (�Œ�l�Ƃ��Ē�`)
    private const float GroundCheckDistance = 0.1f; // �n�ʔ���̋���
    private Rigidbody _rigidbody;             // Rigidbody�̎Q��
    private bool _isGrounded;                 // �n�ʂɐڐG���Ă��邩

    private void Awake()
    {
        // Rigidbody�R���|�[�l���g�̎擾
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // �n�ʔ���
        _isGrounded = IsGrounded();

        // ���͏���
        HandleInput();
    }

    private void HandleInput()
    {
        // �W�����v����
        if (_isGrounded && InputManager.Instance.IsActionPressed("Jump"))
        {
            Jump();
        }

        // 
        // �U������
        if (InputManager.Instance.IsActionPressed("Shoot"))
        {
            Shoot();
        }
    }

    private void Jump()
    {
        // ���������̑��x���W�����v�͂ɐݒ�
        var velocity = _rigidbody.velocity;
        velocity.y = JumpForce;
        _rigidbody.velocity = velocity;
    }

    private void Shoot()
    {
        Debug.Log("Shoot executed!");
    }

    private bool IsGrounded()
    {
        // �n�ʂƂ̋������`�F�b�N
        return Physics.Raycast(transform.position, Vector3.down, GroundCheckDistance);
    }
}