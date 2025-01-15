using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.ClearScene
{
    using GameSystem;

    /// <summary>
    /// �N���A�V�[���Ǘ��N���X
    /// </summary>
    public class ClearSceneController : MonoBehaviour
    {
        GameDataManager gameDataManager;

        // �e�X�e�[�W�N���A���ɌĂяo����鏈��

        // �S�X�e�[�W�N���A���ɌĂяo����鏈��

        // �t���O�Ǘ���GameDataManager����GameData���Ăяo���Ă���(�Ăяo�������̓X�e�[�W�̃C���f�b�N�X)]

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            // �Q�[���f�[�^�Ǘ��N���X�̌Ăяo��
            gameDataManager = GameDataManager.Instance;

            // �X�e�[�W�N���A
            gameDataManager.GetGameData().SetIsClearStage(gameDataManager.GetCurrentStageIndex());

            // �S�X�e�[�W�N���A������
            if (ClearChack() == true)
            {
                EndCredits();
            }
            else
            {
                SceneManager.LoadScene("StageSelect");
            }
        }

        /// <summary>
        /// �N���A�`�F�b�N
        /// </summary>
        /// <returns></returns>
        private bool ClearChack()
        {
            for (int i = 0; i < GameConstants.STAGE_MAX_NUM; i++)
            {
                if (gameDataManager.GetGameData().GetIsClearStage(i) == false)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// �G���h���[��
        /// </summary>
        private void EndCredits()
        {

        }
    }
}