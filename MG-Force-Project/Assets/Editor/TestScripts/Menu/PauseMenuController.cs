using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu; // PauseMenuオブジェクト

    public GameObject firstSelectedButton; // 最初に選択されるボタン

    private bool isPaused = false; // メニューの表示状態を管理


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // エスケープキーが押されたとき
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }


    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        // 時間の進行を制御（ポーズ時は停止）
        Time.timeScale = isPaused ? 0f : 1f;

        if (isPaused)
        {
            // 最初のボタンを選択
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
        else
        {
            // メニューを閉じたときに選択解除
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    // ゲームを終了する
    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
