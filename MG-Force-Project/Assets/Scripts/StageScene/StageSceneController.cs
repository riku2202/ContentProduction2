using UnityEngine;

namespace Game.StageScene
{
    /// <summary>
    /// �X�e�[�W�V�[���Ǘ��N���X
    /// </summary>
    public class StageSceneController : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        
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

        // ���ʂ̏���
        private bool GoalEvent()
        {
            GameObject goal = GameObject.Find(GameConstants.Object.GOAL_CRYSTAL);

            if (goal == null) return false;

            CrystalController crystal = goal.GetComponent<CrystalController>();

            if (crystal == null) return false;

            return crystal.IsGoalEvent;
        }

        // ���͂����t�F�[�Y�̏���


        // ���͂��N�������t�F�[�Y�̏���

        public bool GetIsMenu()
        {
            return isMenuScreen;
        }
    }
}