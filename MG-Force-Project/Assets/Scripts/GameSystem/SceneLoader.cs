using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using Unity.VisualScripting;

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

        [SerializeField] private GameObject _brackOut;

        private bool _canLoading = false;

        /// <summary>
        /// シーンのロード
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(string scene)
        {
            _canLoading = true;

            StartCoroutine(OnLoading(scene));
        }

        private IEnumerator OnLoading(string scene)
        {
            yield return StartCoroutine(BrackOut());

            yield return StartCoroutine(Loading(scene));
        }

        private IEnumerator BrackOut()
        {
            GameObject obj = Instantiate(_brackOut);

            GameObject canvas = GameObject.Find("Canvas");

            obj.transform.SetParent(canvas.transform, false);

            Image Sr = obj.GetComponent<Image>();

            const float TIME_INTERVAL = 0.01f;  // 間隔
            const float MIN_ALPHA = 0.0f;
            const float MAX_ALPHA = 1.0f;

            // 初期化
            float CurrentAlpha = MIN_ALPHA;
            // 不透明化速度
            float BrackOutInterval = 0.01f;

            /* ------ ブラックアウト ------ */

            while (true)
            {
                if (Sr != null)
                {
                    Sr.color = new Color(Sr.color.r, Sr.color.g, Sr.color.b, CurrentAlpha);
                }

                yield return new WaitForSeconds(TIME_INTERVAL);

                CurrentAlpha += BrackOutInterval;

                if (CurrentAlpha >= MAX_ALPHA) break;
            }
        }


        /// <summary>
        /// ローディング処理
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        private IEnumerator Loading(string scene)
        {
            float delay_time = 1.0f;

            if (_canLoading)
            {
                _canLoading = false;

                SceneManager.LoadScene(GameConstants.Scene.Loading.ToString());

                yield return new WaitForSeconds(delay_time);

                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

                if (asyncLoad != null)
                {
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
            else
            {
                yield return new WaitForSeconds(delay_time);
            }
        }
    }
}