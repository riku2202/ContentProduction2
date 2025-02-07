using UnityEngine;

namespace Game.StageScene.Player 
{
    /// <summary>
    /// �v���C���[�̓���Ǘ��N���X(�A�j���[�V�����₻�̂ق��̏����͕ʂ̏ꏊ�ōs��)
    /// </summary>
    public class PlayerMoveController : PlayerControllerBase
    {
        #region -------- Move �萔 --------

        // ���x
        private const float MOVE_SPEED = 10.0f;
        // �ŏ����x
        private const float MIN_SPEED = 0.0f;

        #endregion

        #region -------- Jump �萔 --------

        private const float RAYCAST_LENGTH = 0.825f;

        #endregion

        private Rigidbody _rigidbody;

        // �����x�N�g��
        private Vector3 moveDir = Vector3.zero;
        // �n�ʔ���p
        private Vector3 raycastDir = new Vector3(0.0f, -0.825f, 0.0f);

        // �n�ʂɓ������Ă��邩�̃t���O
        private bool _isGrounded;

        public override void Init()
        {
            _rigidbody = playerObject.GetComponent<Rigidbody>();

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
                    currentState = currentState & ~State.JUMP;
                }
            }

<<<<<<< HEAD
=======
#if false
            DebugManager.LogMessage($"{_isGrounded}");
            Debug.DrawRay(GameObject.Find("hip").transform.position, raycastDir, Color.red, RAYCAST_LENGTH);
#endif
            // @yu-kirohi
            // ����JumpUpdate���ł���Ă邱�ƂƓ������Ǝv��
            // ����Update����Find�g���Ă�̂͏d���Ȃ�v������
>>>>>>> origin/main
            _isGrounded = Physics.Raycast(GameObject.Find("hip").transform.position, raycastDir, RAYCAST_LENGTH);

            _rigidbody.velocity = moveDir;
        }

        private void StillnessUpdate()
        {
            moveDir = new Vector3(MIN_SPEED, _rigidbody.velocity.y, MIN_SPEED);
        }

        private void RunUpdate()
        {
            if (currentDir == Direction.RIGHT)
            {
                moveDir.x = MOVE_SPEED;
            }
            else
            {
                moveDir.x = -MOVE_SPEED;
            }
        }

        private void JumpStart()
        {
            _isGrounded = false;
        }
    }
}