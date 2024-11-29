using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// 磁力動作管理クラス
    /// </summary>
    public class MagnetController
    {
        private const int FORWARD = 1;   // 正の値
        private const int REVERSE = -1;  // 負の値

        private MagnetData SelfData;     // 自分のデータ
        private MagnetData OtherData;  // 相手のデータ

        // 向きが水平かどうか
        private bool IsHorizontal;

        private float Horizontal;  // 水平
        private float Vertical;    // 垂直
        private float Direction;   // 向き

        /// <summary>
        /// 磁力の動作更新処理
        /// </summary>
        /// <param name="self"></param>
        /// <param name="other"></param>
        public void MagnetUpdate(GameObject self, GameObject other)
        {
            // データの取得
            SelfData = self.GetComponent<MagnetObjectManager>().MyData;
            OtherData = other.gameObject.GetComponent<MagnetObjectManager>().MyData;

            // 向きを決める
            Horizontal = self.transform.position.x - other.transform.position.x;
            Vertical = self.transform.position.y - other.transform.position.y;

            //float Threshold = 0.01f;

            IsHorizontal = Mathf.Abs(Horizontal) > Mathf.Abs(Vertical);

            // + Threshold

            Direction = (IsHorizontal) ? Horizontal : Vertical;

            int reverse = (Direction > 0) ? FORWARD : REVERSE;

            // 移動処理
            if (SelfData.MyMangetType == OtherData.MyMangetType)
            {
                self.GetComponent<Rigidbody>().AddForce(MagnetMove(reverse), ForceMode.Impulse);
            }
            else
            {
                self.GetComponent<Rigidbody>().AddForce(MagnetMove(-reverse), ForceMode.Impulse);
            }
        }

        /// <summary>
        /// 磁力の移動処理
        /// </summary>
        /// <param name="reverse"></param>
        /// <returns></returns>
        private Vector3 MagnetMove(int reverse)
        {
            Vector3 magnet_move = new Vector3 (0, 0, 0);

            if (IsHorizontal)
            {
                magnet_move.x = (int)SelfData.MyMagnetPower * reverse * Time.deltaTime;
            }
            else
            {
                magnet_move.y = (int)SelfData.MyMagnetPower * reverse * Time.deltaTime;
            }

            return magnet_move;
        }
    }
}