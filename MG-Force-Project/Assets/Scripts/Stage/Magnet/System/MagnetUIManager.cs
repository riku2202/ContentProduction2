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

        [SerializeField]
        private GameObject NBeam;

        [SerializeField]
        private GameObject SBeam;

        [SerializeField]
        private GameObject Boot;

        [SerializeField]
        private GameObject NoBoot;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            Magnet = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();
        }

        /// <summary>
        /// ���͂̃^�C�v�ύX(Button)
        /// </summary>
        public void OnButton_ChangeMagnetType()
        {
            Magnet.ChangeMagnetType();

            DebugManager.LogMessage(Magnet.CurrentType.ToString(), DebugManager.MessageType.Normal);
        }

        /// <summary>
        /// ���͂̎��s�ύX(Button)
        /// </summary>
        public void OnButton_ChangeMagnetBoot()
        {
            Magnet.ChangeMagnetBoot();

            DebugManager.LogMessage(Magnet.IsMagnetBoot.ToString(), DebugManager.MessageType.Normal);
        }

        /// <summary>
        /// �{�^���̔���(�f�o�b�N�p)
        /// </summary>
        public void OnButtonClick()
        {
            DebugManager.LogMessage("pushButton!", DebugManager.MessageType.Normal);
        }

        /// <summary>
        /// �X�V����
        /// </summary>
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

            if (Magnet.IsMagnetBoot)
            {
                Boot.SetActive(true);
                NoBoot.SetActive(false);
            }
            else
            {
                NoBoot.SetActive(true);
                Boot.SetActive(false);
            }
        }
    }
}