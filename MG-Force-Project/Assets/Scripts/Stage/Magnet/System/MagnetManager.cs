using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// 磁力の管理クラス
    /// </summary>
    public class MagnetManager : MonoBehaviour
    {
        // 磁力起動フラグ
        internal bool IsMagnetBoot { get; private set; }

        // 選択中のタイプ
        public GameConstants.Layer CurrentType { get; private set; }

        // 現在の磁力の強さ
        public int CurrentPower { get; private set; }

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            //Input = GameObject.Find("InputManager").GetComponent<InputManager>();

            IsMagnetBoot = false;

            CurrentType = GameConstants.Layer.N_MAGNET;
            CurrentPower = 1;
        }
        
        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ChangeMagnetType();

                DebugManager.LogMessage(CurrentType.ToString(), DebugManager.MessageType.Normal);
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                ChangeMagnetBoot();

                DebugManager.LogMessage(IsMagnetBoot.ToString(), DebugManager.MessageType.Normal);
            }
        }

        /// <summary>
        /// 磁力タイプの切り替え
        /// </summary>
        public void ChangeMagnetType()
        {
            if (CurrentType != GameConstants.Layer.N_MAGNET)
            {
                CurrentType = GameConstants.Layer.N_MAGNET;
            }
            else
            {
                CurrentType = GameConstants.Layer.S_MAGNET;
            }
        }

        public void ChangeMagnetBoot()
        {
            IsMagnetBoot = !IsMagnetBoot;
        }
    }
}