using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;//移動速度
    [SerializeField]
    float jumpForce = 5f;//ジャンプの強さ
    [SerializeField]
    int maxJumpCount = 2;//ジャンプの最大回数

    private int jumpCount = 0;//現在のジャンプ回数
    private bool isGrounded = false;//地面についているかどうか
    private Rigidbody rb;//Rigidbody2Dを使用

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //右
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Time.deltaTime, 0.0f, 0.0f);
        }
        //左
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