using UnityEditor;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// �I�u�W�F�N�g�̎��͊Ǘ��N���X
    /// </summary>
    public class MagnetObjectManager : MonoBehaviour
    {
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

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
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
            }

            magnetController = new MagnetController();

            DebugManager.LogMessage(MyData.MyObjectType.ToString());
        }

        private void Update()
        {
            if (magnetManager.IsMagnetBoot && Magnet.activeSelf != true)
            {
                Magnet.SetActive(true);
            }
            else if (!magnetManager.IsMagnetBoot && Magnet.activeSelf != false)
            {
                Magnet.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (MagnetFixed) { return; }

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
            if (other == null || MagnetFixed) { return; }

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

        /// <summary>
        /// �������Ă��鎞�̏���
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay(Collider other)
        {
            // null�`�F�b�N
            if (other == null) { return; }

            // ��O�`�F�b�N
            if (other.gameObject.layer != (int)GameConstants.Layer.N_MAGNET && 
                other.gameObject.layer != (int)GameConstants.Layer.S_MAGNET) { return; }

            // ���̃I�u�W�F�N�g�����I�u�W�F�N�g�̏ꍇ
            if (MyData.MyObjectType == MagnetData.ObjectType.Moving)
            {
                // ���͂̓��쏈��
                magnetController.MagnetUpdate(gameObject, other.gameObject);

                //if (Mathf.Abs(gameObject.transform.position.x - parentTransform.position.x) > 0.01f ||
                //      Mathf.Abs(gameObject.transform.position.y - parentTransform.position.y) > 0.01f)
                //{
                //}
            }
        }
    }
}