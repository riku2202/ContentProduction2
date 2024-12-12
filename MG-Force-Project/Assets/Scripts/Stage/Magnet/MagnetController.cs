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

        private MagnetData selfData;     // �����̃f�[�^
        private MagnetData otherData;  // ����̃f�[�^

        // �������������ǂ���
        private bool isHorizontal;

        private float horizontal;  // ����
        private float vertical;    // ����
        private float direction;   // ����

        /// <summary>
        /// ���͂̓���X�V����
        /// </summary>
        /// <param name="self"></param>
        /// <param name="other"></param>
        public void MagnetUpdate(GameObject self, GameObject other)
        {
            // �f�[�^�̎擾
            selfData = self.GetComponent<MagnetObjectManager>().MyData;
            otherData = other.gameObject.GetComponent<MagnetObjectManager>().MyData;

            // ���������߂�
            horizontal = self.transform.position.x - other.transform.position.x;
            vertical = self.transform.position.y - other.transform.position.y;

            //float Threshold = 0.01f;

            isHorizontal = Mathf.Abs(horizontal) > Mathf.Abs(vertical);

            // + Threshold

            direction = (isHorizontal) ? horizontal : vertical;

            int reverse = (direction > 0) ? FORWARD : REVERSE;

            // �ړ�����
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
        /// ���͂̈ړ�����
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