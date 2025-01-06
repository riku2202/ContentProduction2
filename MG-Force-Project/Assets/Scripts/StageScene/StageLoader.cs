using UnityEngine;

using Game.GameSystem;
using UnityEngine.SceneManagement;

namespace Game.StageScene
{
    /// <summary>
    /// �X�e�[�W���[�h�N���X
    /// </summary>
    public class StageLoader : MonoBehaviour 
    {
        private GameDataManager gameDataManager;

        // �X�e�[�W�f�[�^
        [SerializeField]
        private StageData[] datas;

        [SerializeField]
        private GameConstants.Stage currentStage;

        private Transform childTransform;

        private StageCreater stageCreater;

        private void Awake()
        {
            gameDataManager = GameDataManager.Instance;

            Scene scene = SceneManager.GetActiveScene();

            if (scene.buildIndex == (int)GameConstants.Scene.StageSelect)
            {
                gameDataManager.SetCurrentStageIndex((int)currentStage);

                // �X�e�[�W�̐���
                SetStage();

                return;
            }

            gameDataManager.SetCurrentStageIndex((int)currentStage);

            SetStage();
        }

        /// <summary>
        /// �X�e�[�W�̐���
        /// </summary>
        public void SetStage(bool external_data = true)
        {
            if (external_data)
            {
                childTransform = gameObject.transform.Find("StageCreater");
                stageCreater = childTransform.GetComponent<StageCreater>();
                stageCreater.StageCreate();
                stageCreater.BGCreate();
                return;
            }

            int stage_index = gameDataManager.GetCurrentStageIndex();

            Transform transform = GameObject.Find(GameConstants.MAIN_CAMERA).transform;

            Instantiate(datas[stage_index].StagePrefab, Vector3.zero, Quaternion.identity);
            Instantiate(datas[stage_index].StageBG, Vector3.zero, Quaternion.identity, transform);
        }

        /// <summary>
        /// �X�e�[�W�f�[�^�̎擾
        /// </summary>
        /// <param name="stage_index"></param>
        /// <returns></returns>
        public StageData GetStageData(int stage_index)
        {
            DebugManager.LogMessage($"{stage_index}");

            return datas[stage_index];
        }
    }
}