using UnityEngine;

namespace Game.Stage.Player
{
    public class PlayerAnimationController : PlayerControllerBase
    {
        #region -------- Animation íËêî --------

        private const string CURRENT_STATE = "CurrentState";

        private const string IS_LEFT_DIRECTION = "IsLeftDirection";

        #endregion

        private Animator _animator;

        public PlayerAnimationController(Animator animator)
        {
            _animator = animator;
        }

        public void Update()
        {
            if (_animator.GetBool(IS_LEFT_DIRECTION) != isLeftState)
            {
                _animator.SetBool(IS_LEFT_DIRECTION, isLeftState);
            }

            if (_animator.GetInteger(CURRENT_STATE) != (int)currentState)
            {
                _animator.SetInteger(CURRENT_STATE, (int)currentState);
            }

            //if (_animator.GetBool)
            //{

            //}
        }
    }
}