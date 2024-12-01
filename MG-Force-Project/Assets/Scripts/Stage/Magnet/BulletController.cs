using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// 弾の動作管理クラス
    /// </summary>
    public class BulletController : MonoBehaviour
    {
        // 弾の速度
        [SerializeField]
        private float BulletSpeed = 1.0f;

        // 弾のRigidbody
        private Rigidbody Bullet = null;

        // 弾の座標
        private Vector3 BulletPos = Vector3.zero;

        // ターゲットの座標
        private Vector3 TargetPos = Vector3.zero;

        // 向きベクトル
        private Vector3 Direction = Vector3.zero;

        // タグ
        private string FixedTag = GameConstants.ConvertTag(GameConstants.Tag.Fixed);
        private string MovingTag = GameConstants.ConvertTag(GameConstants.Tag.Moving);

        private float Timer;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            Timer = 0.0f;

            Bullet = GetComponent<Rigidbody>();

            // 弾の座標取得
            BulletPos = gameObject.transform.position;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                // ターゲット指定用
                GameObject target = GameObject.Find("target");

                // ターゲットが存在する場合
                if (target != null)
                {
                    // ターゲットの座標取得
                    TargetPos = target.GetComponent<Rigidbody>().position;

                    //// 弾の発射
                    FiringBullet();
                }
                // ターゲットが存在しない場合
                else
                {
                    DebugManager.LogMessage("ターゲットが存在しません", DebugManager.MessageType.Error);

                    Destroy(gameObject);
                }
            }
        }

        private void Update()
        {
            Timer += Time.deltaTime;

            if (Timer > 2)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 弾の発射処理
        /// </summary>
        private void FiringBullet()
        {
            // 向きベクトルを求める
            Direction = TargetPos - BulletPos;

            // 正規化
            Direction.Normalize();

            // 速度を乗算
            Direction *= BulletSpeed;

            // 向きベクトルに応じて移動させる
            Bullet.AddForce(Direction, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            MagnetObjectManager magnet_object = other.GetComponent<MagnetObjectManager>();

            if (other.CompareTag("Player"))
            {
                DebugManager.LogMessage(other.tag + "：Tagのオブジェクトが弾に当たりました", DebugManager.MessageType.Warning, other.GetType().ToString());
            }

            if (other.CompareTag(FixedTag) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "：Tagのオブジェクトが弾に当たりました", DebugManager.MessageType.Warning, other.GetType().ToString());

                Destroy(gameObject);
            }
            else if (other.CompareTag(MovingTag) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "：Tagのオブジェクトが弾に当たりました", DebugManager.MessageType.Warning, other.GetType().ToString());

                Destroy(gameObject);
            }
        }
    }
}