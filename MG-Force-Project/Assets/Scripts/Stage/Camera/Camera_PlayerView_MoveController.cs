using Game.GameSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage.Camera
{

    public class CameraPlayerViewMoveController : MonoBehaviour
    {
        /*
        �J�������v���C���[��ǔ�����悤�ɂ���
        �������ȉ��̏����𖞂����������ɂ��邱��
         �E�v���C���[��K�������A��ʂ̓����ꏊ�Ɉڂ��K�v�͂Ȃ�(�v���C���[�͐�΂ɉf��)
         �E�v���C���[����ʂ̒��S�ł͂Ȃ��������ɂ���悤�ɂ���
         �E�J�������X�e�[�W�O���f���Ȃ��悤�ɂ���
         �E�v���C���[�̓���𒼐ړn�����A��ʂ����܂�h��Ȃ��悤�ɂ���
         �EZ���W�͌Œ��-10�ACamera�R���|�[�l���g�̐ݒ�͕ς��Ȃ��悤�ɂ���
        */

        // �����̒��_���W
        private Vector3 lowerLeft = GameConstants.LowerLeftCamera;

        // �E��̒��_���W
        private Vector3 topRight = GameConstants.TopRightCamera;

        // ���݂̃v���C���[�̈ʒu���
        private Transform currentPlayerPos;

        // �v���C���[�ւ̒ǔ��X�s�[�h
        [SerializeField]
        private float followSpeed = 5.0f;

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
            float targetX = Mathf.Clamp(currentPlayerPos.position.x, lowerLeft.x, topRight.x);
            float targetY = Mathf.Clamp(currentPlayerPos.position.y, lowerLeft.y, topRight.y);

            Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}