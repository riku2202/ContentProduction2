using UnityEngine;

namespace Game.StageScene.Player 
{
    /// <summary>
    /// �v���C���[�̓���Ǘ��N���X(�A�j���[�V�����₻�̂ق��̏����͕ʂ̏ꏊ�ōs��)
    /// </summary>
    public class PlayerMoveController : PlayerControllerBase
    {
        #region -------- Move �萔 --------

        private const float MAX_SPEED = 3.5f;
        // ���x
        private const float MOVE_SPEED = 0.25f;
        // �ŏ����x
        private const float MIN_SPEED = 0.0f;

        #endregion

        #region -------- Jump �萔 --------

        private const float JUMP_FORCE = 5.0f;

        private const float RAYCAST_LENGTH = 0.1f;

        private const float GRAVITY_SCALE = 1.25f;

        #endregion

        private Rigidbody _rigidbody;

        // �����x�N�g��
        private Vector3 moveDir = Vector3.zero;
        // �n�ʔ���p
        private Vector3 raycastDir = Vector3.down;
        
        public override void OnStart()
        {
            _rigidbody = playerObject.GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            isGrounded = false;
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        public override void OnUpdate()
        {
            // �n�ʐڒn����
            CheckGrounded();

            // ���E�ړ����̏���
            if ((currentState & State.RUN) != (int)State.NOT_STATE)
            {
                RunUpdate();
            }
            else
            {
                StopMoving();
            }

            // �W�����v���̏���
            if ((currentState & State.JUMP) != (int)State.NOT_STATE)
            {
                JumpStart();
            }
            else if (isGrounded)
            {
                moveDir.y = MIN_SPEED;
            }

            // �e��������
            if ((currentState & State.SHOOT) != (int)State.NOT_STATE) StopMoving();

            // �d�͍X�V
            if (!isGrounded)
            {
                GravityUpdate();
            }

            // Rigidbody�̍X�V
            _rigidbody.velocity = moveDir;
        }

        private void StopMoving()
        {
            moveDir = new Vector3(MIN_SPEED, _rigidbody.velocity.y, MIN_SPEED);
        }

        private void RunUpdate()
        {
            if (currentDir == Direction.RIGHT)
            {
                moveDir.x += MOVE_SPEED;

                if (moveDir.x > MAX_SPEED)
                {
                    moveDir.x = MAX_SPEED;
                }
            }
            else
            {
                moveDir.x -= MOVE_SPEED;

                if (moveDir.x < -MAX_SPEED)
                {
                    moveDir.x = -MAX_SPEED;
                }
            }
        }

        private void JumpStart()
        {
            if (isGrounded)
            {
                moveDir.y = JUMP_FORCE;
                isGrounded = false;
            }
        }

        /// <summary>
        /// �n�ʐڒn����
        /// </summary>
        private void CheckGrounded()
        {
            float offset_x = 0.45f;
            float offset_y = 0.1f;

            Vector3 left_ray_start_position = new Vector3(playerTransform.position.x - offset_x, playerTransform.position.y + offset_y, playerTransform.position.z);
            Vector3 right_ray_start_position = new Vector3(playerTransform.position.x + offset_x, playerTransform.position.y + offset_y, playerTransform.position.z);

            // �n�ʂɐڒn���Ă��邩�̔���
            if (Physics.Raycast(left_ray_start_position, raycastDir, out RaycastHit left_hit, RAYCAST_LENGTH))
            {
                if (left_hit.collider.CompareTag(GameConstants.Tag.UNTAGGED) ||
                    left_hit.collider.gameObject.layer == (int)GameConstants.Layer.MAGNET_RANGE) return;

                if (left_hit.collider.isTrigger != true)
                {
                    isGrounded = true;
                }
            }
            else if (Physics.Raycast(right_ray_start_position, raycastDir, out RaycastHit right_hit, RAYCAST_LENGTH))
            {
                if (right_hit.collider.CompareTag(GameConstants.Tag.UNTAGGED) ||
                    right_hit.collider.gameObject.layer == (int)GameConstants.Layer.MAGNET_RANGE) return;
                
                if (right_hit.collider.isTrigger != true)
                {
                    isGrounded = true;
                }
            }
            else
            {
                isGrounded = false;
            }

#if UNITY_EDITOR
            Debug.DrawRay(left_ray_start_position, raycastDir * RAYCAST_LENGTH, Color.red);
            Debug.DrawRay(right_ray_start_position, raycastDir * RAYCAST_LENGTH, Color.red);
#endif
        }

        /// <summary>
        /// �d�͂̍X�V����
        /// </summary>
        private void GravityUpdate()
        {
            moveDir.y += Physics.gravity.y * GRAVITY_SCALE * Time.deltaTime;
        }
    }
}