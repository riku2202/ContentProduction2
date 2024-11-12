using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDisappearOnCollision : MonoBehaviour
{
    [SerializeField] private string targetTag = "Box"; // �Փ˂ŏ������������I�u�W�F�N�g�̃^�O

    private void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g���w�肵���^�O�������Ă��邩�m�F
        if (collision.gameObject.CompareTag(targetTag))
        {
            // �Փ˂����I�u�W�F�N�g�Ǝ��g���폜
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
