using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Magnet
{
    /// <summary>
    /// ���͂̍X�V����
    /// </summary>
    public class MagnetManager : MonoBehaviour
    {
        // ���͋N���t���O
        internal bool IsMagnetBoot { get; private set; }

        private const MagnetData.MagnetType NForce = MagnetData.MagnetType.NForce;

        private const MagnetData.MagnetType SForce = MagnetData.MagnetType.SForce;

        // �I�𒆂̃^�C�v
        MagnetData.MagnetType SelectType;

        // �I�𒆂̎��͂̋���
        MagnetData.MagnetPower SelectPower;

        [SerializeField]
        private GameObject N_Object;

        [SerializeField]
        private GameObject S_Object;

        private void Start()
        {
            SelectType = NForce;
        }
        
        private void Update()
        {
            var input = InputManager.Instance;

            if (input.IsActionPressed(GameConstants.INPUT_MAG_CHANGE))
            {
                if (SelectType != NForce)
                {
                    SelectType = NForce;
                }
            }
        }
    }
}