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

        // �v���C���[�ǔ��J����
        public const string PLAYER_VIEW_CAMERA = "PlayerViewCamera";

        // ���͊Ǘ��p�I�u�W�F�N�g
        public const string INPUT_MANAGER_OBJ = "InputManager";

        // �T�E���h�Ǘ��p�I�u�W�F�N�g
        public const string SOUND_MANAGER_OBJ = "SoundManager";

        // ���͊Ǘ��p�I�u�W�F�N�g
        public const string MAGNET_MANAGER_OBJ = "MagnetManager";

        // �X�e�[�W���[�h�p�I�u�W�F�N�g
        public const string STAGE_LOADER_OBJ = "StageLoader";

        // ��������v���C���[�I�u�W�F�N�g
        public const string PLAYER_OBJ = "MagForce(Clone)";

        /* ================ ���� ================ */

        // GamePlay
        public const string INPUT_JUMP = "Jump";
        public const string INPUT_ACTION = "Action";
        public const string INPUT_SELECT = "Select";

        public const string INPUT_MENU_CHANGE = "MenuChange";
        public const string INPUT_MAGNET_RESET = "Magnet Reset";
        public const string INPUT_POLE_SWITCHING = "PoleSwitching";

        public const string INPUT_SHOOT = "Shoot";

        public const string INPUT_VIEWMODE = "ViewMode";

        public const string INPUT_MANGET_BOOT = "Magnet Boot";

        public const string INPUT_NONE = "None";

        public const string INPUT_MOVE = "Move";
        public const string INPUT_SHOOT_ANGLE = "ShootAngle";
        public const string INPUT_MAGNET_POWER = "MagnetPower";
        public const string INPUT_BACK = "InputBack";


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

        public static readonly Vector3 LowerLeft = new Vector3(-6.5f, -4.0f, 0.0f);

        public static readonly Vector3 LowerLeftCamera = new Vector3(0.5f, 0.0f, -10.0f);

        public static readonly Vector3 TopRight = new Vector3(29.0f, 18.5f, 0.0f);

        public static readonly Vector3 TopRightCamera = new Vector3(22.0f, 16.0f, -10.0f);

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