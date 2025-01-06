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
            float delay_time = 1.0f;

            SceneManager.LoadScene(GameConstants.Scene.Loading.ToString());

            yield return new WaitForSeconds(delay_time);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
            asyncLoad.allowSceneActivation = false;

            // シーンがロードできるまで繰り返す
            while (progress < LOADING_PROGRESS_MAX)
            {
                progress = Mathf.Clamp01(asyncLoad.progress / LOADING_PROGRESS_MAX);

                yield return null;
            }

            yield return new WaitForSeconds(delay_time);

            asyncLoad.allowSceneActivation = true;

        }
    }
}