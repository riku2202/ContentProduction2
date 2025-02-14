using Game.GameSystem;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

namespace Game
{
    public class MessageWindow : MonoBehaviour
    {
        #region -------- シングルトンの設定 --------

        private static MessageWindow instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                // 破棄されないようにする
                DontDestroyOnLoad(gameObject);

                _meesageBox.SetActive(false);

                return;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static MessageWindow GetInstance()
        {
            return instance;
        }

        #endregion

        public enum ButtonType
        {
            NONE,
            OK,
            CANCEL,
        }

        private Dictionary<ErrorCode, string> _errorCode = new Dictionary<ErrorCode, string>
        {
            {ErrorCode.E0000, "E0000 : ErrorCode TestMessage" },
            {ErrorCode.E2001, "E2001 : Failed to start properly\nPlease restart" },
            {ErrorCode.E2002, "E2002 : An unexpected error has occurred\nPlease restart" },
            {ErrorCode.E3001, "E3001 : Failed to retrieve data properly\nPlease try again or restart" },
            {ErrorCode.E4001, "E4001 : A critical error has occurred\nPlease try again" },
            {ErrorCode.E5001, "E5001 : An error occurred during stage generation\nPlease restart the game and try again\nIf this issue persists, please contact the developer" },
        };

        [SerializeField] private GameObject _meesageBox;
        [SerializeField] private TextMeshProUGUI _message;

        private ButtonType _buttonType;

        public int ErrorWindow(ErrorCode error_code)
        {
            _buttonType = ButtonType.NONE;

            // ゲームの時間を停止
            Time.timeScale = 0;

            _meesageBox.SetActive(true);

            ShowMessageBox(error_code);

            StartCoroutine(MessageBoxActiving());

            return (int)_buttonType;
        }

        private IEnumerator MessageBoxActiving()
        {
            // 何らかのアクションがあるまで繰り返す
            while (_buttonType == ButtonType.NONE)
            {
                yield return null;
            }

            // ゲームの再開
            Time.timeScale = 1;

            _meesageBox.SetActive(false);
        }

        private void ShowMessageBox(ErrorCode error_code)
        {
            _message.text = $"{_errorCode[error_code]}";
        }

        public void CloseMessageButton(int button_type = 0)
        {
            _buttonType = (ButtonType)button_type;
        }
    }
}