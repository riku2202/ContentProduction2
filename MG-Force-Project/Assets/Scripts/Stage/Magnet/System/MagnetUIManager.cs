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

        [SerializeField]
        private GameObject Boot;

        [SerializeField]
        private GameObject NoBoot;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            Magnet = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();
        }

        /// <summary>
        /// 磁力のタイプ変更(Button)
        /// </summary>
        public void OnButton_ChangeMagnetType()
        {
            Magnet.ChangeMagnetType();

            DebugManager.LogMessage(Magnet.CurrentType.ToString(), DebugManager.MessageType.Normal);
        }

        /// <summary>
        /// 磁力の実行変更(Button)
        /// </summary>
        public void OnButton_ChangeMagnetBoot()
        {
            Magnet.ChangeMagnetBoot();

            DebugManager.LogMessage(Magnet.IsMagnetBoot.ToString(), DebugManager.MessageType.Normal);
        }

        /// <summary>
        /// ボタンの判定(デバック用)
        /// </summary>
        public void OnButtonClick()
        {
            DebugManager.LogMessage("pushButton!", DebugManager.MessageType.Normal);
        }

        /// <summary>
        /// 更新処理
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