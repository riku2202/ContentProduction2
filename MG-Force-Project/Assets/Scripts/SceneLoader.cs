using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Game 
{
    public class SceneLoader : MonoBehaviour
    {
        #region -------- シングルトンの設定 --------

        public static SceneLoader Instance {  get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        #endregion

        // シーンロードの最大
        private float LOADING_PROGRESS_MAX = 0.9f;

        // シーンロードの進行度
        public float progress { get; private set; }

        /// <summary>
        /// シーンのロード
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(string scene)
        {
            StartCoroutine(Loading(scene));
        }

        /// <summary>
        /// ローディング処理
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        private IEnumerator Loading(string scene)
        {
            SceneManager.LoadScene(GameConstants.Scene.Loading.ToString());

            yield return new WaitForSeconds(3f);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

            // シーンがロードできるまで繰り返す
            while (!asyncLoad.isDone)
            {
                progress = Mathf.Clamp01(asyncLoad.progress / LOADING_PROGRESS_MAX);

                yield return null;
            }
        }
    }
}