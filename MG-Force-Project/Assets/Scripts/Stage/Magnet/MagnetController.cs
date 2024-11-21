using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// é•óÕä«óùÉNÉâÉX
    /// </summary>
    public class MagnetController : MonoBehaviour
    {
        private MagnetData MyData;

        private MagnetData OtherData;

        private int MagnetPower = 0;

        private void Start()
        {            
            MyData.SetMagnetData(gameObject.tag, gameObject.layer, MagnetPower);
        }

        private void FixedUpdate()
        {
            if (!MagnetManager.IsMagnetBoot) { return; };
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 8)
            {
                gameObject.layer = GameConstants.N_MAGNET_LAYER;

                MyData.SetMagnetData(gameObject.tag, gameObject.layer, MagnetPower);
            }
        }
    }
}