using UnityEngine;

namespace Game
{
    public class SEManager : MonoBehaviour
    {
        public enum Menu
        {
            MAX_SE,
        }

        public enum Action
        {
            MAX_SE,
        }

        AudioSource _audioSource;

        [SerializeField]
        private AudioClip[] _menuClips = new AudioClip[(int)Menu.MAX_SE];

        [SerializeField]
        private AudioClip[] _ActionClips = new AudioClip[(int)Action.MAX_SE];

        #region -------- シングルトンの設定 --------

        private static SEManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                // 破棄されないようにする
                DontDestroyOnLoad(gameObject);

                _audioSource = GetComponent<AudioSource>();

                return;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        /// <summary>
        /// SEの再生(MenuSE)
        /// </summary>
        /// <param name="clip_index"></param>
        public void PlaySE(Menu clip_index)
        {
            AudioClip set_clip = _menuClips[(int)clip_index];

            _audioSource.PlayOneShot(set_clip);  // 再生
        }

        /// <summary>
        /// SEの再生(ActioinSE)
        /// </summary>
        /// <param name="clip_index"></param>
        public void PlaySE(Action clip_index)
        {
            AudioClip set_clip = _ActionClips[(int)clip_index];

            _audioSource.PlayOneShot(set_clip);  // 再生
        }
    }
}