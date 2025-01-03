using UnityEngine;

namespace Game.Stage.Player
{
    public class PlayerControllerBase
    {
        public enum State
        {
            NOT_STATE = -1,

            STILLNESS,
            RUN,
            JUMP,
            SHOOT,
        }

        protected static State currentState;

        protected static bool isLeftState;

        protected static bool isGranded;

        protected Rigidbody rigidbody;

        public void ChangeState(State state) 
        {
            if (currentState == state) {  return; }

            currentState = state;
        }

        public void SetDirState(bool dir)
        {
            isLeftState = dir;
        }

        public bool GetIsGranded()
        {
            return isGranded;
        }
    }
}