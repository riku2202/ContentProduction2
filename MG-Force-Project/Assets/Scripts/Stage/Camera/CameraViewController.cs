using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage 
{
    /// <summary>
    /// カメラモードの変更処理クラス
    /// </summary>
    public class CameraViewController : MonoBehaviour
    {
        [SerializeField]
        private GameObject Camera_PlayerView;

        [SerializeField]
        private GameObject Camera_FreeView;

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
        private void Start()
        {
            CurrentCameraType = CameraType.PlayerView;
        }

        /// <summary>
        /// 更新処理
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