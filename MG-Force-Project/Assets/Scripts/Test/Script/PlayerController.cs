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
    LayerMask groundLayer;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    float groundCheckRadius = 0.2f;

   
    private bool isGrounded = false;//�n�ʂɂ��Ă��邩�ǂ���
    private Rigidbody rb;//Rigidbody2D���g�p

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveX, 0, 0);
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, rb.velocity.z);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        //�W�����v����
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if(groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

}