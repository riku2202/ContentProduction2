using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���͊Ǘ��N���X
/// </summary>
public class MagnetController : MonoBehaviour
{
    // ���ł���I�u�W�F�N�g����^�O
    private const string MoveObjTag = "Moving";
    
    // �Œ�I�u�W�F�N�g����^�O
    private const string FixedObjTage = "Fixed";

    // ���΂̃��C���[
    private const int NMagnetLayer = 6;  // N��
    private const int SMagnetLayer = 7;  // S��

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
    /// ����������
    /// </summary>
    private void Start()
    {
        MyType = SetType(gameObject);
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void Update()
    {

    }


    /// <summary>
    /// �����蔻��
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