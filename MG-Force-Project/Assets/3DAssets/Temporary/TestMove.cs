using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 Position = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            Position.x -= MoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Position.x += MoveSpeed * Time.deltaTime;
        }

        transform.position = Position;
    }
}
