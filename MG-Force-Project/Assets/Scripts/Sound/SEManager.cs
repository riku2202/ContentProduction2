using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SEManager : MonoBehaviour
    {
        #region -------- �V���O���g���̐ݒ� --------

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
        AudioClip seClip;   //SE�t�@�C��������
        void PlaySE()
        {
            //SE�����Đ�
            seSource.PlayOneShot(seClip);
        }
    }
}