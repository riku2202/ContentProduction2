using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class DoorController : MonoBehaviour
{
<<<<<<< HEAD
   [SerializeField] float defaultY;     // 扉の初期のY座標
   [SerializeField] float openY = 5f;   // 扉のオープン時のY座標
   [SerializeField] float speed = 10f;   // 扉の開閉のスピード
=======
    float defaultY;     // 扉の初期のY座標
    float openY = 50f;   // 扉のオープン時のY座標
    float speed = 10f;   // 扉の開閉のスピード
>>>>>>> 270ba7cecbb94de8f29a654e985b22da7bcefcc4

    public bool isOpen; // 扉を開けるか閉めるかのフラグ

    void Start()
    {
        defaultY = transform.position.y;
    }

    void Update()
    {
        if (isOpen && transform.position.y < openY)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (!isOpen && transform.position.y > defaultY)
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }
    }
}