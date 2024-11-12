using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CharacterControllerScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;       // �L�����N�^�[�̈ړ����x
    [SerializeField] float rotationSpeed = 700f; // �L�����N�^�[�̉�]���x
    [SerializeField] float jumpForce = 5f;       // �W�����v�̋���

    Rigidbody rb;            // �L�����N�^�[�̃��W�b�h�{�f�B
    bool isGrounded = true;   // �n�ʂɐڒn���Ă��邩�ǂ����̃t���O

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �v���C���[���͂̎擾
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �ړ��������v�Z
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // �L�����N�^�[���ړ������ɐi��
        if (movement.magnitude > 0)
        {
            // �ړ�����
            rb.MovePosition(transform.position + (moveSpeed * Time.deltaTime * movement));

            // �L�����N�^�[���ړ������ɉ�]����悤�ɂ���
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }

        // �W�����v���� (�o�c�{�^��)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;  // �W�����v���͒n�ʂɂ��Ă��Ȃ��Ƃ݂Ȃ�
        }
    }

    // �n�ʂɐڐG�����Ƃ��ɌĂ΂��
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // �n�ʂɐG�ꂽ��isGrounded��true��
        }
    }

    // �n�ʂɐڐG���Ă���ԌĂ΂��
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // �n�ʂɂ����isGrounded�����true�ɂ���
        }
    }

    // �n�ʂ��痣�ꂽ�Ƃ��ɌĂ΂��
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;  // �n�ʂ��痣�ꂽ��isGrounded��false��
        }
    }
}