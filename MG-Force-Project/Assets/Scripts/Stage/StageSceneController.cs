using Game.Stage.Magnet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Stage
{
    /// <summary>
    /// ステージシーン管理クラス
    /// </summary>
    public class StageSceneController : MonoBehaviour
    {
        // フェーズ
        private enum Phase
        {
            Reserve,
            Execution,
        }

        // 現在のフェーズ
        private Phase currentPhase;

        private void Awake()
        {
            GameObject input = GameObject.Find(GameConstants.INPUT_MANAGER_OBJ);

            //if (Input == null)
            //{
            //    SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
            //}
        }

        private void Update()
        {
            if (GoalEvent())
            {
                SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
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
            GameObject goal = GameObject.Find("GoalItem");

            if (goal == null) return false;

            GoalManager goal_manager = goal.GetComponent<GoalManager>();

            if (goal_manager == null) return false;

            return goal_manager.IsGoalEvent;
        }

        // 磁力を撃つフェーズの処理


        // 磁力を起動したフェーズの処理

    }
}