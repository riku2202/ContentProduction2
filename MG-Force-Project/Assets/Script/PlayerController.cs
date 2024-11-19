using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;//�ړ����x
    [SerializeField]
    float jumpForce = 5f;//�W�����v�̋���
    [SerializeField]
    int maxJumpCount = 2;//�W�����v�̍ő��

    private int jumpCount = 0;//���݂̃W�����v��
    private bool isGrounded = false;//�n�ʂɂ��Ă��邩�ǂ���
    private Rigidbody rb;//Rigidbody2D���g�p

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {//�܂������ɐi��
            transform.Translate(0.0f, 0.0f, Time.deltaTime);
        }
        //���ɐi��
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.0f, 0.0f, -Time.deltaTime);
        }

        //���������ɉ����Ĕ��]
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }
        //�W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Ground"))
       {
           isGrounded = true;
           jumpCount = 0;
       }
        
    }
    void OnCollisionExit(Collision collision)
    {
       if (collision.gameObject.CompareTag("Ground"))
       {
           isGrounded = false;
       }
            
    }
}