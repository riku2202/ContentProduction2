using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Stage
{
    /// <summary>
    /// �X�e�[�W�I���V�[���̊Ǘ��N���X
    /// </summary>
    public class StageSelectSceneController : MonoBehaviour
    {
        private bool isMenuScreen = false;

        private void Awake()
        {
            GameObject input = GameObject.Find(GameConstants.Object.INPUT);

            if (input == null)
            {
                SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
            }
        }

        private void Update()
        {
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