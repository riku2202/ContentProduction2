using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.LoadingScene
{
    public class LoadingSceneController : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        [SerializeField]
        private Image _progressGage;

        private void Awake()
        {
            _sceneLoader = SceneLoader.Instance;

            if (_sceneLoader == null) SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
        }

        private void Update()
        {
            _progressGage.fillAmount = _sceneLoader.progress;
        }
    }
}