using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System 
{
    /// <summary>
    /// �Q�[���f�[�^
    /// </summary>
    [System.Serializable]
    public class GameData
    {
        // �X�e�[�W�̍ő吔
        private const int STAGE_MAX_NUM = 8;

        // �X�e�[�W�̃N���A�t���O
        private bool[] IsClearStage = new bool[STAGE_MAX_NUM];
    }
}