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
        public static bool IsMagnetBoot;

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
            SelectType = MagnetData.MagnetType.NForce;
        }

        private void Update()
        {
           
        }
    }
}