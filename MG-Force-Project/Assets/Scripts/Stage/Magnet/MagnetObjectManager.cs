using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// �I�u�W�F�N�g�̎��͊Ǘ��N���X
    /// </summary>
    public class MagnetObjectManager : MonoBehaviour
    {
        InputManager input;

        // ���̓f�[�^
        public MagnetData MyData {  get; private set; }

        private MagnetManager magnetManager;
        private MagnetController magnetController;

        [SerializeField]
        private GameObject Magnet;

        [SerializeField]
        private bool MagnetFixed;

        [SerializeField]
        private MagnetData.MagnetPower MagnetFixedPower;

        private Rigidbody rigitbody;

        private bool canMove;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            //input = GameObject.Find(GameConstants.INPUT_MANAGER_OBJ).GetComponent<InputManager>();

            magnetManager = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();

            // Tag�^�ɕϊ�
            GameConstants.Tag tag = GameConstants.ConvertTag(gameObject.tag);

            // ObjectType�^�ɕϊ�
            MagnetData.ObjectType new_object_type = (MagnetData.ObjectType)tag;

            // �R���X�g���N�^�̌Ăяo��
            if (MagnetFixed)
            {
                MagnetData.MagnetType new_magnet_type = (MagnetData.MagnetType)gameObject.layer;
                MyData = new MagnetData(new_object_type, new_magnet_type, MagnetFixedPower);
            }
            else
            {
                MyData = new MagnetData(new_object_type);

                if (MyData.MyObjectType == MagnetData.ObjectType.Moving)
                {
                    rigitbody = GetComponent<Rigidbody>();
                }
            }

            magnetController = new MagnetController();

            DebugManager.LogMessage(MyData.MyObjectType.ToString());
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            // ���͂̋N������
            if (magnetManager.IsMagnetBoot && Magnet.activeSelf != true)
            {
                // ���̗͂L����
                Magnet.SetActive(true);
            }
            else if (!magnetManager.IsMagnetBoot && Magnet.activeSelf != false)
            {
                // ���̖͂�����
                Magnet.SetActive(false);
            }

            // ���I�u�W�F�N�g�ȊO�͏��O
            if (MyData.MyObjectType != MagnetData.ObjectType.Moving) return;

            // �Ӑ}���Ȃ������h������
            if (canMove)
            {
                SetDefultConstraints();
            }
            else
            {
                SetHitPlayerConstraints();
            }

            // ���͌Œ�I�u�W�F�N�g�͏��O
            if (MagnetFixed) return;

            // �t�^�������͂̃��Z�b�g
            if (Input.GetKeyDown(KeyCode.R))
            {

                // ���̓f�[�^�̐錾
                MagnetData.MagnetType reset_type = MagnetData.MagnetType.NotType;
                MagnetData.MagnetPower reset_power = MagnetData.MagnetPower.None;

                // ���C���[�̍X�V
                gameObject.layer = (int)reset_type;

                // ���̓f�[�^�̐ݒ�
                MyData.SetMagnetData(reset_type, reset_power);

                DebugManager.LogMessage("���Z�b�g���܂���");
            }
        }

        /// <summary>
        /// �����������̏���
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other == null || MagnetFixed) return;

            if (magnetManager.IsMagnetBoot)
            {
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
            else
            {
                // �e�ɓ���������
                if (other.gameObject.layer == (int)GameConstants.Layer.BULLET)
                {
                    // ���C���[�̍X�V
                    gameObject.layer = (int)magnetManager.CurrentType;

                    // ���̓f�[�^�̎擾
                    MagnetData.MagnetType new_magnet_type = (MagnetData.MagnetType)gameObject.layer;
                    MagnetData.MagnetPower new_magnet_power = (MagnetData.MagnetPower)magnetManager.CurrentPower;

                    // ���̓f�[�^�̐ݒ�
                    MyData.SetMagnetData(new_magnet_type, new_magnet_power);

                    DebugManager.LogMessage(MyData.MyMangetType.ToString() + " | " + MyData.MyMagnetPower.ToString());
                }
            }
        }

        /// <summary>
        /// �I�u�W�F�N�g�ɓ���������
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // ��O����
            if (!collision.gameObject.CompareTag(GameConstants.Tag.Player.ToString()) || MagnetFixed) { return; }

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
            if (!collision.gameObject.CompareTag(GameConstants.Tag.Player.ToString()) || MagnetFixed) { return; }

            // �v���C���[�����ꂽ�Ƃ��ɓ�����悤�ɂ���
            canMove = true;
        }

        public void SetObjectPower(int power)
        {
            switch (power)
            {
                case (int)MagnetData.MagnetPower.Weak:
                    MagnetFixedPower = MagnetData.MagnetPower.Weak;
                    return;

                case (int)MagnetData.MagnetPower.Medium:
                    MagnetFixedPower = MagnetData.MagnetPower.Medium;
                    return;

                case (int)MagnetData.MagnetPower.Strong:
                    MagnetFixedPower = MagnetData.MagnetPower.Strong;
                    return;
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