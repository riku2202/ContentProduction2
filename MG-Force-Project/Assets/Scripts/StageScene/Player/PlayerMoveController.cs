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

        private const float GRAVITY_SCALE = 0.98f;

        #endregion

        private Rigidbody _rigidbody;

        // �����x�N�g��
        private Vector3 moveDir = Vector3.zero;
        // �n�ʔ���p
        private Vector3 raycastDir = new Vector3(0.0f, -1.0f, 0.0f);

        public override void OnStart()
        {
            _rigidbody = playerObject.GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        public override void OnUpdate()
        {
            // �n�ʐڒn����
            CheckGrounded();

            // ��~���̏���
            if ((currentState & State.STILLNESS) != (int)State.NOT_STATE) StopMoving();

            // ���E�ړ����̏���
            if ((currentState & State.RUN) != (int)State.NOT_STATE) RunUpdate();

            // �W�����v���̏���
            if ((currentState & State.JUMP) != (int)State.NOT_STATE) JumpUpdate();

            // �e��������
            if ((currentState & State.SHOOT) != (int)State.NOT_STATE) StopMoving();

            // �d�͍X�V
            if (!isGrounded)
            {
                GravityUpdate();
            }
            else if (moveDir.y != JUMP_FORCE)
            {
                moveDir.y = MIN_SPEED;
            }

            Debug.Log($"{moveDir}");

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

        private void JumpUpdate()
        {
            if (isGrounded)
            {
                moveDir.y = JUMP_FORCE;
            }
        }

        /// <summary>
        /// �n�ʐڒn����
        /// </summary>
        private void CheckGrounded()
        {
            float offset = 0.1f;

            Vector3 ray_start_position = playerTransform.position;

            ray_start_position.y += offset;

            // �n�ʂɐڒn���Ă��邩�̔���
            isGrounded = Physics.Raycast(ray_start_position, raycastDir, RAYCAST_LENGTH);

#if UNITY_EDITOR
            Debug.DrawRay(playerTransform.position, raycastDir * RAYCAST_LENGTH, Color.red);
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