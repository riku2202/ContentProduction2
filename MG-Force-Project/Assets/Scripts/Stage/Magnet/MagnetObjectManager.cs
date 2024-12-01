using UnityEditor;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// オブジェクトの磁力管理クラス
    /// </summary>
    public class MagnetObjectManager : MonoBehaviour
    {
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

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
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
            }

            magnetController = new MagnetController();

            DebugManager.LogMessage(MyData.MyObjectType.ToString());
        }

        private void Update()
        {
            if (magnetManager.IsMagnetBoot && Magnet.activeSelf != true)
            {
                Magnet.SetActive(true);
            }
            else if (!magnetManager.IsMagnetBoot && Magnet.activeSelf != false)
            {
                Magnet.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (MagnetFixed) { return; }

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
            if (other == null || MagnetFixed) { return; }

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

        /// <summary>
        /// 当たっている時の処理
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay(Collider other)
        {
            // nullチェック
            if (other == null) { return; }

            // 例外チェック
            if (other.gameObject.layer != (int)GameConstants.Layer.N_MAGNET && 
                other.gameObject.layer != (int)GameConstants.Layer.S_MAGNET) { return; }

            // このオブジェクトが可動オブジェクトの場合
            if (MyData.MyObjectType == MagnetData.ObjectType.Moving)
            {
                // 磁力の動作処理
                magnetController.MagnetUpdate(gameObject, other.gameObject);

                //if (Mathf.Abs(gameObject.transform.position.x - parentTransform.position.x) > 0.01f ||
                //      Mathf.Abs(gameObject.transform.position.y - parentTransform.position.y) > 0.01f)
                //{
                //}
            }
        }
    }
}