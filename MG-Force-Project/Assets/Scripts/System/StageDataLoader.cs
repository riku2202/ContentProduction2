using System.IO;
using UnityEngine;

using Game.Stage;

namespace Game.GameSystem
{
    /// <summary>
    /// ステージデータのロード管理クラス
    /// </summary>
    public class StageDataLoader
    {
        // ステージデータのディレクトリー
        private const string STAGEDATA_DIRECTORY = "StageData";

        // Json型のファイル形式
        private const string JSON_FORMAT = ".json";

#if UNITY_EDITOR // ステージデータの更新はデバック時のみ

        /// <summary>
        /// ステージデータの取得
        /// </summary>
        /// <returns></returns>
        public static void LoadStageData()
        {
            // 設定ファイルパスキー
            const string CONFIG_FILE_KEY = "Editor/StageDataFilePath.json";

            // 設定ファイルパス
            string config_filepath = Path.Combine(Application.dataPath, CONFIG_FILE_KEY);

            // 設定ファイル
            string config_file;

            // ファイルが存在する場合
            if (File.Exists(config_filepath))
            {
                config_file = File.ReadAllText(config_filepath);
            }
            else
            {
                return;
            }

            // ステージデータのファイルパス取得
            string stagedata_filepath = config_file.Trim();

            // ダブルクォーテーションを除外する
            stagedata_filepath = stagedata_filepath.Trim('"');

            // nullチェック
            if (string.IsNullOrEmpty(stagedata_filepath)) return;

            for (int i = 0; i < GameConstants.STAGE_MAX_NUM; i++)
            {
                // 現在のステージ取得
                GameConstants.Stage current_stage = (GameConstants.Stage)((int)i);

                // ステージのデータパス
                string stage_filepath = Path.Combine(stagedata_filepath, current_stage.ToString() + JSON_FORMAT);

                if (File.Exists(stage_filepath))
                {
                    // 保存するディレクトリー
                    string save_directory = Path.Combine(Application.streamingAssetsPath, STAGEDATA_DIRECTORY);

                    // ディレクトリが存在しない場合は作成
                    if (!Directory.Exists(save_directory))
                    {
                        Directory.CreateDirectory(save_directory);
                    }

                    // 保存先のファイルパス
                    string assets_filepath = Path.Combine(save_directory, current_stage.ToString() + JSON_FORMAT);

                    // Json形式のデータ取得
                    string fileContent = File.ReadAllText(stage_filepath);

                    // Json形式のステージデータ作成
                    File.WriteAllText(assets_filepath, fileContent);

                    DebugManager.LogMessage($"ステージデータの読み込みに成功しました：{current_stage.ToString()}：{stagedata_filepath} ");
                }
                else
                {
                    DebugManager.LogMessage($"ステージデータの読み込みに失敗しました：{stagedata_filepath}", DebugManager.MessageType.Warning);

                }
            }
        }

#endif

        /// <summary>
        /// ステージデータの呼び出し
        /// </summary>
        /// <param name="stage_index"></param>
        public static string CellStageData(int stage_index)
        {
            // 現在のステージ名
            GameConstants.Stage current_stage = (GameConstants.Stage)(stage_index);

            // ファイル名
            string filename = new string(current_stage + JSON_FORMAT);

            // ファイルパス
            string filepath = Path.Combine(Application.streamingAssetsPath, STAGEDATA_DIRECTORY);

            // ステージデータのパス
            string stagedata_file = Path.Combine(filepath, filename);

            if (File.Exists(stagedata_file))
            {
                string json = File.ReadAllText(stagedata_file);

                return json;
            }
            else
            {
                return null;
            }
        }
    }
}