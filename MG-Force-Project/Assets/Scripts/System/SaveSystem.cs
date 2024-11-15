using UnityEngine;
using System.IO;

namespace System
{
    /// <summary>
    /// データのセーブとロード管理
    /// </summary>
    public class SaveSystem
    {
        // ファイルパスの指定
        private static readonly string FilePath = Application.persistentDataPath + "/gamedata.json";

        /// <summary>
        /// ゲームデータのセーブ
        /// </summary>
        public static void SaveManager()
        {
            // ゲームデータの取得
            GameData data = GameDataManager.Instance.GetGameData();

            // ゲームデータが無効の場合
            if (data == null)
            {
                Debug.Log("【System】セーブするデータがありません : ");
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
                Debug.Log("【System】ロードするデータがありません : ");
            }

            // ゲームデータを設定
            GameDataManager.Instance.SetGameData(loaddata);
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
            Debug.Log("【System】ゲームデータが保存されました : " + FilePath);
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

                Debug.Log("【System】ゲームデータが読み込まれました : " + FilePath);
                return data;
            }
            // ファイルが存在しない場合
            else
            {
                Debug.Log("【System】ゲームデータが見つかりません : " + FilePath);
                return null;
            }
        }
    }
}