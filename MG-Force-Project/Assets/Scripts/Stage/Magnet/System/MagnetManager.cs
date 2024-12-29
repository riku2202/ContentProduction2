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
        private GameSystem.InputHandler _input;

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
            _input = GameObject.Find(GameConstants.Object.INPUT).GetComponent<GameSystem.InputHandler>();

            IsMagnetBoot = false;

            CurrentType = GameConstants.Layer.N_MAGNET;
            CurrentPower = 1;
        }
        
        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            if (_input.IsActionPressed(GameConstants.Input.Action.POLE_SWITCHING) && !IsMagnetBoot)
            {
                ChangeMagnetType();
            }
            
            if (_input.IsActionPressed(GameConstants.Input.Action.MAGNET_BOOT))
            {
                ChangeMagnetBoot();
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