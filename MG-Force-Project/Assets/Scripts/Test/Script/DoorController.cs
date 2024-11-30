using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class DoorController : MonoBehaviour
{
<<<<<<< HEAD
   [SerializeField] float defaultY;     // ���̏�����Y���W
   [SerializeField] float openY = 5f;   // ���̃I�[�v������Y���W
   [SerializeField] float speed = 10f;   // ���̊J�̃X�s�[�h
=======
    float defaultY;     // ���̏�����Y���W
    float openY = 50f;   // ���̃I�[�v������Y���W
    float speed = 10f;   // ���̊J�̃X�s�[�h
>>>>>>> 270ba7cecbb94de8f29a654e985b22da7bcefcc4

    public bool isOpen; // �����J���邩�߂邩�̃t���O

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