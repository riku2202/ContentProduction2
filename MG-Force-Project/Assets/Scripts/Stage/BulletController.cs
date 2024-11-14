using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// �e�̎��s����
/// </summary>
public class BulletController : MonoBehaviour
{
    // �e�̑��x
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

    [SerializeField]
    private Material Nmaterial;

    /// <summary>
    /// ����������
    /// </summary>
    private void Start()
    {
        Bullet = GetComponent<Rigidbody>();

        // �e�̍��W�擾
        BulletPos = Bullet.position;

        if (Input.GetMouseButtonDown(0))
        {
            // �^�[�Q�b�g�m�F�p
            GameObject target = GameObject.Find("Target");

            // �^�[�Q�b�g�����݂���ꍇ
            if (target != null)
            {
                // �^�[�Q�b�g�̍��W�擾
                TargetPos = target.GetComponent<Rigidbody>().position;

                //// �e�̔���
                FiringBullet();
            }
            // �^�[�Q�b�g�����݂��Ȃ��ꍇ
            else
            {
                Debug.Log("�ySystem�z�G���[�@�^�[�Q�b�g�����݂��܂���");
            }
        }
    }

    private void Update()
    {
        Debug.Log(Direction);

        if (transform.position.x < -10 || transform.position.x > 10 ||
            transform.position.y < -10 || transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �e�̔��ˏ���
    /// </summary>
    private void FiringBullet()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moving"))
        {

        }
        else if (other.CompareTag("Fixed"))
        {
            Debug.Log("test");
            
            Renderer rend = other.GetComponent<Renderer>();

            rend.material = Nmaterial;

            Destroy(gameObject);
        }
    }
}