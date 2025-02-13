using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.StageScene.Magnet
{
    /// <summary>
    /// ���͓���Ǘ��N���X
    /// </summary>
    public class MagnetController
    {
        private const int FORWARD = 1;   // ���̒l
        private const int REVERSE = -1;  // ���̒l

        private MagnetData _selfData;     // �����̃f�[�^
        private MagnetData _otherData;  // ����̃f�[�^

        // �������������ǂ���
        private bool _isDirHorizontal;

        private float _horizontal;  // ����
        private float _vertical;    // ����
        private float _direction;   // ����

        /// <summary>
        /// ���͂̓���X�V����
        /// </summary>
        /// <param name="self"></param>
        /// <param name="other"></param>
        public void MagnetUpdate(GameObject self, GameObject other)
        {
            // �f�[�^�̎擾
            _selfData = self.GetComponent<MagnetObjectManager>().MyData;
            _otherData = other.gameObject.GetComponent<MagnetObjectManager>().MyData;

            // ���������߂�
            _horizontal = self.transform.position.x - other.transform.position.x;
            _vertical = self.transform.position.y - other.transform.position.y;

            // �傫���ق��������̕����Ƃ���
            _isDirHorizontal = Mathf.Abs(_horizontal) > Mathf.Abs(_vertical);

            _direction = (_isDirHorizontal) ? _horizontal : _vertical;

            // �l�̑���������
            float reverse = (_direction > 0) ? FORWARD : REVERSE;

            Rigidbody rigidbody = self.GetComponent<Rigidbody>();

            // �ړ�����
            if (_selfData.MyMangetType == _otherData.MyMangetType)
            {
                rigidbody.AddForce(MagnetMove(reverse), ForceMode.Force);
            }
            else
            {

                rigidbody.AddForce(MagnetMove(-reverse), ForceMode.Force);
            }
        }

        /// <summary>
        /// ���͂̈ړ�����
        /// </summary>
        /// <param name="reverse"></param>
        /// <returns></returns>
        private Vector3 MagnetMove(float reverse)
        {
            Vector3 magnet_move = new Vector3 (0, 0, 0);

            if (_isDirHorizontal)
            {
                magnet_move.x = (int)_selfData.MyMagnetPower * (int)_otherData.MyMagnetPower * reverse;
            }
            else
            {
                magnet_move.y = (int)_selfData.MyMagnetPower * (int)_otherData.MyMagnetPower * reverse;
            }

            return magnet_move;
        }
    }
}