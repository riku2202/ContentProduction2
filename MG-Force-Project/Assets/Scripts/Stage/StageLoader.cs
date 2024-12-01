using UnityEditor;
using UnityEngine;

using Game.GameSystem;

namespace Game.Stage
{
    /// <summary>
    /// �X�e�[�W���[�h�N���X
    /// </summary>
    public class StageLoader : MonoBehaviour 
    {
        private GameDataManager gameDataManager;

        // �X�e�[�W�f�[�^
        [SerializeField]
        private StageData[] Data;

        public void SetStage()
        {
            int stage_index = gameDataManager.GetCurrentStageIndex();

            GameObject stage = Data[stage_index].StagePrefab;
            GameObject bg = Data[stage_index].StageBG;
        }

        public StageData GetStageData(int stage_index)
        {
            return Data[stage_index];
        }
    }
}