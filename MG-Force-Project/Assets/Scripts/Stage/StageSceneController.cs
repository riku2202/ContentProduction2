using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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