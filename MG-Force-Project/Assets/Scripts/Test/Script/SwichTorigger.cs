using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichTorigger : MonoBehaviour
{
    float bottomY = 7.0f;
    float speed = 0.5f;

    bool active;

   public DoorController door;
    void Update()
    {
        if (active && transform.position.y > bottomY)
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }

        if(transform.position.y <= bottomY)
        {
            door.isOpen = true;
            enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active && other.CompareTag("Player"))
        {
            active = true;
        }
    }
}