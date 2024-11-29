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
        private InputManager Input;

        // �ȗ��p
        private readonly string FixedTag = GameConstants.Tag.Fixed.ToString();
        private readonly string MovingTag = GameConstants.Tag.Moving.ToString();

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
            Input = GameObject.Find("InputManager").GetComponent<InputManager>();
        }
        
        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            if (Input.IsActionPressed(GameConstants.INPUT_JUMP))
            {
                ChangeMagnetType();

                DebugManager.LogMessage(CurrentType.ToString(), DebugManager.MessageType.Normal, GetType().ToString());
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
    }
}