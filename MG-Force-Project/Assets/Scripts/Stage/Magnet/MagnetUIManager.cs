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

        private void Start()
        {
            Magnet = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();
        }

        public void OnButtonChangeMagnetType()
        {
            Magnet.ChangeMagnetType();

            DebugManager.LogMessage(Magnet.CurrentType.ToString(), DebugManager.MessageType.Normal, GetType().ToString());
        }

        public void OnButtonClick()
        {

            DebugManager.LogMessage("ボタンを押した", DebugManager.MessageType.Normal, GetType().ToString());
        }

        public void Update()
        {

        }
    }
}