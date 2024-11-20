using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private void Update()
    {
        Vector3 screenPosition = Input.mousePosition;

        // Z���̈ʒu���w��
        screenPosition.z = Mathf.Abs(Camera.main.transform.position.z); // �܂��͎w�肵�����[�x

        // ���[���h���W�ɕϊ�
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        transform.position = worldPosition;
    }
}