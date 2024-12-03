using UnityEditor;
using UnityEngine;

using Game.GameSystem;
using UnityEngine.SceneManagement;

namespace Game.Stage
{
    /// <summary>
    /// ステージロードクラス
    /// </summary>
    public class StageLoader : MonoBehaviour 
    {
        private GameDataManager gameDataManager;

        // ステージデータ
        [SerializeField]
        private StageData[] Data;

        private void Awake()
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene.buildIndex == (int)GameConstants.Scene.StageSelect)
            {
                gameDataManager.SetCurrentStageIndex((int)GameConstants.Stage.SELECT);
                SetStage();
            }
        }

        public void SetStage()
        {
            int stage_index = gameDataManager.GetCurrentStageIndex();

            Transform transform = GameObject.Find(GameConstants.MAIN_CAMERA_OBJ).transform;

            GameObject stage = Instantiate(Data[stage_index].StagePrefab);
            GameObject bg = Instantiate(Data[stage_index].StageBG, Vector3.zero, Quaternion.identity, transform);
        }

        public StageData GetStageData(int stage_index)
        {
            return Data[stage_index];
        }
    }
}