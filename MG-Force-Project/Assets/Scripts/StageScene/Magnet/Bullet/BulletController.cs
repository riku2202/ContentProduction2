using UnityEngine;

namespace Game.StageScene.Magnet
{
    /// <summary>
    /// �e�̓���Ǘ��N���X
    /// </summary>
    public class BulletController : MonoBehaviour
    {
        private BulletLineController _lineController;
        // �^�O
        private const string FIXED_TAG = GameConstants.Tag.FIXED;
        private const string MOVING_TAG = GameConstants.Tag.MOVING;

        private const float INIT_SPEED = 10.0f;
        private const float INIT_TIMER = 0.0f;

        // �e��Rigidbody
        private Rigidbody _rigidbody = null;

        private Animator _animator;

        // �^�[�Q�b�g�̍��W
        private Vector3 _targetPos = Vector3.zero;

        // �����x�N�g��
        private Vector3 _direction = Vector3.zero;

        // �e�̑��x
        private float _bulletSpeed = INIT_SPEED;

        private float _timer = INIT_TIMER;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _animator = GetComponent<Animator>();

            _lineController = GameObject.Find("BulletLine").GetComponent<BulletLineController>();
            // �^�[�Q�b�g�̍��W�擾
            _targetPos = _lineController.targetPosition;

            //// �e�̔���
            FiringBullet();
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

        private void OnTriggerEnter(Collider other)
        {
            MagnetObjectManager magnet_object = other.GetComponent<MagnetObjectManager>();

            if (other.CompareTag(FIXED_TAG) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "�FTag�̃I�u�W�F�N�g���e�ɓ�����܂���", DebugManager.MessageType.Warning);

                Destroy(gameObject);
            }
            else if (other.CompareTag(MOVING_TAG) && magnet_object != null)
            {
                DebugManager.LogMessage(other.tag + "�FTag�̃I�u�W�F�N�g���e�ɓ�����܂���", DebugManager.MessageType.Warning);

                Destroy(gameObject);
            }
        }
    }
}