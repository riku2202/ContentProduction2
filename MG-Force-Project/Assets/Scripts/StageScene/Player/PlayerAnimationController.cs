using UnityEngine;

namespace Game.StageScene.Player
{
    public class PlayerAnimationController : PlayerControllerBase
    {
        #region -------- Animation íËêî --------

        private const string CURRENT_STATE = "CurrentState";

        private const string CURRENT_DIRECTION = "CurrentDirection";

        #endregion

        private enum AnimationState
        {
            NONE,
            IDLE,
            RUN,
            JUMP,
            SHOOT,
        }

        private enum AnimationLayer 
        {
            BASE,
            RIGHT,
            LEFT,
        }

        private Animator _animator;

        private AnimationState _currentAnimationState;

        private AnimationLayer _currentAnimationLayer;

        private float _currentAnimationTime;

        public override void OnStart()
        {
            _animator = playerObject.GetComponent<Animator>();

            _currentAnimationState = AnimationState.IDLE;

            _currentAnimationLayer = AnimationLayer.RIGHT;
        }

        public override void OnUpdate()
        {
            _animator.SetLayerWeight((int)_currentAnimationLayer, 0);

            if (currentDir == Direction.RIGHT)
            {
                _currentAnimationLayer = AnimationLayer.RIGHT;
            }
            else
            {
                _currentAnimationLayer = AnimationLayer.LEFT;
            }

            _animator.SetLayerWeight((int)_currentAnimationLayer, 1);

            SetAnimatioinDir();

            StateUpdate();

            if (_animator.GetInteger(CURRENT_DIRECTION) != (int)shootDir)
            {
                _animator.SetInteger(CURRENT_DIRECTION, (int)shootDir);
            }

            if (_animator.GetInteger(CURRENT_STATE) != (int)_currentAnimationState)
            {
                _animator.SetInteger(CURRENT_STATE, (int)_currentAnimationState);
            }
        }

        private void StateUpdate()
        {
            if ((currentState & State.SHOOT) != (int)State.NOT_STATE &&
                _currentAnimationState == AnimationState.SHOOT)
            {
                ShootUpdate();
            }

            if (((currentState & State.JUMP) != (int)State.NOT_STATE))
            {
                _currentAnimationState = AnimationState.JUMP;
            }
            else if (isGrounded)
            {                
                if ((currentState & State.RUN) != (int)State.NOT_STATE)
                {
                    _currentAnimationState = AnimationState.RUN;
                }
                else if ((currentState & State.STILLNESS) != (int)State.NOT_STATE)
                {
                    if ((currentState & State.SHOOT) != (int)State.NOT_STATE)
                    {
                        if (_currentAnimationState != AnimationState.SHOOT)
                        {
                            _currentAnimationTime = 0.0f;
                        }

                        _currentAnimationState = AnimationState.SHOOT;
                    }
                    else
                    {
                        _currentAnimationState = AnimationState.IDLE;
                    }
                }
            }
        }

        private void SetAnimatioinDir()
        {
            if (_currentAnimationLayer == AnimationLayer.RIGHT)
            {
                playerTransform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
            }
            else
            {
                playerTransform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
            }
        }

        private void ShootUpdate()
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo((int)_currentAnimationLayer);

            _currentAnimationTime = stateInfo.normalizedTime;

            if (_currentAnimationTime >= 0.7f)
            {
                _animator.Play(stateInfo.shortNameHash, (int)_currentAnimationLayer, 0.375f);
            }
        }
    }
}