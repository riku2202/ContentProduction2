using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�V�[���̃J�����Ǘ��N���X
/// </summary>
public class CameraViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject Camera_PlayerView;

    [SerializeField]
    private GameObject Camera_FreeView;

    private enum CameraType 
    {
        PlayerView,
        FreeView,
    }

    private CameraType CurrentCameraType;

    private void Start()
    {
        CurrentCameraType  = CameraType.PlayerView;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (CurrentCameraType)
            //{

            //}
            //else
        }
    }
}