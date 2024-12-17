using Game.GameSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage.Camera
{

    public class CameraPlayerViewMoveController : MonoBehaviour
    {
        // �����̒��_���W
        private Vector3 lowerLeft = GameConstants.LowerLeftCamera;

        // �E��̒��_���W
        private Vector3 topRight = GameConstants.TopRightCamera;

        // ���݂̃v���C���[�̈ʒu���
        private Transform currentPlayerTransform;

        // �v���C���[�ւ̒ǔ��X�s�[�h
        [SerializeField]
        private float followSpeed = 5.0f;

        // �v���C���[�Ƃ�Y���̍�
        private const float Y_DIFF_TO_PLAYER = 1.0f;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            currentPlayerTransform = GameObject.Find(GameConstants.PLAYER_OBJ).GetComponent<Transform>();
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            // null�`�F�b�N
            if (currentPlayerTransform == null) return;

            // �v���C���[��ǔ�
            TrackThePlayer();
        }

        /// <summary>
        /// �v���C���[��ǔ�
        /// </summary>
        private void TrackThePlayer()
        {
            float target_x = Mathf.Clamp(currentPlayerTransform.position.x, lowerLeft.x, topRight.x);
            float target_y = Mathf.Clamp(currentPlayerTransform.position.y, lowerLeft.y, topRight.y);

            Vector3 target_pos = new Vector3(target_x, target_y, transform.position.z);

            //if (currentPlayerPositioin.y - transform.position.y > Y_DIFF_TO_PLAYER)
            //{
            //    target_pos.y = Mathf.Clamp(currentPlayerPositioin.y + (currentPlayerPositioin.y - transform.position.y), lowerLeft.y, topRight.y);
            //}


            DebugManager.LogMessage(target_pos.x + " " + target_pos.y + " " + target_pos.z);

            transform.position = Vector3.Lerp(transform.position, target_pos, followSpeed * Time.deltaTime);
        }
    }
}