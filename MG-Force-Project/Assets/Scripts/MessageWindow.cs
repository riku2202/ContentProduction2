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
        #region -------- �V���O���g���̐ݒ� --------

        private static MessageWindow instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                // �j������Ȃ��悤�ɂ���
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
            {ErrorCode.E2001, "E2001 : Failed to start properly. Please restart." },
            {ErrorCode.E2002, "E2002 : An unexpected error has occurred. Please restart." },
            {ErrorCode.E3001, "E3001 : Failed to retrieve data properly. Please try again or restart." },
            {ErrorCode.E4001, "E4001 : A critical error has occurred. Please try again." }
        };

        [SerializeField] private GameObject _meesageBox;
        [SerializeField] private TextMeshProUGUI _message;

        private ButtonType _buttonType;

        public int ErrorWindow(ErrorCode error_code)
        {
            _buttonType = ButtonType.NONE;

            // �Q�[���̎��Ԃ��~
            Time.timeScale = 0;

            _meesageBox.SetActive(true);

            ShowMessageBox(error_code);

            StartCoroutine(MessageBoxActiving());

            return (int)_buttonType;
        }

        private IEnumerator MessageBoxActiving()
        {
            // ���炩�̃A�N�V����������܂ŌJ��Ԃ�
            while (_buttonType == ButtonType.NONE)
            {
                yield return null;
            }

            _meesageBox.SetActive(false);

            // �Q�[���̍ĊJ
            Time.timeScale = 1;
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