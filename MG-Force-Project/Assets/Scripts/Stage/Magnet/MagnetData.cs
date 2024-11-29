using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// オブジェクトの磁力データクラス
    /// </summary>
    public class MagnetData
    {
        // 磁力のタイプ
        public enum MagnetType
        {
            NotType = GameConstants.Layer.DEFAULT,  // 磁力を持っていない

            NForce = GameConstants.Layer.N_MAGNET,  // N極
            SForce = GameConstants.Layer.S_MAGNET,  // S極
        }

        // 磁力の強さ
        public enum MagnetPower
        {
            None = 0,

            Weak = 1,    // 弱
            Medium = 2,  // 中
            Strong = 3,  // 強
            MaxPower,
        }

        // オブジェクトのタイプ
        public enum ObjectType
        {
            NotType = GameConstants.Tag.Untagged, // タグなしオブジェクト

            Fixed = GameConstants.Tag.Fixed,      // 固定オブジェクト
            Moving = GameConstants.Tag.Moving,    // 可動オブジェクト
        }


        /* -------- 値を保持する変数 -------- */

        // 磁力のタイプ
        public MagnetType MyMangetType { get; private set; }

        // 磁力の強さ
        public MagnetPower MyMagnetPower { get; private set; }

        // オブジェクトのタイプ
        public ObjectType MyObjectType { get; private set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="object_type"></param>
        /// <param name="magnet_type"></param>
        /// <param name="magnet_power"></param>
        public MagnetData(ObjectType object_type, MagnetType magnet_type = MagnetType.NotType, MagnetPower magnet_power = MagnetPower.None)
        {
            SetMagnetData(magnet_type, magnet_power, object_type);
        }


        /// <summary>
        /// データの設定
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="layer"></param>
        /// <param name="magnet_power"></param>
        public void SetMagnetData(MagnetType magnet_type, MagnetPower magnet_power, ObjectType object_type = ObjectType.NotType)
        {
            if (SetMagnetType(magnet_type) == GameConstants.ERROR)
            {
                DebugManager.LogMessage("磁力のタイプ設定に失敗しました", DebugManager.MessageType.Error, GetType().ToString());
            }

            if (SetMagnetPower(magnet_power) == GameConstants.ERROR)
            {
                DebugManager.LogMessage("磁力の強さ設定に失敗しました", DebugManager.MessageType.Error, GetType().ToString());
            }

            if (SetObjectType(object_type) == GameConstants.ERROR)
            {
                DebugManager.LogMessage("オブジェクトのタイプ設定に失敗しました", DebugManager.MessageType.Error, GetType().ToString());
            }
        }

        /// <summary>
        /// 磁力のタイプ設定
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
        /// 磁力の強さ設定
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        private int SetMagnetPower(MagnetPower magnet_power)
        {
            if (magnet_power < MagnetPower.None && magnet_power >= MagnetPower.MaxPower) 
            {
                return GameConstants.ERROR; 
            }

            MyMagnetPower = magnet_power;
            return GameConstants.NORMAL;
        }

        /// <summary>
        /// オブジェクトのタイプ設定
        /// </summary>
        /// <param name="object_type"></param>
        /// <returns></returns>
        private int SetObjectType(ObjectType object_type)
        {
            if (object_type == ObjectType.NotType) 
            {  
                return GameConstants.ERROR; 
            }

            MyObjectType = object_type;
            return GameConstants.NORMAL;
        }
    }
}