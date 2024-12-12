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

        private MagnetData selfData;     // 自分のデータ
        private MagnetData otherData;  // 相手のデータ

        // 向きが水平かどうか
        private bool isHorizontal;

        private float horizontal;  // 水平
        private float vertical;    // 垂直
        private float direction;   // 向き

        /// <summary>
        /// 磁力の動作更新処理
        /// </summary>
        /// <param name="self"></param>
        /// <param name="other"></param>
        public void MagnetUpdate(GameObject self, GameObject other)
        {
            // データの取得
            selfData = self.GetComponent<MagnetObjectManager>().MyData;
            otherData = other.gameObject.GetComponent<MagnetObjectManager>().MyData;

            // 向きを決める
            horizontal = self.transform.position.x - other.transform.position.x;
            vertical = self.transform.position.y - other.transform.position.y;

            //float Threshold = 0.01f;

            isHorizontal = Mathf.Abs(horizontal) > Mathf.Abs(vertical);

            // + Threshold

            direction = (isHorizontal) ? horizontal : vertical;

            int reverse = (direction > 0) ? FORWARD : REVERSE;

            // 移動処理
            if (selfData.MyMangetType == otherData.MyMangetType)
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

            if (isHorizontal)
            {
                magnet_move.x = (int)selfData.MyMagnetPower * reverse;
            }
            else
            {
                magnet_move.y = (int)selfData.MyMagnetPower * reverse;
            }

            return magnet_move;
        }
    }
}