using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{
    [SerializeField]
    AudioSource seSource;
    [SerializeField]
    AudioClip seClip;   //SE�t�@�C��������
    // Start is called before the first frame update
   void PlaySE()
    {
        //SE�����Đ�
        seSource.PlayOneShot(seClip);
    }
}
