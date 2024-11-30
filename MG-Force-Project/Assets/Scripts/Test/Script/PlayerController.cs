using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] float speed = 5f;
    [SerializeField] float muoseSensitvity = 2f;
    [SerializeField] float jumpForce = 5f;

    private float veticalRotation = 0f;
    private bool isGrounded = true;
    private Rigidbody rb;
    // Start is called before the first frame update
=======
    [SerializeField]
    float moveSpeed = 5f;//移動速度
    [SerializeField]
    float jumpForce = 5f;//ジャンプの強さ
    
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    float groundCheckRadius = 0.2f;

   
    private bool isGrounded = false;//地面についているかどうか
    private Rigidbody rb;//Rigidbody2Dを使用

>>>>>>> 270ba7cecbb94de8f29a654e985b22da7bcefcc4
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
<<<<<<< HEAD
        //まっすぐに進む
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.0f, 0.0f, Time.deltaTime);
        }
        //後ろに進む
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(0.0f, 0.0f, -Time.deltaTime);
        }
        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.up * jumpForce;


            float mouseX = Input.GetAxis("Mouse X") * muoseSensitvity;
            transform.Rotate(0, mouseX, 0);

            float mouseY = Input.GetAxis("Mouse Y") * muoseSensitvity;
            veticalRotation -= mouseY;
            veticalRotation = Mathf.Clamp(veticalRotation, -90f, 90f);
            Camera.main.transform.localRotation = Quaternion.Euler(veticalRotation, 0, 0);
=======
        float moveX = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveX, 0, 0);
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, rb.velocity.z);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        //ジャンプ処理
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
>>>>>>> 270ba7cecbb94de8f29a654e985b22da7bcefcc4
        }
    }

}