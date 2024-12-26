using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// 弾の動作管理クラス
    /// </summary>
    public class BulletController : MonoBehaviour
    {
        // 弾の速度
        private float _bulletSpeed = 10.0f;

        // 弾のRigidbody
        private Rigidbody _rigidbody = null;

        // ターゲットの座標
        private Vector3 _targetPos = Vector3.zero;

        // 向きベクトル
        private Vector3 _direction = Vector3.zero;

        // タグ
        private string _fixedTag = GameConstants.ConvertTag(GameConstants.Tag.Fixed);
        private string _movingTag = GameConstants.ConvertTag(GameConstants.Tag.Moving);

        private float _timer;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            _timer = 0.0f;

            _rigidbody = GetComponent<Rigidbody>();

            if (Input.GetKeyDown(KeyCode.Return))
            {
                // ターゲット指定用
                GameObject target = GameObject.Find("target");

                // ターゲットが存在する場合
                if (target != null)
                {
                    // ターゲットの座標取得
                    _targetPos = target.transform.position;

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

            if (other.CompareTag("Player"))
            {
                DebugManager.LogMessage(other.tag + "：Tagのオブジェクトが弾に当たりました", DebugManager.MessageType.Warning);
            }

            if (other.CompareTag(_fixedTag) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "：Tagのオブジェクトが弾に当たりました", DebugManager.MessageType.Warning);

                Destroy(gameObject);
            }
            else if (other.CompareTag(_movingTag) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "：Tagのオブジェクトが弾に当たりました", DebugManager.MessageType.Warning);

                Destroy(gameObject);
            }
        }
    }
}