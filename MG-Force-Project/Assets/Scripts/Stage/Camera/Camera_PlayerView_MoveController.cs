using Game.GameSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage.Camera
{

    public class CameraPlayerViewMoveController : MonoBehaviour
    {
        // 左下の頂点座標
        private Vector3 lowerLeft = GameConstants.LowerLeftCamera;

        // 右上の頂点座標
        private Vector3 topRight = GameConstants.TopRightCamera;

        // 現在のプレイヤーの位置情報
        private Transform currentPlayerPos;

        // プレイヤーへの追尾スピード
        [SerializeField]
        private float followSpeed = 5.0f;

        //// カメラのY座標のオフセット
        //private const float CAMERA_Y_OFFSET = -1.0f;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            currentPlayerPos = GameObject.Find(GameConstants.PLAYER_OBJ).GetComponent<Transform>();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            // nullチェック
            if (currentPlayerPos == null) return;

            // プレイヤーを追尾
            TrackThePlayer();
        }

        /// <summary>
        /// プレイヤーを追尾
        /// </summary>
        private void TrackThePlayer()
        {
            float target_x = Mathf.Clamp(currentPlayerPos.position.x, lowerLeft.x, topRight.x);
            float target_y = Mathf.Clamp(currentPlayerPos.position.y, lowerLeft.y, topRight.y);

            Vector3 target_pos = new Vector3(target_x, target_y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, target_pos, followSpeed * Time.deltaTime);
        }
    }
}