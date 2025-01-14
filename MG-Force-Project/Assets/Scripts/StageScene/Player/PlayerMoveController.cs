using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

namespace Game.StageScene.Player 
{
    /// <summary>
    /// �v���C���[�̓���Ǘ��N���X(�A�j���[�V�����₻�̂ق��̏����͕ʂ̏ꏊ�ōs��)
    /// </summary>
    public class PlayerMoveController : PlayerControllerBase
    {
        #region -------- Move �萔 --------

        // �ő呬�x
        private const float MAX_SPEED = 54.0f;
        // �ŏ����x
        private const float MIN_SPEED = 0.0f;
        // �����x
        private const float ADD_SPEED = 10.125f;
        // �����x
        private const float SUB_SPEED = 54.0f;

        #endregion

        #region -------- Jump �萔 --------

        // �W�����v��
        private const float JUMP_POWER = 5.0f;

        private const float RAYCAST_LENGTH = 0.1f;

        #endregion

        private Rigidbody _rigidbody;

        private bool _isGrounded;

        // ���݂̑��x
        private float _currentSpeed;

        // �����x�N�g��
        private Vector3 moveDir = Vector3.zero;

        // �n�ʔ���p
        private Vector3 raycastDir = new Vector3(0.0f, -1.0f, 0.0f);

        public override void Init()
        {
            _rigidbody = playerObject.GetComponent<Rigidbody>();

            _currentSpeed = MIN_SPEED;
            _isGrounded = true;
        }

        public override void Update()
        {
            if ((currentState & State.STILLNESS) != (int)State.NOT_STATE) { StillnessUpdate(); }
            
            if ((currentState & State.RUN) != (int)State.NOT_STATE) { RunUpdate(); }
            
            if ((currentState & State.JUMP) != (int)State.NOT_STATE)
            {
                if (_isGrounded)
                {
                    JumpStart();
                }
                else
                {
                    JumpUpdate();
                }
            }

            // �͂�������
            _rigidbody.AddForce(moveDir, ForceMode.VelocityChange);
        }

        private void StillnessUpdate()
        {
            moveDir = new Vector3(Deceleration(), moveDir.y, moveDir.z);
        }

        private void RunUpdate()
        {
            // �ő呬�x���͊����̈ړ������Z�b�g����
            if (_currentSpeed >= MAX_SPEED)
            {
                _rigidbody.velocity = new Vector3(MIN_SPEED, _rigidbody.velocity.y, MIN_SPEED);

                _currentSpeed = MAX_SPEED;
            }

            if (currentDir == Direction.RIGHT)
            {
                moveDir = new Vector3(Acceleration(), moveDir.y, moveDir.z);
            }
            else
            {
                moveDir = new Vector3(-Acceleration(), moveDir.y, moveDir.z);
            }
        }

        private void JumpStart()
        {
            _isGrounded = false;
        }

        private void JumpUpdate()
        {
            bool hit_raycast = Physics.Raycast(playerTransform.position, raycastDir, RAYCAST_LENGTH);

            if (hit_raycast)
            {
                _isGrounded = true;

                currentState = currentState & ~State.JUMP;
            }

            Debug.DrawRay(playerTransform.position, raycastDir, Color.red, RAYCAST_LENGTH);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        private float Acceleration()
        {
            if (_currentSpeed < MAX_SPEED)
            {
                _currentSpeed += ADD_SPEED;

                if (_currentSpeed > MAX_SPEED)
                {
                    _currentSpeed = MAX_SPEED;
                }

                return _currentSpeed * Time.deltaTime;
            }

            return _currentSpeed * Time.deltaTime;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        private float Deceleration()
        {
            if (_currentSpeed > MIN_SPEED)
            {
                _currentSpeed -= SUB_SPEED;

                if (_currentSpeed < MIN_SPEED)
                {
                    _currentSpeed = MIN_SPEED;
                }

                return _currentSpeed * Time.deltaTime;
            }

            return _currentSpeed * Time.deltaTime;
        }
    }
}