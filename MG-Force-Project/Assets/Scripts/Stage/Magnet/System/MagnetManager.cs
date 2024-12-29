using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// ���͂̊Ǘ��N���X
    /// </summary>
    public class MagnetManager : MonoBehaviour
    {
        private GameSystem.InputHandler _input;

        // ���͋N���t���O
        internal bool IsMagnetBoot { get; private set; }

        // �I�𒆂̃^�C�v
        public GameConstants.Layer CurrentType { get; private set; }

        // ���݂̎��͂̋���
        public int CurrentPower { get; private set; }

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            _input = GameObject.Find(GameConstants.Object.INPUT).GetComponent<GameSystem.InputHandler>();

            IsMagnetBoot = false;

            CurrentType = GameConstants.Layer.N_MAGNET;
            CurrentPower = 1;
        }
        
        /// <summary>
        /// �X�V����
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
        /// ���̓^�C�v�̐؂�ւ�
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