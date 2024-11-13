using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] DoorController doorController; //ドアのコントローラを参照

    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーがスイッチに触れたらドアを開閉
        if(other.CompareTag("player"))
        {
            doorController.ToggleDoors();
        }
    }
}
