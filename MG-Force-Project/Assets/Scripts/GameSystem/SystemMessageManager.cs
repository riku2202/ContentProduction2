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
        [SerializeField] private GameObject _systemMessagePrefab;

        [SerializeField] private TextMeshProUGUI _messageText;

        private Coroutine _currentCoroutine = null;

        private void Awake()
        {
            _systemMessagePrefab.SetActive(false);
        }

        public void DrawMessage(string message)
        {
            if (_systemMessagePrefab == null) return;

            _systemMessagePrefab.SetActive(false);
            
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _messageText.text = message;

            _systemMessagePrefab.SetActive(true);

            _currentCoroutine = StartCoroutine(MessageStart());
        }

        private IEnumerator MessageStart()
        {
            float delty_time = 5.0f;

            yield return new WaitForSeconds(delty_time);

            _systemMessagePrefab.SetActive(false);
        }
    }
}