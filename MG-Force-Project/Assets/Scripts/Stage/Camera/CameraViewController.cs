using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Camera
{
    /// <summary>
    /// �J�������[�h�̕ύX�����N���X
    /// </summary>
    public class CameraViewController : MonoBehaviour
    {
        [SerializeField]
        private GameObject cameraPlayerView;

        [SerializeField]
        private GameObject cameraFreeView;

        // �J�����^�C�v
        private enum CameraType
        {
            PlayerView,
            FreeView,
        }

        // ���݂̃J�����^�C�v
        private CameraType CurrentCameraType;

        /// <summary>
        /// ����������
        /// </summary>
        private void Awake()
        {
            CurrentCameraType = CameraType.PlayerView;

            ChangeActive();
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (CurrentCameraType != CameraType.PlayerView)
                {
                    CurrentCameraType = CameraType.PlayerView;
                }
                else
                {
                    CurrentCameraType = CameraType.FreeView;
                }

                ChangeActive();
            }
        }

        private void ChangeActive()
        {
            if (CurrentCameraType == CameraType.PlayerView)
            {
                cameraPlayerView.SetActive(true);
                cameraFreeView.SetActive(false);
            }
            else
            {
                cameraPlayerView.SetActive(false);
                cameraFreeView.SetActive(true);
            }
        }
    }
}