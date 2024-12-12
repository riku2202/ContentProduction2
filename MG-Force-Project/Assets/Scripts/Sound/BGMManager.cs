using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game 
{
    public class BGMManager : MonoBehaviour
    {
        #region -------- �V���O���g���̐ݒ� --------

        private static BGMManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                DontDestroyOnLoad(gameObject);

                return;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        private AudioSource bgmSource;

        private enum BGM
        {
            Title,
            StageSelect,
            Stage,
            Clear,
            AllClear
        }

        [SerializeField]
        private AudioClip[] bgmSound;

        private void Start()
        {
            StartBGM();
        }

        /// <summary>
        /// �J�n����
        /// </summary>
        private void StartBGM()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            switch (currentScene.buildIndex)
            {
                case (int)GameConstants.Scene.Title:
                    bgmSource.clip = bgmSound[(int)BGM.Title];
                    break;
                case (int)GameConstants.Scene.StageSelect:
                    bgmSource.clip = bgmSound[(int)BGM.StageSelect];
                    break;

                case (int)GameConstants.Scene.Stage:
                    bgmSource.clip = bgmSound[(int)BGM.Stage];
                    break;

                case (int)GameConstants.Scene.Clear:
                    bgmSource.clip = bgmSound[(int)BGM.Clear];
                    break;

                case (int)GameConstants.Scene.Credits:
                    bgmSource.clip = bgmSound[(int)BGM.AllClear];
                    break;
            }

            //�Đ�
            bgmSource.Play();
        }

        ///// <summary>
        ///// ��~����
        ///// </summary>
        //private void StopBGM()
        //{
        //    //��~
        //    bgmSource.Stop();
        //}
    }
}