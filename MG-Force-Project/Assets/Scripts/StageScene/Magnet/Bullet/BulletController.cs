using UnityEngine;

namespace Game.StageScene.Magnet
{
    /// <summary>
    /// 弾の動作管理クラス
    /// </summary>
    public class BulletController : MonoBehaviour
    {
        // タグ
        private const string FIXED_TAG = GameConstants.Tag.FIXED;
        private const string MOVING_TAG = GameConstants.Tag.MOVING;

        private const float INIT_SPEED = 10.0f;
        private const float INIT_TIMER = 0.0f;

        // 弾のRigidbody
        private Rigidbody _rigidbody = null;

        // ターゲットの座標
        private Vector3 _targetPos = Vector3.zero;

        // 向きベクトル
        private Vector3 _direction = Vector3.zero;

        // 弾の速度
        private float _bulletSpeed = INIT_SPEED;

        private float _timer = INIT_TIMER;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            BulletShootController bulletShoot = GameObject.Find(GameConstants.PLAYER_OBJ).GetComponent<BulletShootController>();

            // ターゲットの座標取得
            _targetPos = bulletShoot.targetPos;

            //// 弾の発射
            FiringBullet();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > 12)
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
            _direction = _targetPos - gameObject.transform.position;

            // 正規化
            _direction.Normalize();

            // 速度を乗算
            _direction *= _bulletSpeed;

            // 向きベクトルに応じて移動させる
            _rigidbody.AddForce(_direction, ForceMode.Impulse);
        }

        /// <summary>
        /// 外部から呼び出し可能な弾の発射処理
        /// </summary>
        /// <param name="fireDirection">発射する方向ベクトル</param>
        /// <param name="speed">弾の速度</param>
        public void Fire(Vector3 fireDirection, float speed)
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }

            if (_rigidbody != null)
            {
                // 方向を正規化して速度を設定
                _direction = fireDirection.normalized;
                _bulletSpeed = speed;

                // 向きベクトルに応じて力を加える
                _rigidbody.AddForce(_direction * _bulletSpeed, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("Rigidbody が存在しないため、弾を発射できません。");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            MagnetObjectManager magnet_object = other.GetComponent<MagnetObjectManager>();

            if (other.CompareTag(FIXED_TAG) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "：Tagのオブジェクトが弾に当たりました", DebugManager.MessageType.Warning);

                Destroy(gameObject);
            }
            else if (other.CompareTag(MOVING_TAG) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "：Tagのオブジェクトが弾に当たりました", DebugManager.MessageType.Warning);

                Destroy(gameObject);
            }
        }
    }
}