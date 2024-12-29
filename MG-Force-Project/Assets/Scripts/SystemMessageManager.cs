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
        #region -------- シングルトンの設定 --------

        // シングルトンインスタンス
        public static SystemMessageManager instance { get; private set; }

        private void Awake()
        {
            // シングルトンの設定
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

        #region -------- 参照設定用イベント処理 --------

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
                DebugManager.LogMessage("システムメッセージを表示できません。デバック時はTitleSceneから始めてください",DebugManager.MessageType.Error);
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