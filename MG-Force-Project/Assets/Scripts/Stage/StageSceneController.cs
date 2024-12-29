using Game.Stage.Magnet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Stage
{
    /// <summary>
    /// �X�e�[�W�V�[���Ǘ��N���X
    /// </summary>
    public class StageSceneController : MonoBehaviour
    {
        // �t�F�[�Y
        private enum Phase
        {
            Reserve,
            Execution,
        }

        private bool isMenuScreen = false;

        // ���݂̃t�F�[�Y
        private Phase currentPhase;

        private void Awake()
        {
            GameObject input = GameObject.Find(GameConstants.Object.INPUT);

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

        // ���ʂ̏���
        private bool GoalEvent()
        {
            GameObject goal = GameObject.Find("GoalItem");

            if (goal == null) return false;

            GoalManager goal_manager = goal.GetComponent<GoalManager>();

            if (goal_manager == null) return false;

            return goal_manager.IsGoalEvent;
        }

        // ���͂����t�F�[�Y�̏���


        // ���͂��N�������t�F�[�Y�̏���

        public bool GetIsMenu()
        {
            return isMenuScreen;
        }
    }
}