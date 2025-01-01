using Game.GameSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Stage
{
    public class OptionKeyUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _startKey;
        [SerializeField] private GameObject _plusKey;
        [SerializeField] private GameObject _OptionKey;

        private string _currentDevice = null;

        private void Update()
        {
            if (Gamepad.current != null)
            {
                if (_currentDevice != Gamepad.current.displayName.ToLower())
                {
                    _currentDevice = Gamepad.current.displayName.ToLower();

                    SetKeyBar();
                }
            }
        }

        private void SetKeyBar()
        {
            if (_currentDevice.Contains(GameConstants.Input.Device.PLAY_STATION))
            {
                _startKey.SetActive(false);
                _plusKey.SetActive(false);
                _OptionKey.SetActive(true);
            }
            else if (_currentDevice.Contains(GameConstants.Input.Device.SWITCH))
            {
                _startKey.SetActive(false);
                _plusKey.SetActive(true);
                _OptionKey.SetActive(false);
            }
            else
            {
                _startKey.SetActive(true);
                _plusKey.SetActive(false);
                _OptionKey.SetActive(false);
            }
        }
    }
}