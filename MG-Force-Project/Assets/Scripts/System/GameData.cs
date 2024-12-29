namespace Game.GameSystem 
{
    /// <summary>
    /// �Q�[���f�[�^
    /// </summary>
    [System.Serializable]
    public class GameData
    {
        // �X�e�[�W�̃N���A�t���O
        private bool[] _isClearStages = new bool[GameConstants.STAGE_MAX_NUM];

        public GameData()
        {
            ReSetData();
        }

        public void SetIsClearStage(int stage_number)
        {
            _isClearStages[stage_number] = true;
        }

        public bool GetIsClearStage(int stage_number)
        {
            return _isClearStages[stage_number];
        }

        public int ReSetData()
        {
            for (int i = 0; i < GameConstants.STAGE_MAX_NUM; i++)
            {
                _isClearStages[i] = false;
            }

            return GameConstants.NORMAL;
        }
    }
}