using UnityEngine;

namespace Game.Stage.Player 
{
    /// <summary>
    /// プレイヤーの動作管理クラス(アニメーションやそのほかの処理は別の場所で行う)
    /// </summary>
    public class PlayerMoveController : PlayerControllerBase
    {
        public PlayerMoveController(Rigidbody rigidbody)
        {
            this.rigidbody = rigidbody;
        }

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

        // 現在の速度
        private float _currentSpeed;

        // 向きベクトル
        private Vector3 moveDir = Vector3.zero;

        // 地面判定用
        private Vector3 raycastDir = new Vector3(0.0f, -1.0f, 0.0f);

        public void Init()
        {
            _currentSpeed = MIN_SPEED;
            isLeftState = false;
            isGranded = true;
        }

        public void Update()
        {
            DebugManager.LogMessage($"{currentState}");

            switch (currentState)
            {
                case State.STILLNESS:
                    StillnessUpdate();
                    break;

                case State.RUN:
                    RunUpdate();
                    break;

                case State.JUMP:
                    JumpStart();
                    break;
            }

            // 力を加える
            rigidbody.AddForce(moveDir, ForceMode.Force);

            if (!isGranded) { JumpUpdate(); }
        }

        private void StillnessUpdate()
        {
            moveDir = new Vector3(Deceleration(), rigidbody.velocity.y, rigidbody.velocity.z);
        }

        private void RunUpdate()
        {
            // 最大速度中は既存の移動をリセットする
            if (_currentSpeed == MAX_SPEED)
            {
                rigidbody.velocity = new Vector3(MIN_SPEED, rigidbody.velocity.y, MIN_SPEED);
            }

            if (!isLeftState)
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

            currentState = State.STILLNESS;
            isGranded = false;
        }

        private void JumpUpdate()
        {
            bool hit_raycast = Physics.Raycast(rigidbody.transform.position, raycastDir, RAYCAST_LENGTH);

            if (hit_raycast) { currentState = State.STILLNESS; }
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

                return _currentSpeed;
            }

            return _currentSpeed;
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

                return _currentSpeed;
            }

            return _currentSpeed;
        }
    }
}