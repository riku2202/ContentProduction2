using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float BulletSpeed = 1.0f;

    // 弾のRigidbody
    private Rigidbody Bullet = null;

    // 弾の座標
    private Vector3 BulletPos = Vector3.zero;

    // ターゲットの座標
    private Vector3 TargetPos = Vector3.zero;

    // 向きベクトル
    private Vector3 Direction  = Vector3.zero;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        Bullet = GetComponent<Rigidbody>();

        BulletPos = Bullet.position;

        GameObject target = GameObject.Find("target");

        if (target != null)
        {
            TargetPos = target.GetComponent<Rigidbody>().position;
        }
        else
        {

        }
    }

    /// <summary>
    /// 弾の発射処理
    /// </summary>
    public void FiringBullet()
    {
        // 向きベクトルを求める
        Direction = TargetPos - BulletPos;

        // 正規化
        Direction.Normalize();

        // 速度を乗算
        Direction *= BulletSpeed;

        // 向きベクトルに応じて移動させる
        Bullet.AddForce(Direction, ForceMode.Impulse);
    }
}