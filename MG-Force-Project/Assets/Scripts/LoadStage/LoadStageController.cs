using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NowLowring : MonoBehaviour
{
    private float counttime = 0.0f;
    public float timeLimit = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counttime += Time.deltaTime;

        if(counttime > timeLimit)
        {
            SceneManager.LoadScene("StageSelect");
        }
    }
}
