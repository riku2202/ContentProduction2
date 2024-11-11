using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float BulletSpeed = 1.0f;

    // �e��Rigidbody
    private Rigidbody Bullet = null;

    // �e�̍��W
    private Vector3 BulletPos = Vector3.zero;

    // �^�[�Q�b�g�̍��W
    private Vector3 TargetPos = Vector3.zero;

    // �����x�N�g��
    private Vector3 Direction  = Vector3.zero;

    /// <summary>
    /// ����������
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
    /// �e�̔��ˏ���
    /// </summary>
    public void FiringBullet()
    {
        // �����x�N�g�������߂�
        Direction = TargetPos - BulletPos;

        // ���K��
        Direction.Normalize();

        // ���x����Z
        Direction *= BulletSpeed;

        // �����x�N�g���ɉ����Ĉړ�������
        Bullet.AddForce(Direction, ForceMode.Impulse);
    }
}