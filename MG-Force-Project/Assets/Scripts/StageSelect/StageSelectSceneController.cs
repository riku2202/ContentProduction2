using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Stage
{
    /// <summary>
    /// ステージ選択シーンの管理クラス
    /// </summary>
    public class StageSelectSceneController : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        private CrystalController _crystalController;

        private bool isMenuScreen = false;

        private void Awake()
        {
            GameObject input = GameObject.Find(GameConstants.Object.INPUT);

            if (input == null)
            {
                SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
            }
        }

        private void Start()
        {
            _crystalController = GameObject.Find(GameConstants.Object.GOAL_CRYSTAL).GetComponent<CrystalController>();
        }

        private void Update()
        {
            if (_crystalController.IsGoalEvent)
            {
                _sceneLoader.LoadScene(GameConstants.Scene.Clear.ToString());
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
            }
        }

        public bool GetIsMenu()
        {
            return isMenuScreen;
        }
    }
}