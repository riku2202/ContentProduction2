using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] DoorController doorController; //�h�A�̃R���g���[�����Q��
    [SerializeField] string targetObjectName = "Switch";
    [SerializeField] string playerObjectName = "Player";
    private void OnTriggerEnter(Collider other)
    {
        //�v���C���[���X�C�b�`�ɐG�ꂽ��h�A���J��
        if(other.name == playerObjectName && doorController != null)
        {
            doorController.ToggleDoor();
        }
        if (other.name == playerObjectName && doorController != null)
        {
            doorController.ToggleDoor();
        }

        // �x���폜�p�ł�
        targetObjectName = other.name;
        playerObjectName = other.name;
    }
}
