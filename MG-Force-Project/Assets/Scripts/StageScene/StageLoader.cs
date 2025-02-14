using UnityEngine;
using UnityEngine.SceneManagement;
using Game.GameSystem;

namespace Game.StageScene
{
    /// <summary>
    /// ステージロードクラス
    /// </summary>
    public class StageLoader : MonoBehaviour 
    {
        private InputHandler _inputHandler;
        private GameDataManager _gameDataManager;
        private StageCreater stageCreater;

        private Transform childTransform;

        // ステージデータ
        [SerializeField] private StageData[] _stageDatas;

        private static GameConstants.Stage _currentStage;

        private void Awake()
        {
            _inputHandler = InputHandler.Instance;
            _gameDataManager = GameDataManager.Instance;

            Scene scene = SceneManager.GetActiveScene();

            if (scene.buildIndex == (int)GameConstants.Scene.StageSelect)
            {
                _gameDataManager.SetCurrentStageIndex((int)GameConstants.Stage.Stage_1);

                // ステージの生成
                SetStage();

                return;
            }

            SetStage();
        }

        /// <summary>
        /// ステージの生成
        /// </summary>
        private void SetStage(bool external_data = true)
        {
            if (external_data)
            {
                childTransform = gameObject.transform.Find("StageCreater");
                stageCreater = childTransform.GetComponent<StageCreater>();
                stageCreater.StageCreate();
                stageCreater.BGCreate();
                return;
            }

            int stage_index = _gameDataManager.GetCurrentStageIndex();

            Transform transform = GameObject.Find(GameConstants.MAIN_CAMERA).transform;

            Instantiate(_stageDatas[stage_index].StagePrefab, Vector3.zero, Quaternion.identity);
            Instantiate(_stageDatas[stage_index].StageBG, Vector3.zero, Quaternion.identity, transform);
        }

        /// <summary>
        /// ステージデータの取得
        /// </summary>
        /// <param name="stage_index"></param>
        /// <returns></returns>
        public StageData GetStageData(int stage_index)
        {
            DebugManager.LogMessage($"{stage_index}");

            return _stageDatas[stage_index];
        }
    }
}