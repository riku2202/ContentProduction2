using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Game 
{
    public class SceneLoader : MonoBehaviour
    {
        #region -------- �V���O���g���̐ݒ� --------

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

        // �V�[�����[�h�̍ő�
        private float LOADING_PROGRESS_MAX = 0.9f;

        // �V�[�����[�h�̐i�s�x
        public float progress { get; private set; }

        /// <summary>
        /// �V�[���̃��[�h
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(string scene)
        {
            StartCoroutine(Loading(scene));
        }

        /// <summary>
        /// ���[�f�B���O����
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        private IEnumerator Loading(string scene)
        {
            float delty_time = 5.0f;

            SceneManager.LoadScene(GameConstants.Scene.Loading.ToString());

            yield return new WaitForSeconds(delty_time);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

            // �V�[�������[�h�ł���܂ŌJ��Ԃ�
            while (!asyncLoad.isDone)
            {
                progress = Mathf.Clamp01(asyncLoad.progress / LOADING_PROGRESS_MAX);

                yield return null;
            }

            yield return new WaitForSeconds(delty_time);
        }
    }
}