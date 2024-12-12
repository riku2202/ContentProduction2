using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetgameOnKeyPress : MonoBehaviour
{
    //リセットに使用するキー
    [SerializeField] KeyCode resetKey = KeyCode.R;
   
    // Update is called once per frame
    void Update()
    {
        //指定したキーが押されたときリセット
        if(Input.GetKeyDown(resetKey))
        {
            ResetScene();
        }
    }
    void ResetScene()
    {
        //現在のシーンを再読み込みしてリセット
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
