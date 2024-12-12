using Game.GameSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage.Player 
{
    /// <summary>
    /// �v���C���[�̓���Ǘ��N���X(�A�j���[�V�����₻�̂ق��̏����͕ʂ̏ꏊ�ōs��)
    /// </summary>
    public class PlayerMoveController : MonoBehaviour
    {
        // �ő呬�x
        private const float MAX_SPEED = 120.0f;
        // �ŏ����x
        private const float MIN_SPEED = 0.0f;
        // �����x
        private const float ADD_SPEED = 30.0f;
        // �����x
        private const float SUB_SPEED = 60.0f;

        // ���݂̑��x
        private float currentSpeed = 0.0f;

        // �v���C���[�̓���t���O
        private bool isActive = true;

        // �v���C���[��Rigidbody
        private Rigidbody rb;

        // �����x�N�g��
        private Vector3 moveDir = Vector3.zero;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void FixedUpdate()
        {
            if (!isActive) return;

            if (currentSpeed == MAX_SPEED)
            {
                rb.velocity = Vector3.zero;
            }

            if (Input.GetKey(KeyCode.D) && transform.position.x < GameConstants.TopRight.x)
            {
                moveDir = new Vector3(Acceleration(), moveDir.y, moveDir.z);
            }
            else if (Input.GetKey(KeyCode.A) && transform.position.x > GameConstants.LowerLeft.x)
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
        /// ��������
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
        /// ��������
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