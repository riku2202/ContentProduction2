using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem 
{
    /// <summary>
    /// ゲームデータ
    /// </summary>
    [System.Serializable]
    public class GameData
    {
        // ステージのクリアフラグ
        private bool[] IsClearStage = new bool[GameConstants.STAGE_MAX_NUM];

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
            for (int i = 0; i < GameConstants.STAGE_MAX_NUM; i++)
            {
                IsClearStage[i] = false;
            }

            return GameConstants.NORMAL;
        }
    }
}