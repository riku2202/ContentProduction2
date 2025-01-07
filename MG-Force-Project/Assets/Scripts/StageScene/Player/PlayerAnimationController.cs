using UnityEngine;

namespace Game.StageScene.Player
{
    public class PlayerAnimationController : PlayerControllerBase
    {
        #region -------- Animation íËêî --------

        private const string CURRENT_STATE = "CurrentState";

        private const string CURRENT_DIRECTION = "CurrentDirection";

        #endregion

        protected enum AnimationState
        {
            IDLE,
            RUNNING,
            JUMPING,
            ON_CHAGE,
        }

        private Animator _animator;

        private AnimationState _currentAnimState;

        public override void Init()
        {
            _animator = playerObject.GetComponent<Animator>();

            _currentAnimState = AnimationState.IDLE;
        }

        public override void Update()
        {
            StateUpdate();

            if (_animator.GetInteger(CURRENT_DIRECTION) != (int)currentDir)
            {
                _animator.SetInteger(CURRENT_DIRECTION, (int)currentDir);
            }

            if (_animator.GetInteger(CURRENT_STATE) != (int)_currentAnimState)
            {
                _animator.SetInteger(CURRENT_STATE, (int)_currentAnimState);
            }

            SetAnimatioinDir();
        }

        private void StateUpdate()
        {
            if ((currentState & State.JUMP) != (int)State.NOT_STATE)
            {
                _currentAnimState = AnimationState.JUMPING;
            }
            else if ((currentState & State.SHOOT) != (int)State.NOT_STATE)
            {
                _currentAnimState = AnimationState.ON_CHAGE;
            }
            else if ((currentState & State.RUN) != (int)State.NOT_STATE)
            {
                _currentAnimState = AnimationState.RUNNING;
            }
            else if ((currentState & State.STILLNESS) != (int)State.NOT_STATE)
            {
                _currentAnimState = AnimationState.IDLE;
            }
        }

        private Vector3 _dir;

        private void SetAnimatioinDir()
        {
            _dir = playerTransform.eulerAngles;

            if (currentDir == Direction.RIGHT && _dir.y > 90.0f)
            {
                RightChange();
            }
            else if (currentDir == Direction.LEFT && _dir.y < 240.0f)
            {
                LeftChange(); 
            }
        }

        private void LeftChange()
        {
            _dir.y += 10.0f;

            playerTransform.eulerAngles = _dir;
        }

        private void RightChange()
        {
            _dir.y -= 10.0f;

            playerTransform.eulerAngles = _dir;
        }
    }
}