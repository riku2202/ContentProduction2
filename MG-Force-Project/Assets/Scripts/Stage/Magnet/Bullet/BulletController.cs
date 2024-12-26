using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// �e�̓���Ǘ��N���X
    /// </summary>
    public class BulletController : MonoBehaviour
    {
        // �e�̑��x
        private float _bulletSpeed = 10.0f;

        // �e��Rigidbody
        private Rigidbody _rigidbody = null;

        // �^�[�Q�b�g�̍��W
        private Vector3 _targetPos = Vector3.zero;

        // �����x�N�g��
        private Vector3 _direction = Vector3.zero;

        // �^�O
        private string _fixedTag = GameConstants.ConvertTag(GameConstants.Tag.Fixed);
        private string _movingTag = GameConstants.ConvertTag(GameConstants.Tag.Moving);

        private float _timer;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            _timer = 0.0f;

            _rigidbody = GetComponent<Rigidbody>();

            if (Input.GetKeyDown(KeyCode.Return))
            {
                // �^�[�Q�b�g�w��p
                GameObject target = GameObject.Find("target");

                // �^�[�Q�b�g�����݂���ꍇ
                if (target != null)
                {
                    // �^�[�Q�b�g�̍��W�擾
                    _targetPos = target.transform.position;

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
            _timer += Time.deltaTime;

            if (_timer > 12)
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
            _direction = _targetPos - gameObject.transform.position;

            // ���K��
            _direction.Normalize();

            // ���x����Z
            _direction *= _bulletSpeed;

            // �����x�N�g���ɉ����Ĉړ�������
            _rigidbody.AddForce(_direction, ForceMode.Impulse);
        }

        /// <summary>
        /// �O������Ăяo���\�Ȓe�̔��ˏ���
        /// </summary>
        /// <param name="fireDirection">���˂�������x�N�g��</param>
        /// <param name="speed">�e�̑��x</param>
        public void Fire(Vector3 fireDirection, float speed)
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }

            if (_rigidbody != null)
            {
                // �����𐳋K�����đ��x��ݒ�
                _direction = fireDirection.normalized;
                _bulletSpeed = speed;

                // �����x�N�g���ɉ����ė͂�������
                _rigidbody.AddForce(_direction * _bulletSpeed, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("Rigidbody �����݂��Ȃ����߁A�e�𔭎˂ł��܂���B");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            MagnetObjectManager magnet_object = other.GetComponent<MagnetObjectManager>();

            if (other.CompareTag("Player"))
            {
                DebugManager.LogMessage(other.tag + "�FTag�̃I�u�W�F�N�g���e�ɓ�����܂���", DebugManager.MessageType.Warning);
            }

            if (other.CompareTag(_fixedTag) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "�FTag�̃I�u�W�F�N�g���e�ɓ�����܂���", DebugManager.MessageType.Warning);

                Destroy(gameObject);
            }
            else if (other.CompareTag(_movingTag) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "�FTag�̃I�u�W�F�N�g���e�ɓ�����܂���", DebugManager.MessageType.Warning);

                Destroy(gameObject);
            }
        }
    }
}