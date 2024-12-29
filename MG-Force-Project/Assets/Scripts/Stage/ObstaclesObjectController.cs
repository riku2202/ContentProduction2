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
        /// �I�u�W�F�N�g�ɓ���������
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // ��O�����F�v���C���[�ȊO�̏ꍇ�͏I������
            if (!collision.gameObject.CompareTag(GameConstants.Tag.PLAYER.ToString())) { return; }

            // �v���C���[�Ɠ������Ă���ꍇ�����Ȃ��悤�ɂ���
            _canMove = false;
        }

        /// <summary>
        /// �I�u�W�F�N�g���痣�ꂽ�Ƃ�
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionExit(Collision collision)
        {
            // ��O�����F�v���C���[�ȊO�̏ꍇ�͏I������
            if (!collision.gameObject.CompareTag(GameConstants.Tag.PLAYER.ToString())) { return; }

            // �v���C���[�����ꂽ�Ƃ��ɓ�����悤�ɂ���
            _canMove = true;
        }

        /// <summary>
        /// �f�t�H���g�̐���
        /// </summary>
        private void SetDefultConstraints()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
        }

        /// <summary>
        /// �v���C���[�Ɠ����������̐���
        /// </summary>
        private void SetHitPlayerConstraints()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}