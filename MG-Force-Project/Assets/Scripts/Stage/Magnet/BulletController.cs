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
        private float bulletSpeed = 10.0f;

        // �e��Rigidbody
        private Rigidbody bullet = null;

        // �^�[�Q�b�g�̍��W
        private Vector3 targetPos = Vector3.zero;

        // �����x�N�g��
        private Vector3 direction = Vector3.zero;

        // �^�O
        private string fixedTag = GameConstants.ConvertTag(GameConstants.Tag.Fixed);
        private string movingTag = GameConstants.ConvertTag(GameConstants.Tag.Moving);

        private float timer;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            timer = 0.0f;

            bullet = GetComponent<Rigidbody>();

            if (Input.GetKeyDown(KeyCode.Return))
            {
                // �^�[�Q�b�g�w��p
                GameObject target = GameObject.Find("target");

                // �^�[�Q�b�g�����݂���ꍇ
                if (target != null)
                {
                    // �^�[�Q�b�g�̍��W�擾
                    targetPos = target.transform.position;

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
            timer += Time.deltaTime;

            if (timer > 12)
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
            direction = targetPos - gameObject.transform.position;

            // ���K��
            direction.Normalize();

            // ���x����Z
            direction *= bulletSpeed;

            // �����x�N�g���ɉ����Ĉړ�������
            bullet.AddForce(direction, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            MagnetObjectManager magnet_object = other.GetComponent<MagnetObjectManager>();

            if (other.CompareTag("Player"))
            {
                DebugManager.LogMessage(other.tag + "�FTag�̃I�u�W�F�N�g���e�ɓ�����܂���", DebugManager.MessageType.Warning, other.GetType().ToString());
            }

            if (other.CompareTag(fixedTag) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "�FTag�̃I�u�W�F�N�g���e�ɓ�����܂���", DebugManager.MessageType.Warning, other.GetType().ToString());

                Destroy(gameObject);
            }
            else if (other.CompareTag(movingTag) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "�FTag�̃I�u�W�F�N�g���e�ɓ�����܂���", DebugManager.MessageType.Warning, other.GetType().ToString());

                Destroy(gameObject);
            }
        }
    }
}