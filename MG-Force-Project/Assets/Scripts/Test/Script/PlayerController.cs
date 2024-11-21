//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    [SerializeField] float jumpForce = 5f;
//    private bool isGrounded = true;
//    private Rigidbody rb;
//    // Start is called before the first frame update
//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //まっすぐに進む
//        if (Input.GetKey(KeyCode.RightArrow))
//        {
//            transform.Translate(0.0f, 0.0f, Time.deltaTime);
//        }
//        //後ろに進む
//        if (Input.GetKey(KeyCode.LeftArrow))
//        {
//            transform.Translate(0.0f, 0.0f, -Time.deltaTime);
//        }
//        //ジャンプ
//        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//        {
//            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//            isGrounded = false;
//        }
//        float rotate_speed = 10f;

//        if (Input.GetKey(KeyCode.DownArrow))//右回転
//        {
//            transform.Rotate(0.0f, rotate_speed * Time.deltaTime, 0.0f);
//        }

//        if (Input.GetKey(KeyCode.UpArrow))//左回転
//        {
//            transform.Rotate(0.0f, -rotate_speed * Time.deltaTime, 0.0f);
//        }
//    }
//    void OnCollisionEnter(Collision other)
//    {
//        if (other.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = true;
//        }
//    }
//}