using UnityEngine;

namespace Game.StageScene
{
    /// <summary>
    /// ステージシーン管理クラス
    /// </summary>
    public class StageSceneController : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        
        // フェーズ
        private enum Phase
        {
            Reserve,
            Execution,
        }

        private bool isMenuScreen = false;

        // 現在のフェーズ
        private Phase currentPhase;

        private void Awake()
        {
            GameObject input = GameObject.Find(GameConstants.Object.INPUT);

            _sceneLoader = GameObject.Find(GameConstants.Object.STAGE_LOADER).GetComponent<SceneLoader>();

            //if (Input == null)
            //{
            //    SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
            //}
        }

        private void Update()
        {
            if (GoalEvent())
            {
                _sceneLoader.LoadScene(GameConstants.Scene.Clear.ToString());
            }

            if (currentPhase == Phase.Reserve)
            {

            }
            else if (currentPhase == Phase.Execution)
            {

            }
        }

        // 共通の処理
        private bool GoalEvent()
        {
            GameObject goal = GameObject.Find(GameConstants.Object.GOAL_CRYSTAL);

            if (goal == null) return false;

            CrystalController crystal = goal.GetComponent<CrystalController>();

            if (crystal == null) return false;

            return crystal.IsGoalEvent;
        }

        // 磁力を撃つフェーズの処理


        // 磁力を起動したフェーズの処理

        public bool GetIsMenu()
        {
            return isMenuScreen;
        }
    }
}