using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    GameObject door;    //ドアオブジェクト
    [SerializeField]
    Vector3 openPosition;//開いた時の位置
    [SerializeField]
    float openSpeed = 2f;//開くスピード

    private Vector3 originalPosition;
    private bool isOpen = false;

    void Start()
    {
        originalPosition = door.transform.position;   //初期位置を記録
    }
    private void Update()
    {
        //ドアが開く
        if (isOpen)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, openPosition, Time.deltaTime * openSpeed);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //他のCubeと衝突した場合
        if(collision.gameObject.CompareTag("KeyCube"))
        {
            Debug.Log("Cubeが衝突しました。ドアを開けます!");
            isOpen = true;
        }
    }
}