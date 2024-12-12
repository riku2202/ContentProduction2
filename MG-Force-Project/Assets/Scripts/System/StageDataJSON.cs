using UnityEngine;

namespace Game.Stage 
{
    /// <summary>
    /// �X�e�[�W�f�[�^(JSON)
    /// </summary>
    [System.Serializable]
    public class StageDataJSON
    {
        // �X�e�[�W�̊Ǘ��ԍ�
        private int stageIndex;

        // �X�e�[�W�̃I�u�W�F�N�g
        private GameObject stageObject;

        // �X�e�[�W�̔w�i
        private GameObject stageBG;

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
            stageIndex = stage_index;
            stageObject = stage_object;
            stageBG = stage_bg;
            topRight = top_right;
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data"></param>
        public StageDataJSON(StageDataJSON data)
        {
            stageIndex = data.StageIndex;
            stageObject = data.StageObject;
            stageBG = data.StageBG;
            topRight = data.TopRight;
        }


        /* -------- �ǂݎ��p�̕ϐ� -------- */

        // �X�e�[�W�̊Ǘ��ԍ�
        public int StageIndex => stageIndex;

        // �X�e�[�W�̃I�u�W�F�N�g
        public GameObject StageObject => stageObject;

        // �X�e�[�W�̔w�i
        public GameObject StageBG => stageBG;

        // �X�e�[�W�̉E����W
        public Vector2 TopRight => topRight;
    }
}