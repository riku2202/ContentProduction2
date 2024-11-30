using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField]
    AudioSource bgmSource;
    // Start is called before the first frame update
    void Start()
    {
        //çƒê∂
        bgmSource.Play();
    }

    void StopBGM()
    {
        //í‚é~
        bgmSource.Stop();
    }
    
}
