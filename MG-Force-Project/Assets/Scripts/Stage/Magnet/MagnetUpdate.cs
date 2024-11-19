using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    public class MagnetUpdate : MonoBehaviour
    {
        // 磁力起動フラグ
        public static bool IsMagnetBoot;

        // 選択中のタイプ
        MagnetData.MagnetType SelectType;

        // 選択中の磁力の強さ
        MagnetData.MagnetPower SelectPower;

        private void Update()
        {
            
        }
    }
}