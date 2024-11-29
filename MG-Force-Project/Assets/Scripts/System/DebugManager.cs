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

        private const string NORMAL = "���s�F";

        private const string WARNING = "�x���F";

        private const string ERROR = "�G���[�F";


        /* -------- �o�͏ꏊ -------- */

        private const string DEFAULT_SYSTEM = "System";


        /* -------- �F���� -------- */

        private const string DEFAULT_COLOR = "<color=white>";
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
        public static void LogMessage(string message, MessageType type = MessageType.Normal, string log_sorce = DEFAULT_SYSTEM)
        {
#if UNITY_EDITOR

            string message_color = DEFAULT_COLOR;  // �F(�f�t�H���g�͗�)
            string message_type = NORMAL;  // �J�e�S���[�\��(�f�t�H���g�͎��s)

            // �^�C�v�ɂ���ĕ\���`����ς���
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

            // �\��
            Debug.Log(message_color + "�y" + log_sorce + " �z" + message_type + message + FINISH_COLOR);

#endif
        }
    }
}