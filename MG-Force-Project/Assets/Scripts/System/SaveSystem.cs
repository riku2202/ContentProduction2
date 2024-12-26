using UnityEngine;
using System.IO;

namespace Game.GameSystem
{
    /// <summary>
    /// データのセーブとロード管理クラス
    /// </summary>
    public class SaveSystem
    {
        // 保存するファイルパスの指定
        private static readonly string gameDataFilePath = Application.persistentDataPath + "/gamedata.json";

        // ゲームデータ管理クラスの呼び出し
        private static GameDataManager dataManager = GameDataManager.Instance;

        /// <summary>
        /// ゲームデータのセーブ
        /// </summary>
        public static void SaveManager()
        {
            // ゲームデータの取得
            GameData data = dataManager.GetGameData();

            // ゲームデータが無効の場合
            if (data == null)
            {
                DebugManager.LogMessage("セーブするデータがありません：" + gameDataFilePath, DebugManager.MessageType.Warning);
                return;
            }

            // ゲームデータの保存
            SaveGameData(data);
        }

        /// <summary>
        /// ゲームデータのロード
        /// </summary>
        public static void LoadManager()
        {
            // ゲームデータのロード
            GameData loaddata = LoadGameData();

            // ゲームデータが無効の場合
            if (loaddata == null)
            {
                DebugManager.LogMessage("ロードするデータがありません：" + gameDataFilePath, DebugManager.MessageType.Warning);
            }

            // ゲームデータを設定
            dataManager.SetGameData(loaddata);
        }

        /// <summary>
        /// データをファイルに保存
        /// </summary>
        /// <param name="data"></param>
        private static void SaveGameData(GameData data)
        {
            // GameDataをJson形式に変換
            string json = JsonUtility.ToJson(data);

            // Jsonデータをファイルに書き込み
            File.WriteAllText(gameDataFilePath, json);
            DebugManager.LogMessage("ゲームデータが保存されました：" + gameDataFilePath);
        }

        /// <summary>
        /// ファイルのデータをロード
        /// </summary>
        private static GameData LoadGameData()
        {
            // ファイルが存在する場合
            if (File.Exists(gameDataFilePath))
            {
                string Json = File.ReadAllText(gameDataFilePath);

                GameData data = JsonUtility.FromJson<GameData>(Json);

                DebugManager.LogMessage("ゲームデータが読み込まれました：" + gameDataFilePath);

                return data;
            }
            // ファイルが存在しない場合
            else
            {
                DebugManager.LogMessage("ゲームデータが見つかりません：" + gameDataFilePath, DebugManager.MessageType.Warning);

                return null;
            }
        }

        public static void DeleteGameData()
        {
            if (File.Exists(gameDataFilePath))
            {
                File.Delete(gameDataFilePath);

                DebugManager.LogMessage("ゲームデータが削除されました：" + gameDataFilePath);
            }
            else
            {
                DebugManager.LogMessage("ゲームデータが見つかりません：" + gameDataFilePath);
            }
        }
    }
}