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
        internal bool IsMagnetBoot { get; private set; }

        private const MagnetData.MagnetType NForce = MagnetData.MagnetType.NForce;

        private const MagnetData.MagnetType SForce = MagnetData.MagnetType.SForce;

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
            SelectType = NForce;
        }
        
        private void Update()
        {
            var input = InputManager.Instance;

            if (input.IsActionPressed(GameConstants.INPUT_MAG_CHANGE))
            {
                if (SelectType != NForce)
                {
                    SelectType = NForce;
                }
            }
        }
    }
}