using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class DoorController : MonoBehaviour
{
    [SerializeField] bool isOpen = false;   //�h�A�̊J���
    [SerializeField] float openAngle = 90f; //�h�A�̊J���p�x
    [SerializeField] float closeAngle = 0f; //�h�A���܂�p�x
    [SerializeField] float speed = 2f;  //�h�A�̊J���x

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

            //�ړI�̊p�x�ɋ߂Â�����A�j���[�V�������~
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
