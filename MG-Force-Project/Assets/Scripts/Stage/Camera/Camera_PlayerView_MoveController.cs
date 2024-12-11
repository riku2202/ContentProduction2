using Game.GameSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage.Camera
{

    public class CameraPlayerViewMoveController : MonoBehaviour
    {
        /*
        カメラがプレイヤーを追尾するようにする
        ただし以下の条件を満たした処理にすること
         ・プレイヤーを必ずしも、画面の同じ場所に移す必要はない(プレイヤーは絶対に映す)
         ・プレイヤーが画面の中心ではなく少し下にいるようにする
         ・カメラがステージ外を映さないようにする
         ・プレイヤーの動作を直接渡さず、画面があまり揺れないようにする
         ・Z座標は固定で-10、Cameraコンポーネントの設定は変えないようにする
        */

        // 左下の頂点座標
        private Vector3 lowerLeft = GameConstants.LowerLeftCamera;

        // 右上の頂点座標
        private Vector3 topRight = GameConstants.TopRightCamera;

        // 現在のプレイヤーの位置情報
        private Transform currentPlayerPos;

        // プレイヤーへの追尾スピード
        [SerializeField]
        private float followSpeed = 5.0f;

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
            float targetX = Mathf.Clamp(currentPlayerPos.position.x, lowerLeft.x, topRight.x);
            float targetY = Mathf.Clamp(currentPlayerPos.position.y, lowerLeft.y, topRight.y);

            Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}