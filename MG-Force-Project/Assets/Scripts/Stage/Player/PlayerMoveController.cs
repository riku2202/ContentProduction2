using Game.GameSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage.Player 
{
    /// <summary>
    /// プレイヤーの動作管理クラス(アニメーションやそのほかの処理は別の場所で行う)
    /// </summary>
    public class PlayerMoveController : MonoBehaviour
    {
        // 最大速度
        private const float MAX_SPEED = 8.5f;
        // 最小速度
        private const float MIN_SPEED = 0.0f;
        // 加速度
        private const float ADD_SPEED = 1.25f;
        // 減速度
        private const float SUB_SPEED = 4.0f;

        // 現在の速度
        private float currentSpeed = 0.0f;

        // プレイヤーの動作フラグ
        private bool isActive = true;

        // プレイヤーのRigidbody
        private Rigidbody rb;

        // 向きベクトル
        private Vector3 moveDir = Vector3.zero;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void FixedUpdate()
        {
            if (!isActive) return;

            if (Input.GetKey(KeyCode.D))
            {
                moveDir = new Vector3(Acceleration(), moveDir.y, moveDir.z);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                moveDir = new Vector3(-Acceleration(), moveDir.y, moveDir.z);
            }
            else
            {
                moveDir = new Vector3(Deceleration(), rb.velocity.y, rb.velocity.z);
            }

            rb.AddForce(moveDir, ForceMode.Force);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.position = Vector3.zero;
                rb.velocity = Vector3.zero;
            }
        }

        /// <summary>
        /// 加速処理
        /// </summary>
        /// <returns></returns>
        private float Acceleration()
        {
            if (currentSpeed < MAX_SPEED)
            {
                currentSpeed += ADD_SPEED;

                if (currentSpeed > MAX_SPEED)
                {
                    currentSpeed = MAX_SPEED;
                }

                return currentSpeed;
            }

            return currentSpeed;
        }

        /// <summary>
        /// 減速処理
        /// </summary>
        /// <returns></returns>
        private float Deceleration()
        {
            if (currentSpeed > MIN_SPEED)
            {
                currentSpeed -= SUB_SPEED;

                if (currentSpeed < MIN_SPEED)
                {
                    currentSpeed = MIN_SPEED;
                }

                return currentSpeed;
            }

            return currentSpeed;
        }
    }
}