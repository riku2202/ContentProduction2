using UnityEngine;

namespace Game.StageScene.Player 
{
    /// <summary>
    /// プレイヤーの動作管理クラス(アニメーションやそのほかの処理は別の場所で行う)
    /// </summary>
    public class PlayerMoveController : PlayerControllerBase
    {
        #region -------- Move 定数 --------

        // 最大速度
        private const float MAX_SPEED = 120.0f;
        // 最小速度
        private const float MIN_SPEED = 0.0f;
        // 加速度
        private const float ADD_SPEED = 30.0f;
        // 減速度
        private const float SUB_SPEED = 60.0f;

        #endregion

        #region -------- Jump 定数 --------

        // ジャンプ力
        private const float JUMP_POWER = 1.0f;

        private const float RAYCAST_LENGTH = 0.1f;

        #endregion

        private Rigidbody _rigidbody;

        private bool _isGrounded;

        // 現在の速度
        private float _currentSpeed;

        // 向きベクトル
        private Vector3 moveDir = Vector3.zero;

        // 地面判定用
        private Vector3 raycastDir = new Vector3(0.0f, -1.0f, 0.0f);

        public override void Init()
        {
            _rigidbody = playerObject.GetComponent<Rigidbody>();

            _currentSpeed = MIN_SPEED;
            _isGrounded = true;
        }

        public override void Update()
        {
            if ((currentState & State.STILLNESS) != (int)State.NOT_STATE) { StillnessUpdate(); }
            
            if ((currentState & State.RUN) != (int)State.NOT_STATE) { RunUpdate(); }
            
            if ((currentState & State.JUMP) != (int)State.NOT_STATE)
            {
                if (_isGrounded)
                {
                    JumpStart();
                }
                else
                {
                    JumpUpdate();
                }
            }

            // 力を加える
            _rigidbody.AddForce(moveDir, ForceMode.Force);
        }

        private void StillnessUpdate()
        {
            moveDir = new Vector3(Deceleration(), _rigidbody.velocity.y, _rigidbody.velocity.z);
        }

        private void RunUpdate()
        {
            // 最大速度中は既存の移動をリセットする
            if (_currentSpeed == MAX_SPEED)
            {
                _rigidbody.velocity = new Vector3(MIN_SPEED, _rigidbody.velocity.y, MIN_SPEED);
            }

            if (currentDir == Direction.RIGHT)
            {
                moveDir = new Vector3(Acceleration(), moveDir.y, moveDir.z);
            }
            else
            {
                moveDir = new Vector3(-Acceleration(), moveDir.y, moveDir.z);
            }
        }

        public void JumpStart()
        {
            moveDir = new Vector3(moveDir.x, JUMP_POWER, moveDir.z);

            _isGrounded = false;
        }

        private void JumpUpdate()
        {
            bool hit_raycast = Physics.Raycast(playerTransform.position, raycastDir, RAYCAST_LENGTH);

            if (hit_raycast)
            {
                _isGrounded = true;

                currentState = currentState & ~State.JUMP;
            }
        }

        /// <summary>
        /// 加速処理
        /// </summary>
        /// <returns></returns>
        private float Acceleration()
        {
            if (_currentSpeed < MAX_SPEED)
            {
                _currentSpeed += ADD_SPEED;

                if (_currentSpeed > MAX_SPEED)
                {
                    _currentSpeed = MAX_SPEED;
                }

                return _currentSpeed * Time.deltaTime;
            }

            return _currentSpeed * Time.deltaTime;
        }

        /// <summary>
        /// 減速処理
        /// </summary>
        /// <returns></returns>
        private float Deceleration()
        {
            if (_currentSpeed > MIN_SPEED)
            {
                _currentSpeed -= SUB_SPEED;

                if (_currentSpeed < MIN_SPEED)
                {
                    _currentSpeed = MIN_SPEED;
                }

                return _currentSpeed * Time.deltaTime;
            }

            return _currentSpeed * Time.deltaTime;
        }
    }
}