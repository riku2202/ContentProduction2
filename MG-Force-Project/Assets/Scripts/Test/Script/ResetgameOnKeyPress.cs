using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetgameOnKeyPress : MonoBehaviour
{
    //���Z�b�g�Ɏg�p����L�[
    [SerializeField] KeyCode resetKey = KeyCode.R;
   
    // Update is called once per frame
    void Update()
    {
        //�w�肵���L�[�������ꂽ�Ƃ����Z�b�g
        if(Input.GetKeyDown(resetKey))
        {
            ResetScene();
        }
    }
    void ResetScene()
    {
        //���݂̃V�[�����ēǂݍ��݂��ă��Z�b�g
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
