using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Game.GameSystem;

namespace Game.Title
{
    /// <summary>
    /// タイトルシーンの管理クラス
    /// </summary>
    public class TitleSceneController : MonoBehaviour
    {
        // ゲームデータ管理クラスの呼び出し
        private GameDataManager manager = GameDataManager.Instance;

        // 入力管理クラスの呼び出し
        private InputManager input;

        // ロード管理フラグ
        private static bool isLoadGameData = false;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            // 外部データの読み込み
            StageDataLoader.LoadStageData();

            // ゲームデータの生成
            manager.NewGameData();

            // 実行して一度のみロードする
            if (!isLoadGameData)
            {
                // ゲームデータのロード
                SaveSystem.LoadManager();

                isLoadGameData = true;
            }

            input = GameObject.Find(GameConstants.INPUT_MANAGER_OBJ).GetComponent<InputManager>();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            if (input.IsActionPressed(GameConstants.INPUT_SELECT) || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(GameConstants.Scene.StageSelect.ToString());
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
            }
        }

        /// <summary>
        /// ゲームデータの削除
        /// </summary>
        public void GameDataErase()
        {
            // ゲームデータのリセット
            manager.ReSetGameData();

#if UNITY_EDITOR // UnityEditorでの実行時(デバック用)

            // ゲームデータのセーブ(デバック時はデータを上書きする)
            SaveSystem.SaveManager();


#else // 実際のビルド版実行時

            // ゲームデータの削除
            SaveSystem.DeleteGameData();

#endif
        }
    }
}