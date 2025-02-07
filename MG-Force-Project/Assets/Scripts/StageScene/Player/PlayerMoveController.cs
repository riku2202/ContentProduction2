using UnityEngine;

namespace Game.StageScene.Player 
{
    /// <summary>
    /// プレイヤーの動作管理クラス(アニメーションやそのほかの処理は別の場所で行う)
    /// </summary>
    public class PlayerMoveController : PlayerControllerBase
    {
        #region -------- Move 定数 --------

        // 速度
        private const float MOVE_SPEED = 10.0f;
        // 最小速度
        private const float MIN_SPEED = 0.0f;

        #endregion

        #region -------- Jump 定数 --------

        private const float RAYCAST_LENGTH = 0.825f;

        #endregion

        private Rigidbody _rigidbody;

        // 向きベクトル
        private Vector3 moveDir = Vector3.zero;
        // 地面判定用
        private Vector3 raycastDir = new Vector3(0.0f, -0.825f, 0.0f);

        // 地面に当たっているかのフラグ
        private bool _isGrounded;

        public override void Init()
        {
            _rigidbody = playerObject.GetComponent<Rigidbody>();

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
                    currentState = currentState & ~State.JUMP;
                }
            }

<<<<<<< HEAD
=======
#if false
            DebugManager.LogMessage($"{_isGrounded}");
            Debug.DrawRay(GameObject.Find("hip").transform.position, raycastDir, Color.red, RAYCAST_LENGTH);
#endif
            // @yu-kirohi
            // 多分JumpUpdate内でやってることと同じだと思う
            // あとUpdate内でFind使ってるのは重くなる要因かも
>>>>>>> origin/main
            _isGrounded = Physics.Raycast(GameObject.Find("hip").transform.position, raycastDir, RAYCAST_LENGTH);

            _rigidbody.velocity = moveDir;
        }

        private void StillnessUpdate()
        {
            moveDir = new Vector3(MIN_SPEED, _rigidbody.velocity.y, MIN_SPEED);
        }

        private void RunUpdate()
        {
            if (currentDir == Direction.RIGHT)
            {
                moveDir.x = MOVE_SPEED;
            }
            else
            {
                moveDir.x = -MOVE_SPEED;
            }
        }

        private void JumpStart()
        {
            _isGrounded = false;
        }
    }
}