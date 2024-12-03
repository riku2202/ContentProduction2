using UnityEngine;
using System.IO;

namespace Game.GameSystem
{
    /// <summary>
    /// データのセーブとロード管理
    /// </summary>
    public class SaveSystem
    {
        // ファイルパスの指定
        private static readonly string FilePath = Application.persistentDataPath + "/gamedata.json";

        // ゲームデータ管理クラスの呼び出し
        private static GameDataManager dataManager = GameDataManager.Instance;

        // 初期データ
        private static GameData InitData;

        /// <summary>
        /// ゲームデータの生成
        /// </summary>
        public static void NewGameData()
        {
            InitData = new GameData();
        }

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
                DebugManager.LogMessage("セーブするデータがありません：" + FilePath, DebugManager.MessageType.Warning);
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
                DebugManager.LogMessage("ロードするデータがありません：" + FilePath, DebugManager.MessageType.Warning);
            }

            // 既存のデータがない場合にロードする
            if (dataManager.GetGameData() == InitData)
            {
                // ゲームデータを設定
                dataManager.SetGameData(loaddata);
            }
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
            File.WriteAllText(FilePath, json);
            DebugManager.LogMessage("ゲームデータが保存されました：" + FilePath);
        }

        /// <summary>
        /// ファイルのデータをロード
        /// </summary>
        private static GameData LoadGameData()
        {
            // ファイルが存在する場合
            if (File.Exists(FilePath))
            {
                string Json = File.ReadAllText(FilePath);

                GameData data = JsonUtility.FromJson<GameData>(Json);

                DebugManager.LogMessage("ゲームデータが読み込まれました：" + FilePath);

                return data;
            }
            // ファイルが存在しない場合
            else
            {
                DebugManager.LogMessage("ゲームデータが見つかりません：" + FilePath, DebugManager.MessageType.Warning);

                return null;
            }
        }

        public static void DeleteGameData()
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);

                DebugManager.LogMessage("ゲームデータが削除されました：" + FilePath);
            }
            else
            {
                DebugManager.LogMessage("ゲームデータが見つかりません：" + FilePath);
            }
        }
    }
}