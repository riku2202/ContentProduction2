using Game.GameSystem;
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

        private InputHandler _inputHandler;

        #region -------- �V���O���g���̐ݒ� --------

        private static SEManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                // �j������Ȃ��悤�ɂ���
                DontDestroyOnLoad(gameObject);

                _audioSource = GetComponent<AudioSource>();

                _inputHandler = InputHandler.Instance;
                
                return;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        /// <summary>
        /// SE�̍Đ�(MenuSE)
        /// </summary>
        /// <param name="clip_index"></param>
        public void PlaySE(Menu clip_index)
        {
            AudioClip set_clip = _menuClips[(int)clip_index];

            _audioSource.PlayOneShot(set_clip);  // �Đ�
        }

        /// <summary>
        /// SE�̍Đ�(ActioinSE)
        /// </summary>
        /// <param name="clip_index"></param>
        public void PlaySE(Action clip_index)
        {
            AudioClip set_clip = _ActionClips[(int)clip_index];

            _audioSource.PlayOneShot(set_clip);  // �Đ�
        }

        private const float MIN_VOLUME = 0.0f;
        private const float MAX_VOLUME = 1.0f;
        private const float VARIABLE_VOLUME = 0.1f;

        public float VolumeChange(float volume)
        {
            if (volume != _audioSource.volume)
            {
                _audioSource.volume = volume;
            }
            else if (_inputHandler.IsActionPressed(GameConstants.Input.Action.MENU_LEFT_SELECT) &&
                _audioSource.volume != MIN_VOLUME)
            {
                _audioSource.volume -= VARIABLE_VOLUME;
            }
            else if (_inputHandler.IsActionPressed(GameConstants.Input.Action.MENU_RIGHT_SELECT) &&
                _audioSource.volume != MAX_VOLUME)
            {
                _audioSource.volume += VARIABLE_VOLUME;
            }

            return _audioSource.volume;
        }
    }
}