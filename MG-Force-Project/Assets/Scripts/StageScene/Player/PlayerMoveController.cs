using UnityEngine;

namespace Game.StageScene.Player 
{
    /// <summary>
    /// プレイヤーの動作管理クラス(アニメーションやそのほかの処理は別の場所で行う)
    /// </summary>
    public class PlayerMoveController : PlayerControllerBase
    {
        #region -------- Move 定数 --------

        private const float MAX_SPEED = 3.5f;
        // 速度
        private const float MOVE_SPEED = 0.25f;
        // 最小速度
        private const float MIN_SPEED = 0.0f;

        #endregion

        #region -------- Jump 定数 --------

        private const float JUMP_FORCE = 5.0f;

        private const float RAYCAST_LENGTH = 0.1f;

        private const float GRAVITY_SCALE = 1.25f;

        #endregion

        private Rigidbody _rigidbody;

        // 向きベクトル
        private Vector3 moveDir = Vector3.zero;
        // 地面判定用
        private Vector3 raycastDir = Vector3.down;
        
        public override void OnStart()
        {
            _rigidbody = playerObject.GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            isGrounded = false;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public override void OnUpdate()
        {
            // 地面接地判定
            CheckGrounded();

            // 左右移動時の処理
            if ((currentState & State.RUN) != (int)State.NOT_STATE)
            {
                RunUpdate();
            }
            else
            {
                StopMoving();
            }

            // ジャンプ時の処理
            if ((currentState & State.JUMP) != (int)State.NOT_STATE)
            {
                JumpStart();
            }
            else if (isGrounded)
            {
                moveDir.y = MIN_SPEED;
            }

            // 弾を撃つ処理
            if ((currentState & State.SHOOT) != (int)State.NOT_STATE) StopMoving();

            // 重力更新
            if (!isGrounded)
            {
                GravityUpdate();
            }

            // Rigidbodyの更新
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
        /// 地面接地判定
        /// </summary>
        private void CheckGrounded()
        {
            float offset_x = 0.45f;
            float offset_y = 0.1f;

            Vector3 left_ray_start_position = new Vector3(playerTransform.position.x - offset_x, playerTransform.position.y + offset_y, playerTransform.position.z);
            Vector3 right_ray_start_position = new Vector3(playerTransform.position.x + offset_x, playerTransform.position.y + offset_y, playerTransform.position.z);

            // 地面に接地しているかの判定
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
        /// 重力の更新処理
        /// </summary>
        private void GravityUpdate()
        {
            moveDir.y += Physics.gravity.y * GRAVITY_SCALE * Time.deltaTime;
        }
    }
}