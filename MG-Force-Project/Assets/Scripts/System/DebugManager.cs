using UnityEngine;

namespace Game
{
    /// <summary>
    /// �f�o�b�N�p
    /// </summary>
    public static class DebugManager
    {

        public enum MessageType 
        {
            Normal,
            Warning,
            Error,
        }

        /* -------- ���b�Z�[�W�^�C�v -------- */

        private const string NORMAL = "���s�F";

        private const string WARNING = "�x���F";

        private const string ERROR = "�G���[�F";

        /* -------- ���b�Z�[�W�� -------- */

        private const string DEFAULT_SYSTEM = "System";

        /* -------- �F���� -------- */

        private const string GREEN_COLOR = "<color=lime>";
        private const string YELLOW_COLOR = "<color=yellow>";
        private const string RED_COLOR = "<color=red>";

        private const string FINISH_COLOR = "</color>";

        /// <summary>
        /// ���O���b�Z�[�W�\������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="class_name"></param>
        public static void LogMessage(string message, MessageType type = MessageType.Normal, string class_name = DEFAULT_SYSTEM)
        {
#if UNITY_EDITOR

            string color = GREEN_COLOR;
            string message_type = null;

            switch (type) 
            { 
                case MessageType.Normal:
                    color = GREEN_COLOR;
                    message_type = NORMAL;
                    break;

                case MessageType.Warning:
                    color = YELLOW_COLOR;
                    message_type = WARNING;
                    break;

                case MessageType.Error:
                    color = RED_COLOR;
                    message_type = ERROR;
                    break;
            }

            Debug.Log(color + "�y" + class_name + " �z" + message_type + message + FINISH_COLOR);

#endif
        }
    }
}