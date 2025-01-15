using UnityEngine;

namespace Game.StageScene 
{
    /// <summary>
    /// �X�e�[�W�f�[�^(JSON)
    /// </summary>
    [System.Serializable]
    public class StageDataJSON
    {
        // �X�e�[�W�̊Ǘ��ԍ�
        private int _stageIndex;

        // �X�e�[�W�̃I�u�W�F�N�g
        private GameObject _stageObject;

        // �X�e�[�W�̔w�i
        private GameObject _stageBG;

        // �X�e�[�W�̉E����W
        private Vector2 topRight;

        /// <summary>
        /// �R���X�g���N�^
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
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data"></param>
        public StageDataJSON(StageDataJSON data)
        {
            _stageIndex = data.StageIndex;
            _stageObject = data.StageObject;
            _stageBG = data.StageBG;
            topRight = data.TopRight;
        }

        /* -------- �ǂݎ��p�̕ϐ� -------- */

        // �X�e�[�W�̊Ǘ��ԍ�
        public int StageIndex => _stageIndex;

        // �X�e�[�W�̃I�u�W�F�N�g
        public GameObject StageObject => _stageObject;

        // �X�e�[�W�̔w�i
        public GameObject StageBG => _stageBG;

        // �X�e�[�W�̉E����W
        public Vector2 TopRight => topRight;
    }
}