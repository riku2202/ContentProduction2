using Game.Stage.Magnet;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage 
{
    public class obstaclesObjectController : MonoBehaviour
    {
        private bool _canMove;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_canMove)
            {
                SetDefultConstraints();
            }
            else
            {
                SetHitPlayerConstraints();
            }

            _rigidbody.velocity = Vector3.zero;
        }

        /// <summary>
        /// オブジェクトに当たった時
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // 例外処理：プレイヤー以外の場合は終了する
            if (!collision.gameObject.CompareTag(GameConstants.Tag.PLAYER.ToString())) { return; }

            // プレイヤーと当たっている場合動かないようにする
            _canMove = false;
        }

        /// <summary>
        /// オブジェクトから離れたとき
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionExit(Collision collision)
        {
            // 例外処理：プレイヤー以外の場合は終了する
            if (!collision.gameObject.CompareTag(GameConstants.Tag.PLAYER.ToString())) { return; }

            // プレイヤーが離れたときに動けるようにする
            _canMove = true;
        }

        /// <summary>
        /// デフォルトの制約
        /// </summary>
        private void SetDefultConstraints()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
        }

        /// <summary>
        /// プレイヤーと当たった時の制約
        /// </summary>
        private void SetHitPlayerConstraints()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}