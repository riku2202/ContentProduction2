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
        private string FixedTag = GameConstants.ConvertTag(GameConstants.Tag.Fixed);
        private string MovingTag = GameConstants.ConvertTag(GameConstants.Tag.Moving);

        private float Timer;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            Timer = 0.0f;

            Bullet = GetComponent<Rigidbody>();

            // �e�̍��W�擾
            BulletPos = gameObject.transform.position;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                // �^�[�Q�b�g�w��p
                GameObject target = GameObject.Find("target");

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

                    Destroy(gameObject);
                }
            }
        }

        private void Update()
        {
            Timer += Time.deltaTime;

            if (Timer > 2)
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
            MagnetObjectManager magnet_object = other.GetComponent<MagnetObjectManager>();

            if (other.CompareTag("Player"))
            {
                DebugManager.LogMessage(other.tag + "�FTag�̃I�u�W�F�N�g���e�ɓ�����܂���", DebugManager.MessageType.Warning, other.GetType().ToString());
            }

            if (other.CompareTag(FixedTag) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "�FTag�̃I�u�W�F�N�g���e�ɓ�����܂���", DebugManager.MessageType.Warning, other.GetType().ToString());

                Destroy(gameObject);
            }
            else if (other.CompareTag(MovingTag) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "�FTag�̃I�u�W�F�N�g���e�ɓ�����܂���", DebugManager.MessageType.Warning, other.GetType().ToString());

                Destroy(gameObject);
            }
        }
    }
}