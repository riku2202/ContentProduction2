using UnityEngine;

namespace Game.Stage
{
    /// <summary>
    /// �X�e�[�W�f�[�^
    /// </summary>
    [CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage Data")]
    public class StageData : ScriptableObject
    {
        // �X�e�[�W�̊Ǘ��ԍ�
        [SerializeField]
        private int stageIndex;

        // �X�e�[�W�̃v���n�u
        [SerializeField]
        private GameObject stagePrefab;

        // �X�e�[�W�̔w�i
        [SerializeField]
        private GameObject stageBG;

        // �X�e�[�W�̉E����W
        [SerializeField]
        private Vector2 topRight;


        /* -------- �ǂݎ��p�̕ϐ� -------- */

        // �X�e�[�W�̊Ǘ��ԍ�
        public int StageIndex => stageIndex;

        // �X�e�[�W�̃v���n�u
        public GameObject StagePrefab => stagePrefab;

        // �X�e�[�W�̔w�i
        public GameObject StageBG => stageBG;

        // �X�e�[�W�̉E����W
        public Vector2 TopRight => topRight;
    }
}