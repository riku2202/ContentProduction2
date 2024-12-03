using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem 
{
    /// <summary>
    /// ゲームデータの管理クラス
    /// </summary>
    public class GameDataManager
    {
        #region -------- シングルトンの設定 --------

        // ゲームデータ管理クラスのシングルトンインスタンス
        private static GameDataManager instance;

        /// <summary>
        /// コンストラクタ
        /// シングルトンパターンを実装するため、外部からのインスタンス生成を防ぐ
        /// </summary>
        private GameDataManager() { }

        /// <summary>
        /// シングルトンインスタンスを返す
        /// </summary>
        /// <returns>instance</returns>
        public static GameDataManager Instance
        {
            get
            {
                // インスタンスが存在しない場合
                if (instance == null)
                {
                    // インスタンスを生成
                    instance = new GameDataManager();
                }

                // インスタンスを返す
                return instance;
            }
        }

        #endregion


        // ゲームデータ
        private GameData data;

        /// <summary>
        /// ゲームデータの生成
        /// </summary>
        public void NewGameData()
        {
            data = new GameData();
        }

        #region -------- ゲームデータの設定 --------

        /// <summary>
        /// ゲームデータを設定する
        /// </summary>
        /// <param name="newdata"></param>
        public void SetGameData(GameData newdata)
        {
            data = newdata;
        }

        /// <summary>
        /// ゲームデータを返す
        /// </summary>
        /// <returns>GameData</returns>
        public GameData GetGameData()
        {
            return data;
        }

        /// <summary>
        /// ゲームデータのリセット
        /// </summary>
        public void ReSetGameData()
        {
            if (data.ReSetData() == GameConstants.NORMAL)
            {
                DebugManager.LogMessage("データを削除しました");
            }
            else
            {
                DebugManager.LogMessage("正常にデータが削除できませんでした", DebugManager.MessageType.Error);
            }
        }

        #endregion


        // 現在のステージインデックス
        private int CurrentStageIndex;

        public void SetCurrentStageIndex(int stage_index)
        {
            if (stage_index >= GameConstants.STAGE_MAX_NUM) { return; }

            CurrentStageIndex = stage_index;
        }

        public int GetCurrentStageIndex()
        {
            return CurrentStageIndex;
        }
    }
}