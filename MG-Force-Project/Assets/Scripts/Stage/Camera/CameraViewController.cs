using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage 
{
    /// <summary>
    /// �J�������[�h�̕ύX�����N���X
    /// </summary>
    public class CameraViewController : MonoBehaviour
    {
        [SerializeField]
        private GameObject Camera_PlayerView;

        [SerializeField]
        private GameObject Camera_FreeView;

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
        private void Start()
        {
            CurrentCameraType = CameraType.PlayerView;
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (CurrentCameraType == CameraType.PlayerView)
                {
                    Camera_PlayerView.SetActive(false);
                    Camera_FreeView.SetActive(true);

                    CurrentCameraType = CameraType.FreeView;
                }
                else
                {
                    Camera_PlayerView.SetActive(true);
                    Camera_FreeView.SetActive(false);

                    CurrentCameraType = CameraType.PlayerView;
                }
            }
        }
    }
}