using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// �I�u�W�F�N�g�̎��͊Ǘ��N���X
    /// </summary>
    public class MagnetObjectManager : MonoBehaviour
    {
        GameSystem.InputManager input;

        // ���̓f�[�^
        public MagnetData MyData {  get; protected set; }

        // ���͊Ǘ��N���X
        protected MagnetManager magnetManager;

        // ���͓���Ǘ��N���X
        protected MagnetController magnetController;

        // ���͂̃R���C�_�[�t���I�u�W�F�N�g
        [SerializeField]
        private GameObject _magnetCollider;

        // ���͂̌Œ�L��
        [SerializeField]
        protected bool magnetFixed;

        // ���͂̋���
        [SerializeField]
        protected MagnetData.MagnetPower magnetFixedPower;

        /// <summary>
        /// ����������
        /// </summary>
        protected virtual void Start()
        {
            //input = GameObject.Find(GameConstants.INPUT_MANAGER_OBJ).GetComponent<InputManager>();

            magnetManager = GameObject.Find(GameConstants.MAGNET_MANAGER_OBJ).GetComponent<MagnetManager>();
            magnetController = new MagnetController();

            // Tag�^�ɕϊ�
            GameConstants.Tag tag = GameConstants.ConvertTag(gameObject.tag);

            // �R���X�g���N�^�̌Ăяo��
            if (magnetFixed)
            {
                MagnetData.ObjectType new_object_type = (MagnetData.ObjectType)tag;
                MagnetData.MagnetType new_magnet_type = (MagnetData.MagnetType)gameObject.layer;

                MyData = new MagnetData(new_object_type, new_magnet_type, magnetFixedPower);
            }
            else
            {
                MagnetData.ObjectType new_object_type = (MagnetData.ObjectType)tag;

                MyData = new MagnetData(new_object_type);
            }
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        protected virtual void Update()
        {
            _magnetCollider.SetActive(true);

            // ���͂̋N������
            if (magnetManager.IsMagnetBoot)
            {
                if (!_magnetCollider.activeSelf)
                {
                    // ���̗͂L����
                    _magnetCollider.SetActive(true);
                }

                return;
            }

            // ���̖͂���������
            if (_magnetCollider.activeSelf)
            {
                // ���̖͂�����
                _magnetCollider.SetActive(false);
            }

            // ���͌Œ�I�u�W�F�N�g�͏��O
            if (magnetFixed) return;

            // �t�^�������͂̃��Z�b�g
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetMagnet();
            }
        }

        /// <summary>
        /// ���͂̃��Z�b�g����
        /// </summary>
        private void ResetMagnet()
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

        /// <summary>
        /// �g���K�[�Ɠ���������
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnTriggerEnter(Collider other)
        {
            // ���͂��Œ�̏ꍇ�͏I��
            if (magnetFixed) return;

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
        /// ���͂̋����̃Z�b�g
        /// </summary>
        /// <param name="power"></param>
        public void SetObjectPower(int power)
        {
            switch (power)
            {
                case (int)MagnetData.MagnetPower.Weak:
                    magnetFixedPower = MagnetData.MagnetPower.Weak;
                    return;

                case (int)MagnetData.MagnetPower.Medium:
                    magnetFixedPower = MagnetData.MagnetPower.Medium;
                    return;

                case (int)MagnetData.MagnetPower.Strong:
                    magnetFixedPower = MagnetData.MagnetPower.Strong;
                    return;
            }
        }
    }
}