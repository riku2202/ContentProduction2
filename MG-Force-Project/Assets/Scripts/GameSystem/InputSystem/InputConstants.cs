using UnityEngine;

namespace Game
{
    public static class InputConstants
    {
        public static class Device
        {
            public const string SWITCH = "switch";
            public const string PLAY_STATION = "playstation";
            public const string XBOX = "xbox";
        }

        public static class ActionDevice
        {
            public const string KEY_MOUSE = "KeyMouse";
            public const string GAMEPAD = "Gamepad";
            public const string GAMEPAD_2 = "Gamepad_2";
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
            public const string SHOOT_CANCEL = "ShootCancel";
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
            public const string SHORTCUT_1 = "ShortCut_1";
            public const string SHORTCUT_2 = "ShortCut_2";
            public const string SHORTCUT_3 = "ShortCut_3";
            public const string SHORTCUT_4 = "ShortCut_4";

#if UNITY_EDITOR

            // Debug Action Maps
            public const string DEBUG_NEXT = "NextUpdate";
            public const string DEBUG_RENEXT = "ReUpdate";
            public const string DEBUG_RESET = "ReSet";
#endif
            public const string DEBUG_CREDITS = "CreditsMove";
        }

        public static class ActionVector
        {
            public static readonly Vector2 North = new Vector2(0.0f, 1.0f);
            public static readonly Vector2 South = new Vector2(0.0f, -1.0f);
            public static readonly Vector2 West = new Vector2(-1.0f, 0.0f);
            public static readonly Vector2 East = new Vector2(1.0f, 0.0f);

            public static readonly Vector2 NorthWest = new Vector2(-1.0f, 1.0f);
            public static readonly Vector2 NorthEast = new Vector2(1.0f, 1.0f);
            public static readonly Vector2 SouthWest = new Vector2(-1.0f, -1.0f);
            public static readonly Vector2 SouthEast = new Vector2(1.0f, -1.0f);
        }
    }
}