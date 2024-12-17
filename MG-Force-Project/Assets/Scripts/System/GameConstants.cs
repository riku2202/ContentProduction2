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

        /// <summary>
        /// ゲーム内のタグを定義する列挙型
        /// 【Tag.タグ名.ToString()でタグ(string型)を取得】
        /// </summary>
        public enum Tag
        {
            Untagged, // タグ未設定

            Fixed,    // 固定オブジェクト
            Moving,   // 可動オブジェクト
            Stage,    // ステージオブジェクト
            Player,   // プレイヤーオブジェクト
        }

        #region -------- タグの変換処理 --------

        // String型に対応したTag型
        public static readonly Dictionary<string, Tag> StringToTag = new Dictionary<string, Tag>
        {
            { Tag.Untagged.ToString(), Tag.Untagged },
            { Tag.Fixed.ToString(), Tag.Fixed },
            { Tag.Moving.ToString(), Tag.Moving },
            { Tag.Stage.ToString(), Tag.Stage },
            { Tag.Player.ToString(), Tag.Player },
        };

        // Tag型に対応したString型
        public static readonly Dictionary<Tag, string> TagToString = new Dictionary<Tag, string>
        {
            { Tag.Untagged, Tag.Untagged.ToString() },
            { Tag.Fixed, Tag.Fixed.ToString() },
            { Tag.Moving, Tag.Moving.ToString() },
            { Tag.Stage, Tag.Stage.ToString() },
            { Tag.Player, Tag.Player.ToString() },
        };

        /// <summary>
        /// String型をTag型に変換する
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
                DebugManager.LogMessage("String型をTag型に変換できませんでした", DebugManager.MessageType.Error);
                return Tag.Untagged;
            }
        }

        /// <summary>
        /// Tag型をString型に変換する
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
                DebugManager.LogMessage("Tag型をString型に変換できませんでした", DebugManager.MessageType.Error);
                return Tag.Untagged.ToString();
            }
        }

        #endregion


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

        // プレイヤー追尾カメラ
        public const string PLAYER_VIEW_CAMERA = "PlayerViewCamera";

        // 入力管理用オブジェクト
        public const string INPUT_MANAGER_OBJ = "InputManager";

        // サウンド管理用オブジェクト
        public const string SOUND_MANAGER_OBJ = "SoundManager";

        // 磁力管理用オブジェクト
        public const string MAGNET_MANAGER_OBJ = "MagnetManager";

        // ステージロード用オブジェクト
        public const string STAGE_LOADER_OBJ = "StageLoader";

        // 生成するプレイヤーオブジェクト
        public const string PLAYER_OBJ = "MagForce(Clone)";

        /* ================ 入力 ================ */

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

        public static readonly Vector3 LowerLeft = new Vector3(-6.5f, -4.0f, 0.0f);

        public static readonly Vector3 LowerLeftCamera = new Vector3(0.5f, 0.0f, -10.0f);

        public static readonly Vector3 TopRight = new Vector3(29.0f, 18.5f, 0.0f);

        public static readonly Vector3 TopRightCamera = new Vector3(22.0f, 16.0f, -10.0f);

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