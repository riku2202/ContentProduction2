using Game.GameSystem;
using UnityEngine;

namespace Game.StageScene.Magnet
{
    /// <summary>
    /// オブジェクトの磁力管理クラス
    /// </summary>
    public class MagnetObjectManager : MonoBehaviour
    {
        private InputHandler input;

        // 磁力データ
        public MagnetData MyData {  get; protected set; }

        // 磁力管理クラス
        protected MagnetManager magnetManager;

        // 磁力動作管理クラス
        protected MagnetController magnetController;

        // 磁力のコライダー付きオブジェクト
        [SerializeField] private GameObject _magnetCollider;

        // 磁力の固定有無
        [SerializeField] protected bool magnetFixed;

        // 磁力の強さ
        [SerializeField] protected MagnetData.MagnetPower magnetFixedPower;

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected virtual void Start()
        {
            input = InputHandler.Instance;

            magnetManager = GameObject.Find(GameConstants.MAGNET_MANAGER_OBJ).GetComponent<MagnetManager>();
            magnetController = new MagnetController();

            // コンストラクタの呼び出し
            if (magnetFixed)
            {
                string new_object_type = gameObject.tag;
                MagnetData.MagnetType new_magnet_type = (MagnetData.MagnetType)gameObject.layer;

                MyData = new MagnetData(new_object_type, new_magnet_type, magnetFixedPower);
            }
            else
            {
                string new_object_type = gameObject.tag;

                MyData = new MagnetData(new_object_type);
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected virtual void Update()
        {
            _magnetCollider.SetActive(true);

            if (magnetManager == null) return;

            // 磁力の起動処理
            if (magnetManager.IsMagnetBoot)
            {
                if (!_magnetCollider.activeSelf)
                {
                    // 磁力の有効化
                    _magnetCollider.SetActive(true);
                }

                return;
            }

            // 磁力の無効化処理
            if (_magnetCollider.activeSelf)
            {
                // 磁力の無効化
                _magnetCollider.SetActive(false);
            }

            // 磁力固定オブジェクトは除外
            if (magnetFixed) return;

            // 付与した磁力のリセット
            if (input.IsActionPressed(InputConstants.Action.RESET))
            {
                ResetMagnet();
            }
        }

        /// <summary>
        /// 磁力のリセット処理
        /// </summary>
        private void ResetMagnet()
        {
            // 磁力データの宣言
            MagnetData.MagnetType reset_type = MagnetData.MagnetType.NotType;
            MagnetData.MagnetPower reset_power = MagnetData.MagnetPower.None;

            // レイヤーの更新
            gameObject.layer = (int)reset_type;

            // 磁力データの設定
            MyData.SetMagnetData(reset_type, reset_power);

            MagnetUIManager ui = GameObject.Find("MagnetUIManager").GetComponent<MagnetUIManager>();
            ui.Reset();

            DebugManager.LogMessage("リセットしました");
        }

        /// <summary>
        /// トリガーと当たった時
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnTriggerEnter(Collider other)
        {
            // 磁力が固定の場合は終了
            if (magnetFixed) return;

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
        /// 磁力の強さのセット
        /// </summary>
        /// <param name="power"></param>
        public void SetObjectPower(int power)
        {
            switch (power)
            {
                case (int)MagnetData.MagnetPower.Weak:
                    magnetFixedPower = MagnetData.MagnetPower.Weak;
                    return;

                case (int)MagnetData.MagnetPower.Medium:
                    magnetFixedPower = MagnetData.MagnetPower.Medium;
                    return;

                case (int)MagnetData.MagnetPower.Strong:
                    magnetFixedPower = MagnetData.MagnetPower.Strong;
                    return;
            }
        }
    }
}