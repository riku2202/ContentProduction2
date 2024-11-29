using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// �}�O�l�b�g�֘A��UI�Ǘ��N���X
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

            DebugManager.LogMessage("�{�^����������", DebugManager.MessageType.Normal, GetType().ToString());
        }

        public void Update()
        {

        }
    }
}