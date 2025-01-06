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
    //����̃V�[�������[�h
    public void LoadScene()
    {
        SceneManager.LoadScene((int)currentScene);
    }
    //�^�C�g���V�[���ɖ߂�
    public void LoadTitleScene()
    {
        SceneManager.LoadScene((int)GameScene.Title);
    }
    //�X�e�[�W�N���A���̏���
    public void OnStageGoal()
    {
        SceneManager.LoadScene((int)GameScene.Clear);
    }
    //�Q�[���I��
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("�Q�[���I��");
    }
    //�}�E�X�N���b�N�����Ď�
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LoadNextScene();
        }
    }
}
