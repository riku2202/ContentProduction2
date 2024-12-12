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
        private Transform currentPlayerPos;

        // �v���C���[�ւ̒ǔ��X�s�[�h
        [SerializeField]
        private float followSpeed = 5.0f;

        //// �J������Y���W�̃I�t�Z�b�g
        //private const float CAMERA_Y_OFFSET = -1.0f;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            currentPlayerPos = GameObject.Find(GameConstants.PLAYER_OBJ).GetComponent<Transform>();
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            // null�`�F�b�N
            if (currentPlayerPos == null) return;

            // �v���C���[��ǔ�
            TrackThePlayer();
        }

        /// <summary>
        /// �v���C���[��ǔ�
        /// </summary>
        private void TrackThePlayer()
        {
            float target_x = Mathf.Clamp(currentPlayerPos.position.x, lowerLeft.x, topRight.x);
            float target_y = Mathf.Clamp(currentPlayerPos.position.y, lowerLeft.y, topRight.y);

            Vector3 target_pos = new Vector3(target_x, target_y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, target_pos, followSpeed * Time.deltaTime);
        }
    }
}