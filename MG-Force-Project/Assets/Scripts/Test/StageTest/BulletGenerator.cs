using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject BulletPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 InitPos = transform.position;

            GameObject ob = Instantiate(BulletPrefab);

            ob.transform.position = InitPos;
        }
    }
}