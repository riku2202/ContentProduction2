using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// マグネット関連のUI管理クラス
    /// </summary>
    public class MagnetUIManager : MonoBehaviour
    {
        private MagnetManager Magnet;

        [SerializeField]
        private GameObject NBeam;

        [SerializeField]
        private GameObject SBeam;

        private void Start()
        {
            Magnet = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();
        }

        public void OnButton_ChangeMagnetType()
        {
            Magnet.ChangeMagnetType();

            DebugManager.LogMessage(Magnet.CurrentType.ToString(), DebugManager.MessageType.Normal, GetType().ToString());
        }

        public void OnButton_ChangeMagnetBoot()
        {
            Magnet.ChangeMagnetBoot();

            DebugManager.LogMessage(Magnet.IsMagnetBoot.ToString(), DebugManager.MessageType.Normal, GetType().ToString());
        }

        public void OnButtonClick()
        {
            DebugManager.LogMessage("pushButton!", DebugManager.MessageType.Normal, GetType().ToString());
        }

        public void Update()
        {
            if (Magnet.CurrentType == GameConstants.Layer.N_MAGNET)
            {
                NBeam.SetActive(true);
                SBeam.SetActive(false);
            }
            else if (Magnet.CurrentType == GameConstants.Layer.S_MAGNET)
            {
                SBeam.SetActive(true);
                NBeam.SetActive(false);
            }
        }
    }
}