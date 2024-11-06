using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    // ステージの管理番号
    [SerializeField]
    private int StageIndex;

    // ステージのプレハブ
    [SerializeField]
    private GameObject StagePrefab;
}