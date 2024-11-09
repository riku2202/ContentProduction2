using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System
{
    /// <summary>
    /// �X�e�[�W�f�[�^
    /// </summary>
    [CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage Data")]
    public class StageData : ScriptableObject
    {
        // �X�e�[�W�̊Ǘ��ԍ�
        [SerializeField]
        private int StageIndex;

        // �X�e�[�W�̃v���n�u
        [SerializeField]
        private GameObject StagePrefab;
    }
}