using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] DoorController doorController; //ドアのコントローラを参照
    [SerializeField] string targetObjectName = "Switch";
    [SerializeField] string playerObjectName = "Player";
    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーがスイッチに触れたらドアを開閉
        if(other.name == playerObjectName && doorController != null)
        {
            doorController.ToggleDoor();
        }
        if (other.name == playerObjectName && doorController != null)
        {
            doorController.ToggleDoor();
        }

        // 警告削除用です
        targetObjectName = other.name;
        playerObjectName = other.name;
    }
}
