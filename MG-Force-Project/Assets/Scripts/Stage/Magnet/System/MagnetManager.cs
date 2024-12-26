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
            //Input = GameObject.Find("InputManager").GetComponent<InputManager>();

            IsMagnetBoot = false;

            CurrentType = GameConstants.Layer.N_MAGNET;
            CurrentPower = 1;
        }
        
        /// <summary>
        /// �X�V����
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