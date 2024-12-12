using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SEManager : MonoBehaviour
    {
        #region -------- シングルトンの設定 --------

        private static SEManager instance;

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

        [SerializeField]
        AudioSource seSource;
        [SerializeField]
        AudioClip seClip;   //SEファイルを入れる
        void PlaySE()
        {
            //SEを一回再生
            seSource.PlayOneShot(seClip);
        }
    }
}