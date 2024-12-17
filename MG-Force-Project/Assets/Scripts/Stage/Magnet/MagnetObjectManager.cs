using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// オブジェクトの磁力管理クラス
    /// </summary>
    public class MagnetObjectManager : MonoBehaviour
    {
        InputManager input;

        // 磁力データ
        public MagnetData MyData {  get; private set; }

        private MagnetManager magnetManager;
        private MagnetController magnetController;

        [SerializeField]
        private GameObject Magnet;

        [SerializeField]
        private bool MagnetFixed;

        [SerializeField]
        private MagnetData.MagnetPower MagnetFixedPower;

        private Rigidbody rigitbody;

        private bool canMove;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            //input = GameObject.Find(GameConstants.INPUT_MANAGER_OBJ).GetComponent<InputManager>();

            magnetManager = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();

            // Tag型に変換
            GameConstants.Tag tag = GameConstants.ConvertTag(gameObject.tag);

            // ObjectType型に変換
            MagnetData.ObjectType new_object_type = (MagnetData.ObjectType)tag;

            // コンストラクタの呼び出し
            if (MagnetFixed)
            {
                MagnetData.MagnetType new_magnet_type = (MagnetData.MagnetType)gameObject.layer;
                MyData = new MagnetData(new_object_type, new_magnet_type, MagnetFixedPower);
            }
            else
            {
                MyData = new MagnetData(new_object_type);

                if (MyData.MyObjectType == MagnetData.ObjectType.Moving)
                {
                    rigitbody = GetComponent<Rigidbody>();
                }
            }

            magnetController = new MagnetController();

            DebugManager.LogMessage(MyData.MyObjectType.ToString());
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            // 磁力の起動処理
            if (magnetManager.IsMagnetBoot && Magnet.activeSelf != true)
            {
                // 磁力の有効化
                Magnet.SetActive(true);
            }
            else if (!magnetManager.IsMagnetBoot && Magnet.activeSelf != false)
            {
                // 磁力の無効化
                Magnet.SetActive(false);
            }

            // 可動オブジェクト以外は除外
            if (MyData.MyObjectType != MagnetData.ObjectType.Moving) return;

            // 意図しない動作を防ぐ処理
            if (canMove)
            {
                SetDefultConstraints();
            }
            else
            {
                SetHitPlayerConstraints();
            }

            // 磁力固定オブジェクトは除外
            if (MagnetFixed) return;

            // 付与した磁力のリセット
            if (Input.GetKeyDown(KeyCode.R))
            {

                // 磁力データの宣言
                MagnetData.MagnetType reset_type = MagnetData.MagnetType.NotType;
                MagnetData.MagnetPower reset_power = MagnetData.MagnetPower.None;

                // レイヤーの更新
                gameObject.layer = (int)reset_type;

                // 磁力データの設定
                MyData.SetMagnetData(reset_type, reset_power);

                DebugManager.LogMessage("リセットしました");
            }
        }

        /// <summary>
        /// 当たった時の処理
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other == null || MagnetFixed) return;

            if (magnetManager.IsMagnetBoot)
            {
                // 例外チェック
                if (other.gameObject.layer != (int)GameConstants.Layer.N_MAGNET &&
                    other.gameObject.layer != (int)GameConstants.Layer.S_MAGNET) return;

                // このオブジェクトが可動オブジェクトの場合
                if (MyData.MyObjectType == MagnetData.ObjectType.Moving)
                {
                    // 磁力の動作処理
                    magnetController.MagnetUpdate(gameObject, other.gameObject);
                }
            }
            else
            {
                // 弾に当たった時
                if (other.gameObject.layer == (int)GameConstants.Layer.BULLET)
                {
                    // レイヤーの更新
                    gameObject.layer = (int)magnetManager.CurrentType;

                    // 磁力データの取得
                    MagnetData.MagnetType new_magnet_type = (MagnetData.MagnetType)gameObject.layer;
                    MagnetData.MagnetPower new_magnet_power = (MagnetData.MagnetPower)magnetManager.CurrentPower;

                    // 磁力データの設定
                    MyData.SetMagnetData(new_magnet_type, new_magnet_power);

                    DebugManager.LogMessage(MyData.MyMangetType.ToString() + " | " + MyData.MyMagnetPower.ToString());
                }
            }
        }

        /// <summary>
        /// オブジェクトに当たった時
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // 例外処理
            if (!collision.gameObject.CompareTag(GameConstants.Tag.Player.ToString()) || MagnetFixed) { return; }

            // プレイヤーと当たっている場合動かないようにする
            canMove = false;
        }

        /// <summary>
        /// オブジェクトから離れたとき
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionExit(Collision collision)
        {
            // 例外処理
            if (!collision.gameObject.CompareTag(GameConstants.Tag.Player.ToString()) || MagnetFixed) { return; }

            // プレイヤーが離れたときに動けるようにする
            canMove = true;
        }

        public void SetObjectPower(int power)
        {
            switch (power)
            {
                case (int)MagnetData.MagnetPower.Weak:
                    MagnetFixedPower = MagnetData.MagnetPower.Weak;
                    return;

                case (int)MagnetData.MagnetPower.Medium:
                    MagnetFixedPower = MagnetData.MagnetPower.Medium;
                    return;

                case (int)MagnetData.MagnetPower.Strong:
                    MagnetFixedPower = MagnetData.MagnetPower.Strong;
                    return;
            }
        }

        /// <summary>
        /// デフォルトの制約
        /// </summary>
        private void SetDefultConstraints()
        {
            rigitbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        /// <summary>
        /// プレイヤーと当たった時の制約
        /// </summary>
        private void SetHitPlayerConstraints()
        {
            rigitbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}