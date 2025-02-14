using Game.GameSystem;
using UnityEngine;

namespace Game.StageScene.Camera
{
    /// <summary>
    /// �J�������[�h�̕ύX�����N���X
    /// </summary>
    public class ViewCameraController : MonoBehaviour
    {
        private PlayerViewCameraController _playerCamera;

        private FreeViewCameraController _freeCamera;

        private InputHandler _input;

        // �J�����^�C�v
        private enum CameraType
        {
            PLAYER_VIEW,
            FREE_VIEW,
        }

        // ���݂̃J�����^�C�v
        private CameraType _currentCameraType;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            _input = InputHandler.Instance;

            _playerCamera = GetComponent<PlayerViewCameraController>();
            _freeCamera = GetComponent<FreeViewCameraController>();

            _currentCameraType = CameraType.PLAYER_VIEW;

            ChangeActive();
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            //if (_input.IsActionPressed(InputConstants.Action.VIEW_MODE_START) ||
            //    _input.IsActionPressed(InputConstants.Action.VIEW_MODE_END))
            //{
            //    _currentCameraType = (_currentCameraType != CameraType.PLAYER_VIEW) ? CameraType.PLAYER_VIEW : CameraType.FREE_VIEW;

            //    ChangeActive();
            //}
        }

        private void ChangeActive()
        {
            if (_currentCameraType == CameraType.PLAYER_VIEW)
            {
                _playerCamera.enabled = true;
                _freeCamera.enabled = false;
            }
            else
            {
                _playerCamera.enabled = false;
                _freeCamera.enabled = true;
            }
        }
    }
}