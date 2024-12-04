using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControll : MonoBehaviour
{
    private const float JumpForce = 5f;        // ジャンプ力 (固定値として定義)
    private const float GroundCheckDistance = 0.1f; // 地面判定の距離
    private Rigidbody _rigidbody;             // Rigidbodyの参照
    private bool _isGrounded;                 // 地面に接触しているか

    private void Awake()
    {
        // Rigidbodyコンポーネントの取得
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 地面判定
        _isGrounded = IsGrounded();

        // 入力処理
        HandleInput();
    }

    private void HandleInput()
    {
        // ジャンプ処理
        if (_isGrounded && InputManager.Instance.IsActionPressed("Jump"))
        {
            Jump();
        }

        // 
        // 攻撃処理
        if (InputManager.Instance.IsActionPressed("Shoot"))
        {
            Shoot();
        }
    }

    private void Jump()
    {
        // 垂直方向の速度をジャンプ力に設定
        var velocity = _rigidbody.velocity;
        velocity.y = JumpForce;
        _rigidbody.velocity = velocity;
    }

    private void Shoot()
    {
        Debug.Log("Shoot executed!");
    }

    private bool IsGrounded()
    {
        // 地面との距離をチェック
        return Physics.Raycast(transform.position, Vector3.down, GroundCheckDistance);
    }
}