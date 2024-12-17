using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// 可動オブジェクトクラス
    /// </summary>
    public class MovingObject : MagnetObjectManager
    {
        private bool canMove;

        private Rigidbody rigitbody;

        private List<Collider> isHitMagnet = new List<Collider>();

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override void Start()
        {
            base.Start();

            rigitbody = GetComponent<Rigidbody>();

            canMove = true;
        }

        protected override void Update()
        {
            base.Update();

            // 意図しない動作を防ぐ処理
            if (canMove)
            {
                SetDefultConstraints();

                if (!magnetManager.IsMagnetBoot)
                {
                    rigitbody.velocity = Vector3.zero;
                }
            }
            else
            {
                SetHitPlayerConstraints();
            }
        }

        /// <summary>
        /// オブジェクトに当たった時
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // 例外処理：プレイヤー以外の場合は終了する
            if (magnetManager.IsMagnetBoot || !collision.gameObject.CompareTag(GameConstants.Tag.Player.ToString())) { return; }

            // プレイヤーと当たっている場合動かないようにする
            canMove = false;
        }

        /// <summary>
        /// オブジェクトから離れたとき
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionExit(Collision collision)
        {
            // 例外処理
            if (magnetManager.IsMagnetBoot || !collision.gameObject.CompareTag(GameConstants.Tag.Player.ToString()) || magnetFixed) { return; }

            // プレイヤーが離れたときに動けるようにする
            canMove = true;
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
            isHitMagnet.Add(other);
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
            if (MyData.MyObjectType == MagnetData.ObjectType.Moving)
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
            if (isHitMagnet.IndexOf(other) != -1)
            {
                isHitMagnet.Remove(other);
            }

            // 磁力の範囲に入っていない場合は動作をリセットする
            if (isHitMagnet.Count == 0)
            {
                rigitbody.velocity = Vector3.zero;
            }
        }

        /// <summary>
        /// デフォルトの制約
        /// </summary>
        private void SetDefultConstraints()
        {
            rigitbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        /// <summary>
        /// プレイヤーと当たった時の制約
        /// </summary>
        private void SetHitPlayerConstraints()
        {
            rigitbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}