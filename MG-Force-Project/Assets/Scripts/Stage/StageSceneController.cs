using Game.Stage.Magnet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Stage
{
    /// <summary>
    /// ステージシーン管理クラス
    /// </summary>
    public class StageSceneController : MonoBehaviour
    {
        // フェーズ
        private enum Phase
        {
            Reserve,
            Execution,
        }

        // 現在のフェーズ
        private Phase currentPhase;

        private void Awake()
        {
            GameObject Input = GameObject.Find(GameConstants.INPUT_MANAGER_OBJ);

            //if (Input == null)
            //{
            //    SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
            //}
        }

        private void Update()
        {
            if (currentPhase == Phase.Reserve)
            {

            }
            else if (currentPhase == Phase.Execution)
            {

            }
        }

        // 共通の処理


        // 磁力を撃つフェーズの処理


        // 磁力を起動したフェーズの処理

    }
}