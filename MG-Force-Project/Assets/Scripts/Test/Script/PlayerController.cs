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
        //�E
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Time.deltaTime, 0.0f, 0.0f);
        }
        //��
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Time.deltaTime, 0.0f, 0.0f);
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