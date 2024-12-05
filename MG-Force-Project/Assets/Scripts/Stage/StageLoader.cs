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

        [SerializeField]
        private GameConstants.Stage currentStage;

        private void Awake()
        {
            gameDataManager = GameDataManager.Instance;

            Scene scene = SceneManager.GetActiveScene();

            if (scene.buildIndex == (int)GameConstants.Scene.StageSelect)
            {
                gameDataManager.SetCurrentStageIndex((int)currentStage);

                // ステージの生成
                SetStage();
            }
        }

        Transform childTransform;

        StageCreater stageCreater;

        /// <summary>
        /// ステージの生成
        /// </summary>
        public void SetStage()
        {
            childTransform = gameObject.transform.Find("StageCreater");
            stageCreater = childTransform.GetComponent<StageCreater>();
            stageCreater.StageCreate();

            //int stage_index = gameDataManager.GetCurrentStageIndex();

            //Transform transform = GameObject.Find(GameConstants.MAIN_CAMERA_OBJ).transform;

            //GameObject stage = Instantiate(Data[stage_index].StagePrefab, Vector3.zero, Quaternion.identity);
            //GameObject bg = Instantiate(Data[stage_index].StageBG, Vector3.zero, Quaternion.identity, transform);
        }

        /// <summary>
        /// ステージデータの取得
        /// </summary>
        /// <param name="stage_index"></param>
        /// <returns></returns>
        public StageData GetStageData(int stage_index)
        {
            return Data[stage_index];
        }
    }
}