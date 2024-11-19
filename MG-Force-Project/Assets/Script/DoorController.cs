using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    GameObject door;    //�h�A�I�u�W�F�N�g
    [SerializeField]
    Vector3 openPosition;//�J�������̈ʒu
    [SerializeField]
    float openSpeed = 2f;//�J���X�s�[�h

    private Vector3 originalPosition;
    private bool isOpen = false;

    void Start()
    {
        originalPosition = door.transform.position;   //�����ʒu���L�^
    }
    private void Update()
    {
        //�h�A���J��
        if (isOpen)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, openPosition, Time.deltaTime * openSpeed);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //����Cube�ƏՓ˂����ꍇ
        if(collision.gameObject.CompareTag("KeyCube"))
        {
            Debug.Log("Cube���Փ˂��܂����B�h�A���J���܂�!");
            isOpen = true;
        }
    }
}