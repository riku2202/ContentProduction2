using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using Game.GameSystem;
using UnityEngine.InputSystem;

namespace Game 
{
    public class SystemMessageManager : MonoBehaviour
    {
        #region -------- �V���O���g���̐ݒ� --------

        // �V���O���g���C���X�^���X
        public static SystemMessageManager instance { get; private set; }

        private void Awake()
        {
            // �V���O���g���̐ݒ�
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            InitReference();
        }

        #endregion

        private GameObject _systemMessagePrefab;

        private GameObject _systemMessageText;

        private TextMeshProUGUI _messageText;

        #region -------- �Q�Ɛݒ�p�C�x���g���� --------

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            InitReference();
        }

        #endregion

        private void InitReference()
        {
            _systemMessagePrefab = GameObject.Find(GameConstants.Object.SYSTEM_MESSAGE_OBJ);

            if (_systemMessagePrefab == null)
            {
                DebugManager.LogMessage("�V�X�e�����b�Z�[�W��\���ł��܂���B�f�o�b�N����TitleScene����n�߂Ă�������",DebugManager.MessageType.Error);
                return;
            }

            _systemMessageText = _systemMessagePrefab.transform.Find(GameConstants.Object.SYSTEM_MESSAGE_TEXT).gameObject;

            _messageText = _systemMessageText.GetComponent<TextMeshProUGUI>();

            if (_systemMessagePrefab != null)
            {
                _systemMessagePrefab.SetActive(false);
            }
        }

        public void DrawMessage(string message)
        {
            if (_systemMessagePrefab == null) return;

            _systemMessagePrefab.SetActive(true);

            _messageText.text = message;

            StartCoroutine(MessageStart());
        }

        IEnumerator MessageStart()
        {
            float delty_time = 5.0f;

            yield return new WaitForSeconds(delty_time);

            _systemMessagePrefab.SetActive(false);
        }
    }
}