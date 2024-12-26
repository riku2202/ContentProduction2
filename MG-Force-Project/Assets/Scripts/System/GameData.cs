namespace Game.GameSystem 
{
    /// <summary>
    /// ゲームデータ
    /// </summary>
    [System.Serializable]
    public class GameData
    {
        // ステージのクリアフラグ
        private bool[] isClearStages = new bool[GameConstants.STAGE_MAX_NUM];

        public GameData()
        {
            ReSetData();
        }

        public void SetIsClearStage(int stage_number)
        {
            isClearStages[stage_number] = true;
        }

        public bool GetIsClearStage(int stage_number)
        {
            return isClearStages[stage_number];
        }

        public int ReSetData()
        {
            for (int i = 0; i < GameConstants.STAGE_MAX_NUM; i++)
            {
                isClearStages[i] = false;
            }

            return GameConstants.NORMAL;
        }
    }
}