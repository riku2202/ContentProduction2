using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// ���I�u�W�F�N�g�N���X
    /// </summary>
    public class MovingObject : MagnetObjectManager
    {
        private bool canMove;

        private Rigidbody rigitbody;

        private List<Collider> isHitMagnet = new List<Collider>();

        /// <summary>
        /// ����������
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

            // �Ӑ}���Ȃ������h������
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
        /// �I�u�W�F�N�g�ɓ���������
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // ��O�����F�v���C���[�ȊO�̏ꍇ�͏I������
            if (magnetManager.IsMagnetBoot || !collision.gameObject.CompareTag(GameConstants.Tag.Player.ToString())) { return; }

            // �v���C���[�Ɠ������Ă���ꍇ�����Ȃ��悤�ɂ���
            canMove = false;
        }

        /// <summary>
        /// �I�u�W�F�N�g���痣�ꂽ�Ƃ�
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionExit(Collision collision)
        {
            // ��O����
            if (magnetManager.IsMagnetBoot || !collision.gameObject.CompareTag(GameConstants.Tag.Player.ToString()) || magnetFixed) { return; }

            // �v���C���[�����ꂽ�Ƃ��ɓ�����悤�ɂ���
            canMove = true;
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
            isHitMagnet.Add(other);
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
            if (MyData.MyObjectType == MagnetData.ObjectType.Moving)
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
            if (isHitMagnet.IndexOf(other) != -1)
            {
                isHitMagnet.Remove(other);
            }

            // ���͈͂̔͂ɓ����Ă��Ȃ��ꍇ�͓�������Z�b�g����
            if (isHitMagnet.Count == 0)
            {
                rigitbody.velocity = Vector3.zero;
            }
        }

        /// <summary>
        /// �f�t�H���g�̐���
        /// </summary>
        private void SetDefultConstraints()
        {
            rigitbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        /// <summary>
        /// �v���C���[�Ɠ����������̐���
        /// </summary>
        private void SetHitPlayerConstraints()
        {
            rigitbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}