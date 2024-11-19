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
        // ステージの最大数
        private const int STAGE_MAX_NUM = 8;

        // ステージのクリアフラグ
        private bool[] IsClearStage = new bool[STAGE_MAX_NUM];
    }
}