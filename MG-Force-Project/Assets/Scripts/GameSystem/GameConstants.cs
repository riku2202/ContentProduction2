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

        public static class Tag 
        {
            public const string UNTAGGED = "Untagged";
            public const string FIXED = "Fixed";
            public const string MOVING = "Moving";
            public const string STAGE = "Stage";
            public const string PLAYER = "Player";
        }

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
            public const string STAGE_SELECT = "StageSelectSceneManager";
            public const string STAGE = "StageSceneManager";

            public const string INPUT = "InputManager";
            public const string SYSTEM_MESSAGE = "SystemMessagePrefab";
            public const string DEVICE_MANAGER = "DeviceManager";

            public const string BGM_MANAGER = "BGMManager";
            public const string SE_MANAGER = "SEManager";
            public const string TITLE_SCENE_MANAGER = "TitleSceneManager";
            public const string GOAL_CRYSTAL = "Crystal_Model_Prefab(Clone)";

            public const string STAGE_LOADER = "StageLoader";
        }

        public const string MAIN_CAMERA = "Main Camera";

        // �T�E���h�Ǘ��p�I�u�W�F�N�g
        public const string SOUND_MANAGER_OBJ = "SoundManager";

        // ���͊Ǘ��p�I�u�W�F�N�g
        public const string MAGNET_MANAGER_OBJ = "MagnetManager";

        // �X�e�[�W���[�h�p�I�u�W�F�N�g
        public const string STAGE_LOADER_OBJ = "StageLoader";

        // ��������v���C���[�I�u�W�F�N�g
        public const string PLAYER_OBJ = "MagForce(Clone)";

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