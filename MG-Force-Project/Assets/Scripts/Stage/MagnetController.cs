using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 磁力管理クラス
/// </summary>
public class MagnetController : MonoBehaviour
{
    // 可動できるオブジェクト判定タグ
    private const string MoveObjTag = "Moving";
    
    // 固定オブジェクト判定タグ
    private const string FixedObjTage = "Fixed";

    // 磁石のレイヤー
    private const int NMagnetLayer = 6;  // N極
    private const int SMagnetLayer = 7;  // S極

    enum ObjType
    {
        NMove,
        SMove,
        NFixed,
        SFixed,
        NotType,
    }

    private ObjType MyType;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        MyType = SetType(gameObject);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {

    }


    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        switch (SetType(other.gameObject)) 
        {
            case ObjType.NMove:
                break;
        }
    }

    private ObjType SetType(GameObject obj)
    {
        if (obj.layer == NMagnetLayer)
        {
            if (obj.CompareTag(MoveObjTag))
            {
                return ObjType.NMove;
            }
            else if (obj.CompareTag(FixedObjTage))
            {
                return ObjType.NFixed;
            }
        }
        else if (obj.layer == SMagnetLayer)
        {
            if (obj.CompareTag(MoveObjTag))
            {
                return ObjType.SMove;
            }
            else if (obj.CompareTag(FixedObjTage))
            {
                return ObjType.SFixed;
            }
        }

        return ObjType.NotType;
    }
}