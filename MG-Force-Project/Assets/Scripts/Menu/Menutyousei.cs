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


                // マウスカーソルを表示にし、位置固定解除
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

                // マウスカーソルを非表示にし、位置を固定
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;


            }
        }
    }
}
