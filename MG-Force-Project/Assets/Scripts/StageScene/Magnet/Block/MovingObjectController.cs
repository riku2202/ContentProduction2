using System.Collections.Generic;
using UnityEngine;

namespace Game.StageScene.Magnet
{
    /// <summary>
    /// 可動オブジェクトクラス
    /// </summary>
    public class MovingObjectController : MagnetObjectManager
    {
        private bool _canMove;

        private Rigidbody _rigitbody;

        private List<Collider> _isHitMagnet = new List<Collider>();

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override void Start()
        {
            base.Start();

            _rigitbody = GetComponent<Rigidbody>();

            _canMove = true;
        }

        protected override void Update()
        {
            base.Update();

            // 意図しない動作を防ぐ処理
            if (_canMove)
            {
                SetDefultConstraints();

                if (!magnetManager.IsMagnetBoot)
                {
                    _rigitbody.velocity = Vector3.zero;
                }
            }
            else
            {
                SetHitPlayerConstraints();
            }
        }

        #region -------- 判定処理 --------

        /// <summary>
        /// オブジェクトに当たった時
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // 例外処理：プレイヤー以外の場合は終了する
            if (magnetManager.IsMagnetBoot || !collision.gameObject.CompareTag(GameConstants.Tag.PLAYER.ToString())) { return; }

            // プレイヤーと当たっている場合動かないようにする
            _canMove = false;
        }

        /// <summary>
        /// オブジェクトから離れたとき
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionExit(Collision collision)
        {
            // 例外処理
            if (magnetManager.IsMagnetBoot || !collision.gameObject.CompareTag(GameConstants.Tag.PLAYER.ToString()) || magnetFixed) { return; }

            // プレイヤーが離れたときに動けるようにする
            _canMove = true;
        }

        /// <summary>
        /// トリガーと当たった時
        /// </summary>
        /// <param name="other"></param>
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            // 例外チェック：磁力の範囲オブジェクトではない場合は終了
            if (other.gameObject.layer != (int)GameConstants.Layer.MAGNET_RANGE) return;

            // リストに追加
            _isHitMagnet.Add(other);
        }

        /// <summary>
        /// トリガーと当たっている時の処理
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay(Collider other)
        {
            // 磁力が起動していなければ終了
            if (!magnetManager.IsMagnetBoot) return;

            // 例外チェック
            if (other.gameObject.layer != (int)GameConstants.Layer.N_MAGNET &&
                other.gameObject.layer != (int)GameConstants.Layer.S_MAGNET) return;

            // このオブジェクトが可動オブジェクトの場合
            if (MyData.MyObjectType == GameConstants.Tag.MOVING)
            {
                // 磁力の動作処理
                magnetController.MagnetUpdate(gameObject, other.gameObject);
            }
        }

        /// <summary>
        /// トリガーが離れた時の処理
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            // 例外チェック：磁力の範囲オブジェクトではない場合は終了
            if (other.gameObject.layer != (int)GameConstants.Layer.MAGNET_RANGE) return;

            // リストから削除
            if (_isHitMagnet.IndexOf(other) != -1)
            {
                _isHitMagnet.Remove(other);
            }

            // 磁力の範囲に入っていない場合は動作をリセットする
            if (_isHitMagnet.Count == 0)
            {
                _rigitbody.velocity = Vector3.zero;
            }
        }

        #endregion

        /// <summary>
        /// デフォルトの制約
        /// </summary>
        private void SetDefultConstraints()
        {
            _rigitbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        /// <summary>
        /// プレイヤーと当たった時の制約
        /// </summary>
        private void SetHitPlayerConstraints()
        {
            _rigitbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}