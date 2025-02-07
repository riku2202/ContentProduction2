using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private GameObject _gameObject;
    private Transform _transform;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _gameObject = GetComponent<GameObject>();
    }
}
