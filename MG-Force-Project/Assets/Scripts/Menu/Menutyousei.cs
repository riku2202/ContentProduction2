using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menutyousei : MonoBehaviour
{
    [SerializeField] GameObject MenuObject;

    bool menuzyoutai;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuObject  == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape) == true)
            {
                MenuObject.gameObject.SetActive(true);
                menuzyoutai = true;


                // �}�E�X�J�[�\����\���ɂ��A�ʒu�Œ����
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;


            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) == false)
            {
                MenuObject.gameObject.SetActive(false);
                menuzyoutai = false;

                // �}�E�X�J�[�\�����\���ɂ��A�ʒu���Œ�
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;


            }
        }
    }
}
