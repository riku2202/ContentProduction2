using Game.Stage.Magnet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Stage
{
    /// <summary>
    /// �X�e�[�W�V�[���Ǘ��N���X
    /// </summary>
    public class StageSceneController : MonoBehaviour
    {
        // �t�F�[�Y
        private enum Phase
        {
            Reserve,
            Execution,
        }

        // ���݂̃t�F�[�Y
        private Phase currentPhase;

        private void Awake()
        {
            GameObject Input = GameObject.Find(GameConstants.INPUT_MANAGER_OBJ);

            //if (Input == null)
            //{
            //    SceneManager.LoadScene(GameConstants.Scene.Title.ToString());
            //}
        }

        private void Update()
        {
            if (currentPhase == Phase.Reserve)
            {

            }
            else if (currentPhase == Phase.Execution)
            {

            }
        }

        // ���ʂ̏���


        // ���͂����t�F�[�Y�̏���


        // ���͂��N�������t�F�[�Y�̏���

    }
}