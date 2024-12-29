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
        [SerializeField]
        private GameObject _systemMessagePrefab;

        [SerializeField]
        private TextMeshProUGUI _messageText;

        private void Awake()
        {
            _systemMessagePrefab.SetActive(false);
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
            float delty_time = 4.0f;

            yield return new WaitForSeconds(delty_time);

            _systemMessagePrefab.SetActive(false);
        }
    }
}