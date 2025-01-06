using UnityEngine;

namespace Game.StageScene 
{
    /// <summary>
    /// ステージデータ(JSON)
    /// </summary>
    [System.Serializable]
    public class StageDataJSON
    {
        // ステージの管理番号
        private int _stageIndex;

        // ステージのオブジェクト
        private GameObject _stageObject;

        // ステージの背景
        private GameObject _stageBG;

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
            _stageIndex = stage_index;
            _stageObject = stage_object;
            _stageBG = stage_bg;
            topRight = top_right;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data"></param>
        public StageDataJSON(StageDataJSON data)
        {
            _stageIndex = data.StageIndex;
            _stageObject = data.StageObject;
            _stageBG = data.StageBG;
            topRight = data.TopRight;
        }

        /* -------- 読み取り用の変数 -------- */

        // ステージの管理番号
        public int StageIndex => _stageIndex;

        // ステージのオブジェクト
        public GameObject StageObject => _stageObject;

        // ステージの背景
        public GameObject StageBG => _stageBG;

        // ステージの右上座標
        public Vector2 TopRight => topRight;
    }
}