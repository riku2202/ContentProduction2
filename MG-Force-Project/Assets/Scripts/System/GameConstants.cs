using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// ゲーム全体での定数管理用
    /// </summary>
    public static class GameConstants
    {
        /* ================ シーン ================ */

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

        /* ================ タグ ================ */

        public static class Tag 
        {
            public const string UNTAGGED = "Untagged";
            public const string FIXED = "Fixed";
            public const string MOVING = "Moving";
            public const string STAGE = "Stage";
            public const string PLAYER = "Player";
        }

        /* ================ レイヤー ================ */

        /// <summary>
        /// ゲーム内のタグを定義する列挙型
        /// 【int型にキャストして使用する】
        /// </summary>
        public enum Layer
        {
            DEFAULT = 0,         // すべてのオブジェクトのデフォルト
            TRANSPARENT_FX = 1,  // 透明エフェクト用
            IGNORE_RAYCAST = 2,  // レイキャストを無視する

            WATER = 4,           // 水の表現用
            UI = 5,              // ユーザーインターフェース
            N_MAGNET = 6,        // 磁力 N極
            S_MAGNET = 7,        // 磁力 S極
            BULLET = 8,          // 磁力の弾
            MAGNET_RANGE = 9,    // 磁力の範囲
        }

        /* ================ オブジェクト ================ */

        public static class Object 
        {
            public const string STAGE_SELECT_OBJ = "StageSelectSceneManager";
            public const string STAGE_OBJ = "StageSceneManager";

            public const string INPUT_OBJ = "InputManager";
            public const string SYSTEM_MESSAGE_OBJ = "SystemMessagePrefab";
            public const string SYSTEM_MESSAGE_TEXT = "Message";
        }

        public const string MAIN_CAMERA = "Main Camera";

        // サウンド管理用オブジェクト
        public const string SOUND_MANAGER_OBJ = "SoundManager";

        // 磁力管理用オブジェクト
        public const string MAGNET_MANAGER_OBJ = "MagnetManager";

        // ステージロード用オブジェクト
        public const string STAGE_LOADER_OBJ = "StageLoader";

        // 生成するプレイヤーオブジェクト
        public const string PLAYER_OBJ = "MagForce(Clone)";

        /// <summary>
        /// 入力定数
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

        /* ================ ステージ ================ */

        // ステージの最大数
        public const int STAGE_MAX_NUM = 10;

        // ステージ名
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

        /* ================ 時間 ================ */

        // 標準値
        public const int DEFAULT_FPS = 60;

        public const int HIGH_FPS = 120;

        /* ================ 座標 ================ */

        public static readonly Vector3 LowerLeft = new Vector3(-6.5f, -3.0f, 0.0f);        // 左下最小座標
        public static readonly Vector3 TopRight = new Vector3(29.5f, 19.0f, 0.0f);         // 右上最大座標

        public static readonly Vector3 LowerLeftCamera = new Vector3(1.0f, 1.0f, -10.0f);  // カメラの左下最小座標
        public static readonly Vector3 TopRightCamera = new Vector3(22.5f, 16.5f, -10.0f); // カメラの右上最大座標

        /* ================ 数値計算 ================ */

        // 半分計算
        public const float HAFE = 2.0f;

        // 反転値
        public const float INVERSION = -1.0f;

        /* ================ 状態管理 ================ */

        // 値のリセット
        public const float RESET = 0.0f;

        // 正常動作の判定用
        public const int NORMAL = 0;

        // エラー動作の判定用
        public const int ERROR = -1;
    }
}