using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    // 可動できるオブジェクト判定タグ
    private const string MoveObjTag = "Moving";


    private const int NMagnetLayer = 6;
    private const int SMagnetLayer = 7;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        
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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(MoveObjTag))
        {
            if (gameObject.layer == NMagnetLayer)
            {

            }
            else if (gameObject.layer == SMagnetLayer)
            {

            }

            if (other.gameObject.layer == NMagnetLayer)
            {

            }
            else if (other.gameObject.layer == SMagnetLayer)
            {

            }
        }
    }
}