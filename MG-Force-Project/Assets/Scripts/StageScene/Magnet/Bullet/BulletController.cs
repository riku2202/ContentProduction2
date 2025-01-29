using UnityEngine;

namespace Game.StageScene.Magnet
{
    /// <summary>
    /// �e�̓���Ǘ��N���X
    /// </summary>
    public class BulletController : MonoBehaviour
    {
        // �^�O
        private const string FIXED_TAG = GameConstants.Tag.FIXED;
        private const string MOVING_TAG = GameConstants.Tag.MOVING;

        private const float INIT_SPEED = 10.0f;
        private const float INIT_TIMER = 0.0f;

        // �e��Rigidbody
        private Rigidbody _rigidbody = null;

        private Animator _animator;


        // @yu-ki-rohi
        // Fire�ŏ��������s���ꍇ�A�ȉ��̃����o�ϐ�����
        // _timer�ȊO�͕K�v�͂Ȃ������Ɍ����܂��B
        // ����Rigidbody�����[�J���ł��������ł��B
        // Animator������g�p����Ă��Ȃ����ȁH

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

            // @yu-ki-rohi
            // �����炭�s�v�Ȏ擾�ł���
            BulletShootController bulletShoot = GameObject.Find(GameConstants.PLAYER_OBJ).GetComponent<BulletShootController>();

            // @yu-ki-rohi
            // �ȉ��͎d�l�ύX�O�̎c�[�ł��傤��
            // ���s���ԓI��Fire��Start�ɂȂ�Ǝv���̂ŁA�e�̋������Ӑ}���Ȃ����̂ɂȂ�\��������܂�

            // �^�[�Q�b�g�̍��W�擾
            _targetPos = GameObject.Find("target").GetComponent<Transform>().position;

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