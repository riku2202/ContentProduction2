using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private void Update()
    {
        Vector3 screenPosition = Input.mousePosition;

        // Z軸の位置を指定
        screenPosition.z = Mathf.Abs(Camera.main.transform.position.z); // または指定したい深度

        // ワールド座標に変換
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        transform.position = worldPosition;
    }
}