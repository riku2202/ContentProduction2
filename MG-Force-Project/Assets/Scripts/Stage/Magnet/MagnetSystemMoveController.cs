using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSystemMoveController : MonoBehaviour
{
    [SerializeField]
    private GameObject MainCamera;

    private void FixedUpdate()
    {
        Vector3 new_position = MainCamera.transform.position;

        new_position.z = GameConstants.RESET;

        transform.position = new_position;
    }
}
