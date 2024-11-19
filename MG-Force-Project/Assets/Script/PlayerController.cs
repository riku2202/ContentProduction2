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
        if (Input.GetKey(KeyCode.A))
        {//まっすぐに進む
            transform.Translate(0.0f, 0.0f, Time.deltaTime);
        }
        //後ろに進む
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.0f, 0.0f, -Time.deltaTime);
        }

        //動く方向に応じて反転
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }
        //ジャンプ処理
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