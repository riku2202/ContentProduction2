using UnityEngine;

namespace Game.StageScene.Player
{
    public class PlayerControllerBase
    {
        protected enum State
        {
            NOT_STATE = 0,

            STILLNESS = 1 << 0,
            RUN = 1 << 1,
            JUMP = 1 << 2,
            SHOOT = 1 << 3,
        }

        protected enum Direction
        {
            LEFT,
            RIGHT,
        }

        protected static State currentState;

        protected static Direction currentDir;

        // プレイヤーオブジェクト
        protected static GameObject playerObject;

        protected static Transform playerTransform;

        public PlayerControllerBase() { }

        public PlayerControllerBase(GameObject player) 
        {
            playerObject = player;
            playerTransform = player.transform;
        }

        public virtual void Init() { }

        public virtual void Update() { }
    }
}