using UnityEngine;

namespace Game
{
    /// <summary>
    /// デバック用
    /// </summary>
    public static class DebugManager
    {
        // メッセージタイプ
        public enum MessageType 
        {
            Normal,
            Warning,
            Error,
        }


        /* -------- カテゴリー表示 -------- */

        private const string NORMAL = "実行：";

        private const string WARNING = "警告：";

        private const string ERROR = "エラー：";


        /* -------- 出力場所 -------- */

        private const string DEFAULT_SYSTEM = "System";


        /* -------- 色分け -------- */

        private const string DEFAULT_COLOR = "<color=white>";
        private const string GREEN_COLOR = "<color=lime>";
        private const string YELLOW_COLOR = "<color=yellow>";
        private const string RED_COLOR = "<color=red>";

        private const string FINISH_COLOR = "</color>";


        /// <summary>
        /// ログメッセージ表示処理
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="class_name"></param>
        public static void LogMessage(string message, MessageType type = MessageType.Normal, string log_sorce = DEFAULT_SYSTEM)
        {
#if UNITY_EDITOR

            string message_color = DEFAULT_COLOR;  // 色(デフォルトは緑)
            string message_type = NORMAL;  // カテゴリー表示(デフォルトは実行)

            // タイプによって表示形式を変える
            switch (type) 
            { 
                case MessageType.Normal:
                    message_color = DEFAULT_COLOR;
                    message_type = NORMAL;
                    break;

                case MessageType.Warning:
                    message_color = YELLOW_COLOR;
                    message_type = WARNING;
                    break;

                case MessageType.Error:
                    message_color = RED_COLOR;
                    message_type = ERROR;
                    break;
            }

            // 表示
            Debug.Log(message_color + "【" + log_sorce + " 】" + message_type + message + FINISH_COLOR);

#endif
        }
    }
}