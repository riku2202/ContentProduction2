using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    // ���ł���I�u�W�F�N�g����^�O
    private const string MoveObjTag = "Moving";


    private const int NMagnetLayer = 6;
    private const int SMagnetLayer = 7;

    /// <summary>
    /// ����������
    /// </summary>
    private void Start()
    {
        
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