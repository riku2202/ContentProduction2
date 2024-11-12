using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDisappearOnCollision : MonoBehaviour
{
    [SerializeField] private string targetTag = "Box"; // 衝突で消失させたいオブジェクトのタグ

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが指定したタグを持っているか確認
        if (collision.gameObject.CompareTag(targetTag))
        {
            // 衝突したオブジェクトと自身を削除
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
