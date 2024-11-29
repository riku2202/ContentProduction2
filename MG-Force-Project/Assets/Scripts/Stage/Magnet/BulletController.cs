using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// �e�̓���Ǘ��N���X
    /// </summary>
    public class BulletController : MonoBehaviour
    {
        // �e�̑��x
        [SerializeField]
        private float BulletSpeed = 1.0f;

        // �e��Rigidbody
        private Rigidbody Bullet = null;

        // �e�̍��W
        private Vector3 BulletPos = Vector3.zero;

        // �^�[�Q�b�g�̍��W
        private Vector3 TargetPos = Vector3.zero;

        // �����x�N�g��
        private Vector3 Direction = Vector3.zero;

        // �^�O
        private string FixedTag = GameConstants.Tag.Fixed.ToString();
        private string MovingTag = GameConstants.Tag.Moving.ToString();

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            Bullet = GetComponent<Rigidbody>();

            // �e�̍��W�擾
            BulletPos = Bullet.position;

            if (Input.GetMouseButtonDown(0))
            {
                // �^�[�Q�b�g�m�F�p
                GameObject target = GameObject.Find("Target");

                // �^�[�Q�b�g�����݂���ꍇ
                if (target != null)
                {
                    // �^�[�Q�b�g�̍��W�擾
                    TargetPos = target.GetComponent<Rigidbody>().position;

                    //// �e�̔���
                    FiringBullet();
                }
                // �^�[�Q�b�g�����݂��Ȃ��ꍇ
                else
                {
                    DebugManager.LogMessage("�^�[�Q�b�g�����݂��܂���", DebugManager.MessageType.Error);
                }
            }
        }

        private void Update()
        {
            if (transform.position.x < -10 || transform.position.x > 10 ||
                transform.position.y < -10 || transform.position.y > 10)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// �e�̔��ˏ���
        /// </summary>
        private void FiringBullet()
        {
            // �����x�N�g�������߂�
            Direction = TargetPos - BulletPos;

            // ���K��
            Direction.Normalize();

            // ���x����Z
            Direction *= BulletSpeed;

            // �����x�N�g���ɉ����Ĉړ�������
            Bullet.AddForce(Direction, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(FixedTag))
            {
                Debug.Log("Hit");

                Destroy(gameObject);
            }
            else if (other.CompareTag(MovingTag))
            {
                Debug.Log("Hit");

                Destroy(gameObject);
            }
        }
    }
}