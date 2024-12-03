using UnityEditor;
using UnityEngine;

using Game.GameSystem;
using UnityEngine.SceneManagement;

namespace Game.Stage
{
    /// <summary>
    /// �X�e�[�W���[�h�N���X
    /// </summary>
    public class StageLoader : MonoBehaviour 
    {
        private GameDataManager gameDataManager;

        // �X�e�[�W�f�[�^
        [SerializeField]
        private StageData[] Data;

        private void Awake()
        {
            gameDataManager = GameDataManager.Instance;

            Scene scene = SceneManager.GetActiveScene();

            if (scene.buildIndex == (int)GameConstants.Scene.StageSelect)
            {
                gameDataManager.SetCurrentStageIndex((int)GameConstants.Stage.SELECT);
                SetStage();
            }
        }

        /// <summary>
        /// �X�e�[�W�̐���
        /// </summary>
        public void SetStage()
        {
            int stage_index = gameDataManager.GetCurrentStageIndex();

            Transform transform = GameObject.Find(GameConstants.MAIN_CAMERA_OBJ).transform;

            GameObject stage = Instantiate(Data[stage_index].StagePrefab, Vector3.zero, Quaternion.identity);
            GameObject bg = Instantiate(Data[stage_index].StageBG, Vector3.zero, Quaternion.identity, transform);
        }

        /// <summary>
        /// �X�e�[�W�f�[�^�̎擾
        /// </summary>
        /// <param name="stage_index"></param>
        /// <returns></returns>
        public StageData GetStageData(int stage_index)
        {
            return Data[stage_index];
        }
    }
}