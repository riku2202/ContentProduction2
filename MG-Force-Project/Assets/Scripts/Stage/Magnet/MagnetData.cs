using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    public class MagnetData
    {
        // オブジェクトのタイプ
        public enum ObjectType
        {
            Fixed,   // 固定
            Moving,  // 可動
        }

        private ObjectType currentObjectType;

        // 磁石のレイヤー
        public const int N_MAGNET_LAYER = 6;  // N極
        public const int S_MAGNET_LAYER = 7;  // S極

        // 磁力のタイプ
        public enum MagnetType
        {
            NForce,  // N極
            SForce,  // S極
            NotForce,
        }

        private MagnetType currentMagnetType;

        // 磁力の強さ
        public enum MagnetPower
        {
            None = 0,

            Weak = 1,    // 弱
            Medium = 2,  // 中
            Strong = 3,  // 強
        }

        private MagnetPower currentMagnetPower;

        public void SetMagnetData(string tag, int layer, int power)
        {
            if (SetObjType(tag) == -1)
            {
                Debug.Log("【MagnetData.cs】エラー　オブジェクトのタイプ設定に失敗しました");
            }

            if (SetMagnetType(layer) == -1)
            {
                Debug.Log("【MagnetData.cs】エラー　磁力のタイプ設定に失敗しました");
            }

            if (SetMagnetPower(power) == -1)
            {
                Debug.Log("【MagnetData.cs】エラー　磁力の強さ設定に失敗しました");
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
                    return -1;  // 無効な値の場合はエラーを返す
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