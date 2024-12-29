using UnityEngine;

namespace Game.Stage.Player 
{
    /// <summary>
    /// プレイヤーの動作管理クラス(アニメーションやそのほかの処理は別の場所で行う)
    /// </summary>
    public class PlayerMoveController : MonoBehaviour
    {
        private GameSystem.InputHandler _input;
        
        // 最大速度
        private const float MAX_SPEED = 120.0f;
        // 最小速度
        private const float MIN_SPEED = 0.0f;
        // 加速度
        private const float ADD_SPEED = 30.0f;
        // 減速度
        private const float SUB_SPEED = 60.0f;

        // ジャンプ力
        private const float JUMP_POWER = 1.0f;

        // 現在の速度
        private float currentSpeed = 0.0f;

        // プレイヤーのRigidbody
        private Rigidbody rb;

        // プレイヤーの動作フラグ
        private bool isActive = true;

        [SerializeField]
        // 地面に設置しているかどうか
        private bool isGranded = true;

        // 向きベクトル
        private Vector3 moveDir = Vector3.zero;

        private Vector3 raycastDir = new Vector3(0.0f, -1.0f, 0.0f);

        private const float RAYCAST_LENGTH = 0.1f;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            _input = GameObject.Find("InputManager").GetComponent<GameSystem.InputHandler>();

            rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void FixedUpdate()
        {
            // 生存チェック
            if (!isActive) return;

            if (!isGranded)
            {
                CheckGranded();
            }

            // 最大速度中は既存の移動をリセットする
            if (currentSpeed == MAX_SPEED)
            {
                rb.velocity = new Vector3(MIN_SPEED, rb.velocity.y, MIN_SPEED);
            }

            // 右移動
            if (_input.IsActionPressing(GameConstants.Input.Action.RIGHTMOVE) && transform.position.x < GameConstants.TopRight.x)
            {
                moveDir = new Vector3(Acceleration(), moveDir.y, moveDir.z);
            }
            // 左移動
            else if (_input.IsActionPressing(GameConstants.Input.Action.LEFTMOVE) && transform.position.x > GameConstants.LowerLeft.x)
            {
                moveDir = new Vector3(-Acceleration(), moveDir.y, moveDir.z);
            }
            // 停止
            else
            {
                moveDir = new Vector3(Deceleration(), rb.velocity.y, rb.velocity.z);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGranded)
            {
                moveDir = new Vector3(moveDir.x, JUMP_POWER, moveDir.z);
                isGranded = false;
            }

            // 力を加える
            rb.AddForce(moveDir, ForceMode.Force);

            // 初期地移動(デバック用)
            if (Input.GetKeyDown(KeyCode.Q))
            {
                transform.position = Vector3.zero;
                rb.velocity = Vector3.zero;
            }
        }

        private void CheckGranded()
        {
            isGranded =  Physics.Raycast(gameObject.transform.position, raycastDir, RAYCAST_LENGTH);
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