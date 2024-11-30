using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float muoseSensitvity = 2f;
    [SerializeField] float jumpForce = 5f;

    private float veticalRotation = 0f;
    private bool isGrounded = true;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }
}