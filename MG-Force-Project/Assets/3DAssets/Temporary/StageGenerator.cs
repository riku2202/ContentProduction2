using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject StageBox;

    [SerializeField]
    private Transform Parentobj;

    [SerializeField]
    private int widthNum;

    [SerializeField]
    private int heightNum;

    [SerializeField]
    private float Startwidth;

    [SerializeField]
    private float Startheight;

    // ŠÔŠu
    private float Spacing = 0.5f;

    private void Start()
    {
        for (int i = 0; i < widthNum; i++)
        {
            for (int j = 0; j < heightNum; j++)
            {
                Vector3 stagePos = new Vector3(Startwidth + (Spacing * i), (Startheight + (Spacing * j)), 0.0f);

                Instantiate(StageBox, stagePos, Quaternion.identity, Parentobj);
            }
        }
    }
}
