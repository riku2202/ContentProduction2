using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CharacterControllerScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;       // キャラクターの移動速度
    [SerializeField] float rotationSpeed = 700f; // キャラクターの回転速度
    [SerializeField] float jumpForce = 5f;       // ジャンプの強さ

    Rigidbody rb;            // キャラクターのリジッドボディ
    bool isGrounded = true;   // 地面に接地しているかどうかのフラグ

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // プレイヤー入力の取得
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 移動方向を計算
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // キャラクターが移動方向に進む
        if (movement.magnitude > 0)
        {
            // 移動処理
            rb.MovePosition(transform.position + (moveSpeed * Time.deltaTime * movement));

            // キャラクターが移動方向に回転するようにする
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }

        // ジャンプ処理 (バツボタン)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;  // ジャンプ中は地面についていないとみなす
        }
    }

    // 地面に接触したときに呼ばれる
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // 地面に触れたらisGroundedをtrueに
        }
    }

    // 地面に接触している間呼ばれる
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // 地面にいる間isGroundedを常にtrueにする
        }
    }

    // 地面から離れたときに呼ばれる
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;  // 地面から離れたらisGroundedをfalseに
        }
    }
}