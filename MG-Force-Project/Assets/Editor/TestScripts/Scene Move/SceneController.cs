using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    Title,
    Settings,
    Options,
    Loading,
    StageSelect,
    Stage,
    Clear,
    Credits
}
public class SceneController : MonoBehaviour
{
    [SerializeField] private GameScene currentScene;

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("This is the last scene!");
        }
    }
    //特定のシーンをロード
    public void LoadScene()
    {
        SceneManager.LoadScene((int)currentScene);
    }
    //タイトルシーンに戻る
    public void LoadTitleScene()
    {
        SceneManager.LoadScene((int)GameScene.Title);
    }
    //ステージクリア時の処理
    public void OnStageGoal()
    {
        SceneManager.LoadScene((int)GameScene.Clear);
    }
    //ゲーム終了
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("ゲーム終了");
    }
    //マウスクリックｗｐ監視
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LoadNextScene();
        }
    }
}
