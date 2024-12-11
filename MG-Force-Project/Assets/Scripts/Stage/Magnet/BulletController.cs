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
        private float bulletSpeed = 10.0f;

        // 弾のRigidbody
        private Rigidbody bullet = null;

        // ターゲットの座標
        private Vector3 targetPos = Vector3.zero;

        // 向きベクトル
        private Vector3 direction = Vector3.zero;

        // タグ
        private string fixedTag = GameConstants.ConvertTag(GameConstants.Tag.Fixed);
        private string movingTag = GameConstants.ConvertTag(GameConstants.Tag.Moving);

        private float timer;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            timer = 0.0f;

            bullet = GetComponent<Rigidbody>();

            if (Input.GetKeyDown(KeyCode.Return))
            {
                // ターゲット指定用
                GameObject target = GameObject.Find("target");

                // ターゲットが存在する場合
                if (target != null)
                {
                    // ターゲットの座標取得
                    targetPos = target.transform.position;

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
            timer += Time.deltaTime;

            if (timer > 12)
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
            direction = targetPos - gameObject.transform.position;

            // 正規化
            direction.Normalize();

            // 速度を乗算
            direction *= bulletSpeed;

            // 向きベクトルに応じて移動させる
            bullet.AddForce(direction, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            MagnetObjectManager magnet_object = other.GetComponent<MagnetObjectManager>();

            if (other.CompareTag("Player"))
            {
                DebugManager.LogMessage(other.tag + "：Tagのオブジェクトが弾に当たりました", DebugManager.MessageType.Warning, other.GetType().ToString());
            }

            if (other.CompareTag(fixedTag) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "：Tagのオブジェクトが弾に当たりました", DebugManager.MessageType.Warning, other.GetType().ToString());

                Destroy(gameObject);
            }
            else if (other.CompareTag(movingTag) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "：Tagのオブジェクトが弾に当たりました", DebugManager.MessageType.Warning, other.GetType().ToString());

                Destroy(gameObject);
            }
        }
    }
}