using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu; // PauseMenu�I�u�W�F�N�g

    public GameObject firstSelectedButton; // �ŏ��ɑI�������{�^��

    private bool isPaused = false; // ���j���[�̕\����Ԃ��Ǘ�


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �G�X�P�[�v�L�[�������ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }


    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        // ���Ԃ̐i�s�𐧌�i�|�[�Y���͒�~�j
        Time.timeScale = isPaused ? 0f : 1f;

        if (isPaused)
        {
            // �ŏ��̃{�^����I��
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
        else
        {
            // ���j���[������Ƃ��ɑI������
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    // �Q�[�����I������
    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
