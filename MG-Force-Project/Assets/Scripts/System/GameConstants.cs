using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// �Q�[���S�̂ł̒萔�Ǘ��p
    /// </summary>
    public static class GameConstants
    {
        /* ================ �V�[�� ================ */

        public enum Scene
        {
            Title,
            Setting,
            Options,
            Loading,
            StageSelect,
            Stage,
            Clear,
            Credits,
        }

        /* ================ �^�O ================ */

        /// <summary>
        /// �Q�[�����̃^�O���`����񋓌^
        /// �yTag.�^�O��.ToString()�Ń^�O(string�^)���擾�z
        /// </summary>
        public enum Tag
        {
            Untagged, // �^�O���ݒ�

            Fixed,    // �Œ�I�u�W�F�N�g
            Moving,   // ���I�u�W�F�N�g
            Stage,    // �X�e�[�W�I�u�W�F�N�g
            Player,   // �v���C���[�I�u�W�F�N�g
        }

        #region -------- �^�O�̕ϊ����� --------

        // String�^�ɑΉ�����Tag�^
        public static readonly Dictionary<string, Tag> StringToTag = new Dictionary<string, Tag>
        {
            { Tag.Untagged.ToString(), Tag.Untagged },
            { Tag.Fixed.ToString(), Tag.Fixed },
            { Tag.Moving.ToString(), Tag.Moving },
            { Tag.Stage.ToString(), Tag.Stage },
            { Tag.Player.ToString(), Tag.Player },
        };

        // Tag�^�ɑΉ�����String�^
        public static readonly Dictionary<Tag, string> TagToString = new Dictionary<Tag, string>
        {
            { Tag.Untagged, Tag.Untagged.ToString() },
            { Tag.Fixed, Tag.Fixed.ToString() },
            { Tag.Moving, Tag.Moving.ToString() },
            { Tag.Stage, Tag.Stage.ToString() },
            { Tag.Player, Tag.Player.ToString() },
        };

        /// <summary>
        /// String�^��Tag�^�ɕϊ�����
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static Tag ConvertTag(string tag)
        {
            if (StringToTag.TryGetValue(tag, out Tag get_tag))
            {
                return get_tag;
            }
            else
            {
                DebugManager.LogMessage("String�^��Tag�^�ɕϊ��ł��܂���ł���", DebugManager.MessageType.Error);
                return Tag.Untagged;
            }
        }

        /// <summary>
        /// Tag�^��String�^�ɕϊ�����
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string ConvertTag(Tag tag)
        {
            if (TagToString.TryGetValue(tag, out string get_tag))
            {
                return get_tag;
            }
            else
            {
                DebugManager.LogMessage("Tag�^��String�^�ɕϊ��ł��܂���ł���", DebugManager.MessageType.Error);
                return Tag.Untagged.ToString();
            }
        }

        #endregion

        /* ================ ���C���[ ================ */

        /// <summary>
        /// �Q�[�����̃^�O���`����񋓌^
        /// �yint�^�ɃL���X�g���Ďg�p����z
        /// </summary>
        public enum Layer
        {
            DEFAULT = 0,         // ���ׂẴI�u�W�F�N�g�̃f�t�H���g
            TRANSPARENT_FX = 1,  // �����G�t�F�N�g�p
            IGNORE_RAYCAST = 2,  // ���C�L���X�g�𖳎�����

            WATER = 4,           // ���̕\���p
            UI = 5,              // ���[�U�[�C���^�[�t�F�[�X
            N_MAGNET = 6,        // ���� N��
            S_MAGNET = 7,        // ���� S��
            BULLET = 8,          // ���͂̒e
            MAGNET_RANGE = 9,    // ���͈͂̔�
        }

        /* ================ �I�u�W�F�N�g ================ */

        public static class Object 
        {
            public const string STAGE_SELECT_OBJ = "StageSelectSceneManager";
            public const string STAGE_OBJ = "StageSceneManager";

            public const string INPUT_OBJ = "InputManager";
            public const string SYSTEM_MESSAGE_OBJ = "SystemMessagePrefab";
            public const string SYSTEM_MESSAGE_TEXT = "Message";
        }

        // �v���C���[�ǔ��J����
        public const string PLAYER_VIEW_CAMERA = "PlayerViewCamera";

        // �T�E���h�Ǘ��p�I�u�W�F�N�g
        public const string SOUND_MANAGER_OBJ = "SoundManager";

        // ���͊Ǘ��p�I�u�W�F�N�g
        public const string MAGNET_MANAGER_OBJ = "MagnetManager";

        // �X�e�[�W���[�h�p�I�u�W�F�N�g
        public const string STAGE_LOADER_OBJ = "StageLoader";

        // ��������v���C���[�I�u�W�F�N�g
        public const string PLAYER_OBJ = "MagForce(Clone)";

        /// <summary>
        /// ���͒萔
        /// </summary>
        public static class Input
        {
            public static class ActionDevice
            {
                public const string KEY_MOUSE = "KeyMouse";
                public const string GAMEPAD = "Gamepad";
            }

            public static class ActionMaps 
            {
                public const string PLAYER_MAPS = "Player";
                public const string MAGNET_MAPS = "Magnet";
                public const string CAMERA_MAPS = "Camera";
                public const string MENU_MAPS = "Menu";

                public const string SHORTCUT_MAPS = "Shortcut";
                public const string DEBUG_MAPS = "Debug";
            }

            public static class Action 
            {
                // Player Action Maps
                public const string ACTION = "Action";
                public const string LEFTMOVE = "LeftMove";
                public const string RIGHTMOVE = "RightMove";
                public const string JUMP = "Jump";
                public const string MENU_OPEN = "MenuOpen";
                public const string VIEW_MODE_START = "ViewModeStart";
                public const string MAGNET_BOOT = "MagnetBoot";

                // Magnet Action Maps
                public const string POLE_SWITCHING = "PoleSwitching";
                public const string MAGNET_POWER = "PowerCharge";
                public const string SHOOT = "Shoot";
                public const string SHOOT_ANGLE = "ShootAngle";
                public const string RESET = "Reset";

                // Camera Action Maps
                public const string VIEW_MODE_END = "ViewModeEnd";
                public const string VIEW_MOVE = "ViewMove";

                // Menu Action Maps
                public const string MENU_CLOSE = "Close";
                public const string MENU_DECISION = "Decision";
                public const string MENU_BACK = "Back";
                public const string MENU_LEFT_SELECT = "LeftSelect";
                public const string MENU_RIGHT_SELECT = "RightSelect";
                public const string MENU_UP_SELECT = "UpSelect";
                public const string MENU_DOWN_SELECT = "DownSelect";

                // Shortcut Action Maps

#if UNITY_EDITOR

                // Debug Action Maps
                public const string DEBUG_RESET = "ReSet";

#endif
            }
        }

        /* ================ �X�e�[�W ================ */

        // �X�e�[�W�̍ő吔
        public const int STAGE_MAX_NUM = 10;

        // �X�e�[�W��
        public enum Stage
        {
            Stage_Select,
            Stage_1,
            Stage_2,
            Stage_3,
            Stage_4,
            Stage_5,
            Stage_6,
            Stage_7,
            Stage_8,
            DebugStage,
        }

        /* ================ ���� ================ */

        // �W���l
        public const int DEFAULT_FPS = 60;

        public const int HIGH_FPS = 120;

        /* ================ ���W ================ */

        public static readonly Vector3 LowerLeft = new Vector3(-6.5f, -3.0f, 0.0f);        // �����ŏ����W
        public static readonly Vector3 TopRight = new Vector3(29.5f, 19.0f, 0.0f);         // �E��ő���W

        public static readonly Vector3 LowerLeftCamera = new Vector3(1.0f, 1.0f, -10.0f);  // �J�����̍����ŏ����W
        public static readonly Vector3 TopRightCamera = new Vector3(22.5f, 16.5f, -10.0f); // �J�����̉E��ő���W

        /* ================ ���l�v�Z ================ */

        // �����v�Z
        public const float HAFE = 2.0f;

        // ���]�l
        public const float INVERSION = -1.0f;

        /* ================ ��ԊǗ� ================ */

        // �l�̃��Z�b�g
        public const float RESET = 0.0f;

        // ���퓮��̔���p
        public const int NORMAL = 0;

        // �G���[����̔���p
        public const int ERROR = -1;
    }
}