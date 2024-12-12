using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Stage
{
    /// <summary>
    /// �X�e�[�W�I���V�[���̊Ǘ��N���X
    /// </summary>
    public class StageSelectSceneController : MonoBehaviour
    {
        private void Awake()
        {
            GameObject input = GameObject.Find(GameConstants.INPUT_MANAGER_OBJ);

            if (input == null)
            {
                SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
            }
        }
    }
}