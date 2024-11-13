using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] DoorController doorController; //�h�A�̃R���g���[�����Q��

    private void OnTriggerEnter(Collider other)
    {
        //�v���C���[���X�C�b�`�ɐG�ꂽ��h�A���J��
        if(other.CompareTag("player"))
        {
            doorController.ToggleDoors();
        }
    }
}
