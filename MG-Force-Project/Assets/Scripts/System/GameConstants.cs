namespace Game
{
    /// <summary>
    /// ゲーム全体での定数管理用
    /// </summary>
    public static class GameConstants
    {
        /* -------- 時間 -------- */

        public const int DEFAULT_FPS = 60;

        public const int HIGH_FPS = 120;

        /* -------- 入力 -------- */

        public const string INPUT_ACTION = "Action";
        public const string INPUT_JUMP = "Jump";

        public const string INPUT_MAG_CHANGE = "Change";
        
        /* -------- タグ -------- */

        // 固定オブジェクト
        public const string FIXED_OBJTAG = "Fixed";

        // 可動オブジェクト
        public const string MOVE_OBJTAG = "Moving";

        /* -------- レイヤー -------- */

        // N極
        public const int N_MAGNET_LAYER = 7;

        // S極
        public const int S_MAGNET_LAYER = 8;

        // 弾
        public const int BULLET_LAYER = 9;

        /* -------- 変数 -------- */

        // 数値のリセット
        public const int RESET = 0;

        // 数値のリセット(float)
        public const float RESET_F = 0.0f;

        // 半分
        public const int HAFE = 2;

        // 半分(float)
        public const float HAFE_F = 2.0f;

        // 反転
        public const int INVERSION = -1;

        // 反転(float)
        public const float INVERSION_F = -1.0f;

        /* -------- 関数 -------- */

        // 正常値
        public const int NORMAL = 0;

        // エラー値
        public const int ERROR = -1;
    }
}