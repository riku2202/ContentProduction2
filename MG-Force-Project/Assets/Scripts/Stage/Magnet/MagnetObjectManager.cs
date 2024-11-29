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
        MagnetData MyData;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            // Tag�^�ɕϊ�
            GameConstants.Tag tag = GameConstants.ConvertTag(gameObject.tag);

            // ObjectType�^�ɕϊ�
            MagnetData.ObjectType new_object_type = (MagnetData.ObjectType)tag;

            // �R���X�g���N�^�̌Ăяo��
            MyData = new MagnetData(new_object_type);
        }

        /// <summary>
        /// �����������̏���
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            // �e�ɓ���������
            if (other.gameObject.layer == (int)GameConstants.Layer.BULLET)
            {
                // ���͊Ǘ��N���X�̌Ăяo��
                MagnetManager magnet = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();

                // ���C���[�̍X�V
                gameObject.layer = (int)magnet.CurrentType;

                // ���̓f�[�^�̎擾
                MagnetData.MagnetType new_magnet_type = (MagnetData.MagnetType)gameObject.layer;
                MagnetData.MagnetPower new_magnet_power = (MagnetData.MagnetPower)magnet.CurrentPower;

                // ���̓f�[�^�̐ݒ�
                MyData.SetMagnetData(new_magnet_type, new_magnet_power);
            }
        }
    }
}