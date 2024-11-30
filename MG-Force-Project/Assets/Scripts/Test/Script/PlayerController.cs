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

>>>>>>> 270ba7cecbb94de8f29a654e985b22da7bcefcc4
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
<<<<<<< HEAD
        //�܂������ɐi��
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.0f, 0.0f, Time.deltaTime);
        }
        //���ɐi��
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(0.0f, 0.0f, -Time.deltaTime);
        }
        //�W�����v
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
>>>>>>> 270ba7cecbb94de8f29a654e985b22da7bcefcc4
        }
    }

}