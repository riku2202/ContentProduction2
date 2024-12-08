using Game.GameSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage.Camera
{

    public class Camera_PlayerView_MoveController : MonoBehaviour
    {
        // �J�������v���C���[��ǔ�����悤�ɂ���
        // �������ȉ��̏����𖞂����������ɂ��邱��

        // �E�v���C���[��K�������A��ʂ̓����ꏊ�Ɉڂ��K�v�͂Ȃ�(�v���C���[�͐�΂ɉf��)
        // �E�v���C���[����ʂ̒��S�ł͂Ȃ��������ɂ���悤�ɂ���
        // �E�J�������X�e�[�W�O���f���Ȃ��悤�ɂ���
        // �E�v���C���[�̓���𒼐ړn�����A��ʂ����܂�h��Ȃ��悤�ɂ���
        // �EZ���W�͌Œ��-10�ACamera�R���|�[�l���g�̐ݒ�͕ς��Ȃ��悤�ɂ���

        private Transform PlayerTransform;

        private Vector3 LowerLeft = GameConstants.LowerLeftCamera;

        private Vector3 TopRight;

        private Vector3 LastPlayerPos;

        [SerializeField]
        private float followSpeed = 5.0f;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            Physics.gravity = new Vector3(0, -1.0f, 0);

            StageLoader stage_loader = GameObject.Find("StageLoader").GetComponent<StageLoader>();

            GameDataManager gamedata_manager = GameDataManager.Instance;

            TopRight = stage_loader.GetStageData(gamedata_manager.GetCurrentStageIndex()).TopRight;
            TopRight.z = -10;

            transform.position = LowerLeft;

            LastPlayerPos = LowerLeft;
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            GameObject player = GameObject.Find(GameConstants.PLAYER_OBJ);

            if (player == null) return;

            PlayerTransform = player.GetComponent<Transform>();

            if (PlayerTransform.position != LastPlayerPos)
            {
                DebugManager.LogMessage("test");
                FollowPlayer();
                LastPlayerPos = PlayerTransform.position;
            }
        }

        private void FollowPlayer()
        {
            float targetX = Mathf.Clamp(PlayerTransform.position.x, LowerLeft.x, TopRight.x);
            float targetY = Mathf.Clamp(PlayerTransform.position.y, LowerLeft.y, TopRight.y);

            Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}