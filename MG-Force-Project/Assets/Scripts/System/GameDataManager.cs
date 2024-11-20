using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System 
{
    /// <summary>
    /// ゲームデータの管理クラス
    /// </summary>
    public class GameDataManager
    {
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

        // ゲームデータ
        private GameData data;

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
    }
}