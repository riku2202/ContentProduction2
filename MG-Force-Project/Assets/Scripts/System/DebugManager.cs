using UnityEngine;

namespace Game
{
    /// <summary>
    /// �f�o�b�N�p
    /// </summary>
    public static class DebugManager
    {
        // ���b�Z�[�W�^�C�v
        public enum MessageType 
        {
            Normal,
            Warning,
            Error,
        }

        /* -------- �J�e�S���[�\�� -------- */

        private const string NORMAL = "���s";

        private const string WARNING = "�x��";

        private const string ERROR = "�G���[";

        /* -------- �F���� -------- */

        private const string DEFAULT_COLOR = "<color=white>";
        //private const string GREEN_COLOR = "<color=lime>";
        private const string YELLOW_COLOR = "<color=yellow>";
        private const string RED_COLOR = "<color=red>";

        private const string FINISH_COLOR = "</color>";

        /// <summary>
        /// ���O���b�Z�[�W�\������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="class_name"></param>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void LogMessage(string message, MessageType type = MessageType.Normal)
        {
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(1, true);
            System.Diagnostics.StackFrame frame = stackTrace.GetFrame(0);
            string fileName = System.IO.Path.GetFileName(frame.GetFileName() ?? "Unknown File");
            int lineNumber = frame.GetFileLineNumber();
            string callerInfo = $"[{fileName}:{lineNumber}]";

            // �^�C�v�ɂ���ĕ\���`����ς���
            switch (type) 
            { 
                case MessageType.Normal:
                    Debug.Log($"{DEFAULT_COLOR}�y{NORMAL}�z{message} {callerInfo}{FINISH_COLOR}");
                    break;

                case MessageType.Warning:
                    Debug.LogWarning($"{YELLOW_COLOR}�y{WARNING}�z{message} {callerInfo}{FINISH_COLOR}");
                    break;

                case MessageType.Error:
                    Debug.LogError($"{RED_COLOR}�y{ERROR}�z{message} {callerInfo}{FINISH_COLOR}");
                    break;
            }
        }
    }
}