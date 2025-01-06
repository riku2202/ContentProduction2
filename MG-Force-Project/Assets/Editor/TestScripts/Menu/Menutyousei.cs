using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menutyousei : MonoBehaviour
{
    //[SerializeField] private GameObject MenuPanel;

    //private bool isMenuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        //if (MenuPanel != null)
        //{
        //    MenuPanel.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

#if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;
#endif



        }
    }

    private void ToogleMenu()
    {
        //if (MenuPanel != null)
        //{
        //    isMenuOpen = !isMenuOpen;

        //    MenuPanel.SetActive(isMenuOpen);

        //    Time.timeScale = isMenuOpen ? 0 : 1;

        //    Cursor.visible = isMenuOpen;
        //    Cursor.lockState = isMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
        //}
    }

    //private void OpenMenu()
    //{
    //    if (MenuPanel != null)
    //    {
    //        isMenuOpen = true;
    //        MenuPanel.SetActive(true);
    //        Time.timeScale = 0;
    //        Cursor.visible = true;
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //}

    //private void CloseMenu()
    //{
    //    if (MenuPanel != null)
    //    {
    //        isMenuOpen = false;
    //        MenuPanel.SetActive(false);
    //        Time.timeScale = 1;
    //        Cursor.visible = false;
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //}



}
