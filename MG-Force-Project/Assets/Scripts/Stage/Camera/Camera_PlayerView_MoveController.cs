using Game.GameSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage.Camera
{

    public class Camera_PlayerView_MoveController : MonoBehaviour
    {
        // カメラがプレイヤーを追尾するようにする
        // ただし以下の条件を満たした処理にすること

        // ・プレイヤーを必ずしも、画面の同じ場所に移す必要はない(プレイヤーは絶対に映す)
        // ・プレイヤーが画面の中心ではなく少し下にいるようにする
        // ・カメラがステージ外を映さないようにする
        // ・プレイヤーの動作を直接渡さず、画面があまり揺れないようにする
        // ・Z座標は固定で-10、Cameraコンポーネントの設定は変えないようにする

        private Transform Player;

        private Vector3 LowerLeft = GameConstants.LowerLeftCamera;

        private Vector3 TopRight;

        private Vector3 LastPlayerPos;

        [SerializeField]
        private float followSpeed = 5.0f;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            Physics.gravity = new Vector3(0, -1.0f, 0);

            StageLoader stage_loader = GameObject.Find("StageLoader").GetComponent<StageLoader>();

            GameDataManager gamedata_manager = GameDataManager.Instance;

            TopRight = stage_loader.GetStageData(gamedata_manager.GetCurrentStageIndex()).TopRight;
            TopRight.z = -10;

            transform.position = LowerLeft;

            //LastPlayerPos = Player.position;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            GameObject p = GameObject.Find("PlayerPrefab");
            if (p == null) return;

            Player = GameObject.Find("playerPrefab").GetComponent<Transform>();

            if (Player == null) return;

            if (Player.position != LastPlayerPos)
            {
                FollowPlayer();
                LastPlayerPos = Player.position;
            }
        }

        private void FollowPlayer()
        {
            float targetX = Mathf.Clamp(Player.position.x, LowerLeft.x, TopRight.x);
            float targetY = Mathf.Clamp(Player.position.y, LowerLeft.y, TopRight.y);

            Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}