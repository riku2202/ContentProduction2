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
        private string FixedTag = GameConstants.Tag.Fixed.ToString();
        private string MovingTag = GameConstants.Tag.Moving.ToString();

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            Bullet = GetComponent<Rigidbody>();

            // 弾の座標取得
            BulletPos = Bullet.position;

            if (Input.GetMouseButtonDown(0))
            {
                // ターゲット確認用
                GameObject target = GameObject.Find("Target");

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
                }
            }
        }

        private void Update()
        {
            if (transform.position.x < -10 || transform.position.x > 10 ||
                transform.position.y < -10 || transform.position.y > 10)
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
            if (other.CompareTag(FixedTag))
            {
                Debug.Log("Hit");

                Destroy(gameObject);
            }
            else if (other.CompareTag(MovingTag))
            {
                Debug.Log("Hit");

                Destroy(gameObject);
            }
        }
    }
}