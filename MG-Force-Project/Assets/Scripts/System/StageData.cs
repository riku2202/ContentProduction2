using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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