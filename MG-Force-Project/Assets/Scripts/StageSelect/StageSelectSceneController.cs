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
        [SerializeField]
        private StageData SelectStage;

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("StageSelect");
            }
        }
    }
}