using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.ClearScene
{
    using GameSystem;

    /// <summary>
    /// クリアシーン管理クラス
    /// </summary>
    public class ClearSceneController : MonoBehaviour
    {
        GameDataManager gameDataManager;

        // 各ステージクリア時に呼び出される処理

        // 全ステージクリア時に呼び出される処理

        // フラグ管理はGameDataManagerからGameDataを呼び出してする(呼び出し引数はステージのインデックス)]

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            // ゲームデータ管理クラスの呼び出し
            gameDataManager = GameDataManager.Instance;

            // ステージクリア
            gameDataManager.GetGameData().SetIsClearStage(gameDataManager.GetCurrentStageIndex());

            // 全ステージクリアしたか
            if (ClearChack() == true)
            {
                EndCredits();
            }
            else
            {
                SceneManager.LoadScene("StageSelect");
            }
        }

        /// <summary>
        /// クリアチェック
        /// </summary>
        /// <returns></returns>
        private bool ClearChack()
        {
            for (int i = 0; i < GameConstants.STAGE_MAX_NUM; i++)
            {
                if (gameDataManager.GetGameData().GetIsClearStage(i) == false)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// エンドロール
        /// </summary>
        private void EndCredits()
        {

        }
    }
}