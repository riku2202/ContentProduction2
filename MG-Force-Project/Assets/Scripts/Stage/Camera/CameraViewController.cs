using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Camera
{
    /// <summary>
    /// カメラモードの変更処理クラス
    /// </summary>
    public class CameraViewController : MonoBehaviour
    {
        [SerializeField]
        private GameObject cameraPlayerView;

        [SerializeField]
        private GameObject cameraFreeView;

        // カメラタイプ
        private enum CameraType
        {
            PlayerView,
            FreeView,
        }

        // 現在のカメラタイプ
        private CameraType CurrentCameraType;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            CurrentCameraType = CameraType.PlayerView;

            ChangeActive();
        }

        /// <summary>
        /// 更新処理
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