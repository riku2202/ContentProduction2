using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class DoorController : MonoBehaviour
{
    [SerializeField] bool isOpen = false;   //ドアの開閉状態
    [SerializeField] float openAngle = 90f; //ドアの開く角度
    [SerializeField] float closeAngle = 0f; //ドアが閉まる角度
    [SerializeField] float speed = 2f;  //ドアの開閉速度

    private Quaternion openRotation;
    private Quaternion closeRotation;
    private bool isAnimating = false;

    // Start is called before the first frame update
    void Start()
    {
        openRotation = Quaternion.Euler(0,openAngle,0);
        closeRotation = Quaternion.Euler(0,closeAngle,0);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    ToggleDoor();
        //}

        if (isAnimating)
        {
            Quaternion targetRotation = isOpen ? openRotation : closeRotation;
            transform.localRotation = Quaternion.Slerp(transform.localRotation,targetRotation,
                Time.deltaTime * speed);

            //目的の角度に近づいたらアニメーションを停止
            if(Quaternion.Angle(transform.localRotation,targetRotation) < 0.1f)
            {
                transform.localRotation = targetRotation;
                isAnimating = false;
            }
        }
        
    }
   public void ToggleDoor()
    {
        isOpen = !isOpen;
        isAnimating = true;
    }
}
