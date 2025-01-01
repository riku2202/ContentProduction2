
using Game.GameSystem;
using UnityEngine;

namespace Game.Stage
{
    public class UIManager : MonoBehaviour
    {
        private InputHandler _inputHandler;

        [SerializeField] private GameObject _gamepadKeyBar;
        [SerializeField] private GameObject _gamepad_2KeyBar;
        [SerializeField] private GameObject _keyboardKeyBar;

        private string _currentDevice;

        private void Start()
        {
            _inputHandler = InputHandler.Instance;

            _currentDevice = _inputHandler.GetControlScheme();

            SetKeyBar();
        }

        private void Update()
        {
            if (_currentDevice == _inputHandler.GetControlScheme()) return;
            
            SetKeyBar();

            _currentDevice = _inputHandler.GetControlScheme();
        }

        private void SetKeyBar()
        {
            switch (_currentDevice)
            {
                case GameConstants.Input.ActionDevice.GAMEPAD:
                    _gamepadKeyBar.SetActive(true);
                    _gamepad_2KeyBar.SetActive(false);
                    _keyboardKeyBar.SetActive(false);
                    break;

                case GameConstants.Input.ActionDevice.GAMEPAD_2:
                    _gamepadKeyBar.SetActive(false);
                    _gamepad_2KeyBar.SetActive(true);
                    _keyboardKeyBar.SetActive(false);
                    break;

                case GameConstants.Input.ActionDevice.KEY_MOUSE:
                    _gamepadKeyBar.SetActive(false);
                    _gamepad_2KeyBar.SetActive(false);
                    _keyboardKeyBar.SetActive(true);
                    break;
            }
        }
    }
}