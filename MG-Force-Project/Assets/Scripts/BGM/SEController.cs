using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{
    [SerializeField]
    AudioSource seSource;
    [SerializeField]
    AudioClip seClip;   //SEファイルを入れる
    // Start is called before the first frame update
   void PlaySE()
    {
        //SEを一回再生
        seSource.PlayOneShot(seClip);
    }
}
