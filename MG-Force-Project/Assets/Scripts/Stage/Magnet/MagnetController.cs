using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// ���͓���Ǘ��N���X
    /// </summary>
    public class MagnetController
    {
        private const int FORWARD = 1;   // ���̒l
        private const int REVERSE = -1;  // ���̒l

        private MagnetData SelfData;     // �����̃f�[�^
        private MagnetData OtherData;  // ����̃f�[�^

        // �������������ǂ���
        private bool IsHorizontal;

        private float Horizontal;  // ����
        private float Vertical;    // ����
        private float Direction;   // ����

        /// <summary>
        /// ���͂̓���X�V����
        /// </summary>
        /// <param name="self"></param>
        /// <param name="other"></param>
        public void MagnetUpdate(GameObject self, GameObject other)
        {
            // �f�[�^�̎擾
            SelfData = self.GetComponent<MagnetObjectManager>().MyData;
            OtherData = other.gameObject.GetComponent<MagnetObjectManager>().MyData;

            // ���������߂�
            Horizontal = self.transform.position.x - other.transform.position.x;
            Vertical = self.transform.position.y - other.transform.position.y;

            //float Threshold = 0.01f;

            IsHorizontal = Mathf.Abs(Horizontal) > Mathf.Abs(Vertical);

            // + Threshold

            Direction = (IsHorizontal) ? Horizontal : Vertical;

            int reverse = (Direction > 0) ? FORWARD : REVERSE;

            // �ړ�����
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
        /// ���͂̈ړ�����
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