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

        private const float GRAVITY_SCALE = 0.98f;

        #endregion

        private Rigidbody _rigidbody;

        // 向きベクトル
        private Vector3 moveDir = Vector3.zero;
        // 地面判定用
        private Vector3 raycastDir = new Vector3(0.0f, -1.0f, 0.0f);

        public override void OnStart()
        {
            _rigidbody = playerObject.GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public override void OnUpdate()
        {
            // 地面接地判定
            CheckGrounded();

            // 停止時の処理
            if ((currentState & State.STILLNESS) != (int)State.NOT_STATE) StopMoving();

            // 左右移動時の処理
            if ((currentState & State.RUN) != (int)State.NOT_STATE) RunUpdate();

            // ジャンプ時の処理
            if ((currentState & State.JUMP) != (int)State.NOT_STATE) JumpUpdate();

            // 弾を撃つ処理
            if ((currentState & State.SHOOT) != (int)State.NOT_STATE) StopMoving();

            // 重力更新
            if (!isGrounded)
            {
                GravityUpdate();
            }
            else if (moveDir.y != JUMP_FORCE)
            {
                moveDir.y = MIN_SPEED;
            }

            Debug.Log($"{moveDir}");

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

        private void JumpUpdate()
        {
            if (isGrounded)
            {
                moveDir.y = JUMP_FORCE;
            }
        }

        /// <summary>
        /// 地面接地判定
        /// </summary>
        private void CheckGrounded()
        {
            float offset = 0.1f;

            Vector3 ray_start_position = playerTransform.position;

            ray_start_position.y += offset;

            // 地面に接地しているかの判定
            isGrounded = Physics.Raycast(ray_start_position, raycastDir, RAYCAST_LENGTH);

#if UNITY_EDITOR
            Debug.DrawRay(playerTransform.position, raycastDir * RAYCAST_LENGTH, Color.red);
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