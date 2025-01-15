using Game.GameSystem;
using Game.Title;
using UnityEngine;
using System.Collections;

namespace Game 
{
    public class GameDataEraseController : MonoBehaviour
    {
        private InputHandler _input;

        // �Q�[���f�[�^�Ǘ��N���X�̌Ăяo��
        private GameDataManager _manager = GameDataManager.Instance;

        private enum ButtonType
        {
            NONE = -1,

            YES,
            NO,
            FINISH,
            MAX,
        }

        [SerializeField] private GameObject _checkWinodow;

        [SerializeField] private GameObject _confirmWindow;

        [SerializeField] private GameObject[] _buttonObjects = new GameObject[(int)ButtonType.MAX];

        private ButtonType _currentButton = ButtonType.NONE;

        private Vector3 _targetButton = new Vector3(1.2f, 1.2f, 1.2f);

        private Vector3 _offTargetButton = new Vector3(1.0f, 1.0f, 1.0f);

        public bool isActive { get; private set; }

        private void Awake()
        {
            _input = InputHandler.Instance;

            // �Q�[���f�[�^�̐���
            _manager.NewGameData();
        }

        private void OnEnable()
        {
            isActive = true;
            _checkWinodow.SetActive(true);
            _confirmWindow.SetActive(false);
        }

        private void Update()
        {
            if (_input.IsActionPressed(InputConstants.Action.MENU_DECISION))
            {
                DataEraseDecision((int)_currentButton);
            }

            if (_input.IsActionPressed(InputConstants.Action.MENU_LEFT_SELECT))
            {
                if (_currentButton == ButtonType.NONE)
                {
                    _currentButton = ButtonType.NO;
                }
                else if (_currentButton != ButtonType.YES)
                {
                    _currentButton--;
                }
            }
            else if (_input.IsActionPressed(InputConstants.Action.MENU_RIGHT_SELECT))
            {
                if (_currentButton == ButtonType.NONE)
                {
                    _currentButton = ButtonType.NO;
                }
                else if (_currentButton != ButtonType.NO)
                {
                    _currentButton++;
                }
            }

            ButtonScaleUpdate();
        }

        public void DataEraseDecision(int button_index)
        {
            if (button_index == (int)ButtonType.NONE)
            {
                _currentButton = ButtonType.NO;
            }
            else if (button_index == (int)ButtonType.YES)
            {
                StartCoroutine(StartDataErase());
            }
            else
            {
                isActive = false;
            }
        }

        private void ButtonScaleUpdate()
        {
            for (int i = (int)ButtonType.YES; i < (int)ButtonType.MAX; i++)
            {
                if ((int)_currentButton == i)
                {
                    _buttonObjects[i].transform.localScale = _targetButton;
                }
                else
                {
                    _buttonObjects[i].transform.localScale = _offTargetButton;
                }
            }
        }

        private IEnumerator StartDataErase()
        {
            float delty_time = 2.0f;

            yield return new WaitForSeconds(delty_time);

            GameDataErase();

            _checkWinodow.SetActive(false);
            _confirmWindow.SetActive(true);

            while (isActive)
            {
                if (_input.IsActionPressed(InputConstants.Action.MENU_DECISION) ||
                    _input.IsActionPressed(InputConstants.Action.MENU_BACK))
                {
                    isActive = false;
                }

                yield return null;
            }
        }
        
        /// <summary>
        /// �Q�[���f�[�^�̍폜
        /// </summary>
        private void GameDataErase()
        {
            // �Q�[���f�[�^�̃��Z�b�g
            _manager.ReSetGameData();

#if UNITY_EDITOR // UnityEditor�ł̎��s��(�f�o�b�N�p)

            // �Q�[���f�[�^�̃Z�[�u(�f�o�b�N���̓f�[�^���㏑������)
            SaveSystem.SaveManager();

#else // ���ۂ̃r���h�Ŏ��s��

            // �Q�[���f�[�^�̍폜
            SaveSystem.DeleteGameData();
#endif
        }
    }
}