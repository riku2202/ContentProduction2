using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Game.GameSystem;

namespace Game.StageSelect
{
    /// <summary>
    /// ステージ選択シーンの管理クラス
    /// </summary>
    public class StageSelectSceneController : MonoBehaviour
    {
        private void Awake()
        {
            GameObject Input = GameObject.Find(GameConstants.INPUT_MANAGER_OBJ);

            if (Input == null)
            {
                SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
            }
        }

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(GameConstants.Scene.StageSelect.ToString());
            }
        }
    }
}