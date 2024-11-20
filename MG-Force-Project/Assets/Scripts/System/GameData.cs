using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem 
{
    /// <summary>
    /// �Q�[���f�[�^
    /// </summary>
    [System.Serializable]
    public class GameData
    {
        // �X�e�[�W�̍ő吔
        private const int STAGE_MAX_NUM = 8;

        // �X�e�[�W�̃N���A�t���O
        private bool[] IsClearStage = new bool[STAGE_MAX_NUM];

        public void SetIsClearStage(int stage_number)
        {
            IsClearStage[stage_number] = true;
        }

        public bool GetIsClearStage(int stage_number)
        {
            return IsClearStage[stage_number];
        }


        public int ReSetData()
        {
            for (int i = 0; i < STAGE_MAX_NUM; i++)
            {
                IsClearStage[i] = false;
            }

            return GameConstants.NORMAL;
        }
    }
}