using UnityEngine;

namespace Game.Stage 
{
    /// <summary>
    /// ステージデータ(JSON)
    /// </summary>
    [System.Serializable]
    public class StageDataJSON
    {
        // ステージの管理番号
        private int stageIndex;

        // ステージのオブジェクト
        private GameObject stageObject;

        // ステージの背景
        private GameObject stageBG;

        // ステージの右上座標
        private Vector2 topRight;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="stage_index"></param>
        /// <param name="stage_object"></param>
        /// <param name="stage_bg"></param>
        /// <param name="top_right"></param>
        public StageDataJSON(int stage_index, GameObject stage_object, GameObject stage_bg, Vector2 top_right)
        {
            stageIndex = stage_index;
            stageObject = stage_object;
            stageBG = stage_bg;
            topRight = top_right;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data"></param>
        public StageDataJSON(StageDataJSON data)
        {
            stageIndex = data.StageIndex;
            stageObject = data.StageObject;
            stageBG = data.StageBG;
            topRight = data.TopRight;
        }


        /* -------- 読み取り用の変数 -------- */

        // ステージの管理番号
        public int StageIndex => stageIndex;

        // ステージのオブジェクト
        public GameObject StageObject => stageObject;

        // ステージの背景
        public GameObject StageBG => stageBG;

        // ステージの右上座標
        public Vector2 TopRight => topRight;
    }
}