using System.Collections.Generic;
using UnityEngine;

namespace Game.StageScene.Magnet
{
    /// <summary>
    /// ���I�u�W�F�N�g�N���X
    /// </summary>
    public class MovingObjectController : MagnetObjectManager
    {
        private bool _canMove;

        private Rigidbody _rigitbody;

        private List<Collider> _isHitMagnet = new List<Collider>();

        /// <summary>
        /// ����������
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

            // �Ӑ}���Ȃ������h������
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

        #region -------- ���菈�� --------

        /// <summary>
        /// �I�u�W�F�N�g�ɓ���������
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // ��O�����F�v���C���[�ȊO�̏ꍇ�͏I������
            if (magnetManager.IsMagnetBoot || !collision.gameObject.CompareTag(GameConstants.Tag.PLAYER.ToString())) { return; }

            // �v���C���[�Ɠ������Ă���ꍇ�����Ȃ��悤�ɂ���
            _canMove = false;
        }

        /// <summary>
        /// �I�u�W�F�N�g���痣�ꂽ�Ƃ�
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionExit(Collision collision)
        {
            // ��O����
            if (magnetManager.IsMagnetBoot || !collision.gameObject.CompareTag(GameConstants.Tag.PLAYER.ToString()) || magnetFixed) { return; }

            // �v���C���[�����ꂽ�Ƃ��ɓ�����悤�ɂ���
            _canMove = true;
        }

        /// <summary>
        /// �g���K�[�Ɠ���������
        /// </summary>
        /// <param name="other"></param>
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            // ��O�`�F�b�N�F���͈͂̔̓I�u�W�F�N�g�ł͂Ȃ��ꍇ�͏I��
            if (other.gameObject.layer != (int)GameConstants.Layer.MAGNET_RANGE) return;

            // ���X�g�ɒǉ�
            _isHitMagnet.Add(other);
        }

        /// <summary>
        /// �g���K�[�Ɠ������Ă��鎞�̏���
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay(Collider other)
        {
            // ���͂��N�����Ă��Ȃ���ΏI��
            if (!magnetManager.IsMagnetBoot) return;

            // ��O�`�F�b�N
            if (other.gameObject.layer != (int)GameConstants.Layer.N_MAGNET &&
                other.gameObject.layer != (int)GameConstants.Layer.S_MAGNET) return;

            // ���̃I�u�W�F�N�g�����I�u�W�F�N�g�̏ꍇ
            if (MyData.MyObjectType == GameConstants.Tag.MOVING)
            {
                // ���͂̓��쏈��
                magnetController.MagnetUpdate(gameObject, other.gameObject);
            }
        }

        /// <summary>
        /// �g���K�[�����ꂽ���̏���
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            // ��O�`�F�b�N�F���͈͂̔̓I�u�W�F�N�g�ł͂Ȃ��ꍇ�͏I��
            if (other.gameObject.layer != (int)GameConstants.Layer.MAGNET_RANGE) return;

            // ���X�g����폜
            if (_isHitMagnet.IndexOf(other) != -1)
            {
                _isHitMagnet.Remove(other);
            }

            // ���͈͂̔͂ɓ����Ă��Ȃ��ꍇ�͓�������Z�b�g����
            if (_isHitMagnet.Count == 0)
            {
                _rigitbody.velocity = Vector3.zero;
            }
        }

        #endregion

        /// <summary>
        /// �f�t�H���g�̐���
        /// </summary>
        private void SetDefultConstraints()
        {
            _rigitbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        /// <summary>
        /// �v���C���[�Ɠ����������̐���
        /// </summary>
        private void SetHitPlayerConstraints()
        {
            _rigitbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}