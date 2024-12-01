using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{
    [SerializeField]
    AudioSource seSource;
    [SerializeField]
    AudioClip seClip;   //SEƒtƒ@ƒCƒ‹‚ğ“ü‚ê‚é
    // Start is called before the first frame update
   void PlaySE()
    {
        //SE‚ğˆê‰ñÄ¶
        seSource.PlayOneShot(seClip);
    }
}
