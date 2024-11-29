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
        MagnetData MyData;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            // Tag型に変換
            GameConstants.Tag tag = GameConstants.ConvertTag(gameObject.tag);

            // ObjectType型に変換
            MagnetData.ObjectType new_object_type = (MagnetData.ObjectType)tag;

            // コンストラクタの呼び出し
            MyData = new MagnetData(new_object_type);
        }

        /// <summary>
        /// 当たった時の処理
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            // 弾に当たった時
            if (other.gameObject.layer == (int)GameConstants.Layer.BULLET)
            {
                // 磁力管理クラスの呼び出し
                MagnetManager magnet = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();

                // レイヤーの更新
                gameObject.layer = (int)magnet.CurrentType;

                // 磁力データの取得
                MagnetData.MagnetType new_magnet_type = (MagnetData.MagnetType)gameObject.layer;
                MagnetData.MagnetPower new_magnet_power = (MagnetData.MagnetPower)magnet.CurrentPower;

                // 磁力データの設定
                MyData.SetMagnetData(new_magnet_type, new_magnet_power);
            }
        }
    }
}