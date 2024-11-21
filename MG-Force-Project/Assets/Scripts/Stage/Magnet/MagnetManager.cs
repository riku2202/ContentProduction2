using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// 磁力の更新処理
    /// </summary>
    public class MagnetManager : MonoBehaviour
    {
        // 磁力起動フラグ
        public static bool IsMagnetBoot;

        // 選択中のタイプ
        MagnetData.MagnetType SelectType;

        // 選択中の磁力の強さ
        MagnetData.MagnetPower SelectPower;

        [SerializeField]
        private GameObject N_Object;

        [SerializeField]
        private GameObject S_Object;

        private void Start()
        {
            SelectType = MagnetData.MagnetType.NForce;
        }

        private void Update()
        {
           
        }
    }
}