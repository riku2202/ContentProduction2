using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game 
{
    public class BGMManager : MonoBehaviour
    {
        private enum BGM 
        {
            TITLE,
            SELECT_STAGE,
            STAGE,
            CLEAR,
            ALL_CLEAR,

            MAX_BGM,
        }

        // �����𗬂�AudioSouurce
        private AudioSource _audioSource;

        // ��������
        [SerializeField]
        private AudioClip[] _audioClips = new AudioClip[(int)BGM.MAX_BGM];

        // �Ή�����BGM�}�b�s���O�p�̎���
        private static readonly Dictionary<int, BGM> _sceneBGM = new Dictionary<int, BGM> 
        {
            {(int)GameConstants.Scene.Title, BGM.TITLE },
            {(int) GameConstants.Scene.StageSelect, BGM.SELECT_STAGE },
            {(int) GameConstants.Scene.Stage, BGM.STAGE },
            {(int) GameConstants.Scene.Clear, BGM.CLEAR },
            {(int) GameConstants.Scene.Credits, BGM.ALL_CLEAR },
        };

        #region -------- �V���O���g���̐ݒ� --------

        private static BGMManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                // �j������Ȃ��悤�ɂ���
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

        #region -------- �C�x���g���� --------

        /// <summary>
        /// �V�[����ǂݍ��񂾎��̃C�x���g�ǉ�
        /// </summary>
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        /// <summary>
        /// �V�[����ǂݍ��񂾎��̃C�x���g
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            PlayBGM();  // �Đ�
        }

        /// <summary>
        /// �V�[����ǂݍ��񂾎��̃C�x���g�̍폜
        /// </summary>
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        #endregion

        private void Start()
        {
            PlayBGM();
        }

        /// <summary>
        /// BGM�̍Đ�����
        /// </summary>
        private void PlayBGM()
        {
            // ���݂̃V�[���擾
            Scene scene = SceneManager.GetActiveScene();

            // BGM�̃Z�b�g
            _audioSource.clip = SetBGM(scene.buildIndex);

            // null�`�F�b�N
            if (_audioSource.clip != null)
            {
                _audioSource.Play();  // �Đ�
            }
            else
            {
                // �G���[�\��(Debug�p)
                DebugManager.LogMessage($"{scene.name}��BGM���Đ��ł��܂���ł���", DebugManager.MessageType.Error);
            }
        }

        /// <summary>
        /// BGM�̃Z�b�g
        /// </summary>
        /// <param name="scene_index"></param>
        /// <returns></returns>
        private AudioClip SetBGM(int scene_index)
        {
            if (_sceneBGM.TryGetValue(scene_index, out BGM set_clip))
            {
                // �͈͊O�`�F�b�N
                if (set_clip >= BGM.MAX_BGM) return null;

                return _audioClips[(int)set_clip];
            }

            return null;
        }
    }
}