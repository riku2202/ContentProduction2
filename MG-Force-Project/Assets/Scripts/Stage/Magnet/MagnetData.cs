using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    public class MagnetData
    {
        // �I�u�W�F�N�g�̃^�C�v
        public enum ObjectType
        {
            Fixed,   // �Œ�
            Moving,  // ��
        }

        private ObjectType currentObjectType;

        // ���΂̃��C���[
        public const int N_MAGNET_LAYER = 6;  // N��
        public const int S_MAGNET_LAYER = 7;  // S��

        // ���͂̃^�C�v
        public enum MagnetType
        {
            NForce,  // N��
            SForce,  // S��
            NotForce,
        }

        private MagnetType currentMagnetType;

        // ���͂̋���
        public enum MagnetPower
        {
            None = 0,

            Weak = 1,    // ��
            Medium = 2,  // ��
            Strong = 3,  // ��
        }

        private MagnetPower currentMagnetPower;

        public void SetMagnetData(string tag, int layer, int power)
        {
            if (SetObjType(tag) == -1)
            {
                Debug.Log("�yMagnetData.cs�z�G���[�@�I�u�W�F�N�g�̃^�C�v�ݒ�Ɏ��s���܂���");
            }

            if (SetMagnetType(layer) == -1)
            {
                Debug.Log("�yMagnetData.cs�z�G���[�@���͂̃^�C�v�ݒ�Ɏ��s���܂���");
            }

            if (SetMagnetPower(power) == -1)
            {
                Debug.Log("�yMagnetData.cs�z�G���[�@���͂̋����ݒ�Ɏ��s���܂���");
            }
        }

        private int SetObjType(string tag)
        {
            switch (tag)
            {
                case GameConstants.FIXED_OBJTAG:
                    currentObjectType = ObjectType.Fixed;
                    return 0;

                case GameConstants.MOVE_OBJTAG:
                    currentObjectType = ObjectType.Moving;
                    return 0;

                default:
                    return -1;
            }
        }

        private int SetMagnetType(int layer)
        {
            switch (layer)
            {
                case N_MAGNET_LAYER:
                    currentMagnetType = MagnetType.NForce;
                    return 0;

                case S_MAGNET_LAYER:
                    currentMagnetType = MagnetType.SForce;
                    return 0;

                default:
                    currentMagnetType = MagnetType.NotForce;
                    return 0;
            }
        }

        private int SetMagnetPower(int power)
        {
            switch (power)
            {
                case (int)MagnetPower.None:
                    currentMagnetPower = MagnetPower.None;
                    return 0;

                case (int)MagnetPower.Weak:
                    currentMagnetPower = MagnetPower.Weak;
                    return 0;

                case (int)MagnetPower.Medium:
                    currentMagnetPower = MagnetPower.Medium;
                    return 0;

                case (int)MagnetPower.Strong:
                    currentMagnetPower = MagnetPower.Strong;
                    return 0;

                default:
                    return -1;  // �����Ȓl�̏ꍇ�̓G���[��Ԃ�
            }
        }

        public ObjectType GetObjType()
        {
            return currentObjectType;
        }

        public MagnetType GetMagnetType()
        {
            return currentMagnetType;
        }

        public MagnetPower GetMagnetPower()
        {
            return currentMagnetPower;
        }
    }
}