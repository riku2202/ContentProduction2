using UnityEngine;

namespace Game.Stage.Player 
{
    /// <summary>
    /// �v���C���[�̓���Ǘ��N���X(�A�j���[�V�����₻�̂ق��̏����͕ʂ̏ꏊ�ōs��)
    /// </summary>
    public class PlayerMoveController : MonoBehaviour
    {
        private GameSystem.InputHandler _input;
        
        // �ő呬�x
        private const float MAX_SPEED = 120.0f;
        // �ŏ����x
        private const float MIN_SPEED = 0.0f;
        // �����x
        private const float ADD_SPEED = 30.0f;
        // �����x
        private const float SUB_SPEED = 60.0f;

        // �W�����v��
        private const float JUMP_POWER = 1.0f;

        // ���݂̑��x
        private float currentSpeed = 0.0f;

        // �v���C���[��Rigidbody
        private Rigidbody rb;

        // �v���C���[�̓���t���O
        private bool isActive = true;

        [SerializeField]
        // �n�ʂɐݒu���Ă��邩�ǂ���
        private bool isGranded = true;

        // �����x�N�g��
        private Vector3 moveDir = Vector3.zero;

        private Vector3 raycastDir = new Vector3(0.0f, -1.0f, 0.0f);

        private const float RAYCAST_LENGTH = 0.1f;

        /// <summary>
        /// ����������
        /// </summary>
        private void Start()
        {
            _input = GameObject.Find("InputManager").GetComponent<GameSystem.InputHandler>();

            rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void FixedUpdate()
        {
            // �����`�F�b�N
            if (!isActive) return;

            if (!isGranded)
            {
                CheckGranded();
            }

            // �ő呬�x���͊����̈ړ������Z�b�g����
            if (currentSpeed == MAX_SPEED)
            {
                rb.velocity = new Vector3(MIN_SPEED, rb.velocity.y, MIN_SPEED);
            }

            // �E�ړ�
            if (_input.IsActionPressing(GameConstants.Input.Action.RIGHTMOVE) && transform.position.x < GameConstants.TopRight.x)
            {
                moveDir = new Vector3(Acceleration(), moveDir.y, moveDir.z);
            }
            // ���ړ�
            else if (_input.IsActionPressing(GameConstants.Input.Action.LEFTMOVE) && transform.position.x > GameConstants.LowerLeft.x)
            {
                moveDir = new Vector3(-Acceleration(), moveDir.y, moveDir.z);
            }
            // ��~
            else
            {
                moveDir = new Vector3(Deceleration(), rb.velocity.y, rb.velocity.z);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGranded)
            {
                moveDir = new Vector3(moveDir.x, JUMP_POWER, moveDir.z);
                isGranded = false;
            }

            // �͂�������
            rb.AddForce(moveDir, ForceMode.Force);

            // �����n�ړ�(�f�o�b�N�p)
            if (Input.GetKeyDown(KeyCode.Q))
            {
                transform.position = Vector3.zero;
                rb.velocity = Vector3.zero;
            }
        }

        private void CheckGranded()
        {
            isGranded =  Physics.Raycast(gameObject.transform.position, raycastDir, RAYCAST_LENGTH);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        private float Acceleration()
        {
            if (currentSpeed < MAX_SPEED)
            {
                currentSpeed += ADD_SPEED;

                if (currentSpeed > MAX_SPEED)
                {
                    currentSpeed = MAX_SPEED;
                }

                return currentSpeed;
            }

            return currentSpeed;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        private float Deceleration()
        {
            if (currentSpeed > MIN_SPEED)
            {
                currentSpeed -= SUB_SPEED;

                if (currentSpeed < MIN_SPEED)
                {
                    currentSpeed = MIN_SPEED;
                }

                return currentSpeed;
            }

            return currentSpeed;
        }
    }
}