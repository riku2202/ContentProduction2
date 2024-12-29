using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// �I�u�W�F�N�g�̎��̓f�[�^�N���X
    /// </summary>
    public class MagnetData
    {
        // ���͂̃^�C�v
        public enum MagnetType
        {
            NotType = GameConstants.Layer.DEFAULT,  // ���͂������Ă��Ȃ�

            NForce = GameConstants.Layer.N_MAGNET,  // N��
            SForce = GameConstants.Layer.S_MAGNET,  // S��
        }

        // ���͂̋���
        public enum MagnetPower
        {
            None = 0,

            Weak = 1,    // ��
            Medium = 2,  // ��
            Strong = 3,  // ��
            MaxPower,
        }

        // �I�u�W�F�N�g�̃^�C�v
        public enum ObjectType
        {
            NotType,

            Fixed = 1,
            Moving = 2,
        }

        /* -------- �l��ێ�����ϐ� -------- */

        // ���͂̃^�C�v
        public MagnetType MyMangetType { get; private set; }

        // ���͂̋���
        public MagnetPower MyMagnetPower { get; private set; }

        // �I�u�W�F�N�g�̃^�C�v
        public string MyObjectType { get; private set; }


        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="object_type"></param>
        /// <param name="magnet_type"></param>
        /// <param name="magnet_power"></param>
        public MagnetData(string object_type, MagnetType magnet_type = MagnetType.NotType, MagnetPower magnet_power = MagnetPower.None)
        {
            SetMagnetData(magnet_type, magnet_power, object_type);
        }


        /// <summary>
        /// �f�[�^�̐ݒ�
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="layer"></param>
        /// <param name="magnet_power"></param>
        public void SetMagnetData(MagnetType magnet_type, MagnetPower magnet_power, string object_type = GameConstants.Tag.UNTAGGED)
        {
            if (SetMagnetType(magnet_type) == GameConstants.ERROR)
            {
                DebugManager.LogMessage("���͂̃^�C�v�ݒ�Ɏ��s���܂���", DebugManager.MessageType.Error);
            }

            if (SetMagnetPower(magnet_power) == GameConstants.ERROR)
            {
                DebugManager.LogMessage("���͂̋����ݒ�Ɏ��s���܂���", DebugManager.MessageType.Error);
            }

            if (SetObjectType(object_type) == GameConstants.ERROR)
            {
                DebugManager.LogMessage("�I�u�W�F�N�g�̃^�C�v�ݒ�Ɏ��s���܂���", DebugManager.MessageType.Error);
            }
        }

        /// <summary>
        /// ���͂̃^�C�v�ݒ�
        /// </summary>
        /// <param name="magnet_type"></param>
        /// <returns></returns>
        private int SetMagnetType(MagnetType magnet_type)
        {
            if (MyMangetType != MagnetType.NotType && magnet_type != MagnetType.NotType) 
            {
                return GameConstants.ERROR; 
            }

            MyMangetType = magnet_type;
            return GameConstants.NORMAL;
        }

        /// <summary>
        /// ���͂̋����ݒ�
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        private int SetMagnetPower(MagnetPower magnet_power)
        {
            if (MyMagnetPower != MagnetPower.None && magnet_power != MagnetPower.None)
            {
                return GameConstants.ERROR; 
            }

            MyMagnetPower = magnet_power;
            return GameConstants.NORMAL;
        }

        /// <summary>
        /// �I�u�W�F�N�g�̃^�C�v�ݒ�
        /// </summary>
        /// <param name="object_type"></param>
        /// <returns></returns>
        private int SetObjectType(string object_type)
        {
            if (object_type == GameConstants.Tag.UNTAGGED) 
            {  
                return GameConstants.ERROR; 
            }

            MyObjectType = object_type;
            return GameConstants.NORMAL;
        }
    }
}