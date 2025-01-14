using Game.GameSystem;
using Game.StageScene.Magnet;
using UnityEngine;

namespace Game.StageScene.Player
{
    public class PlayerStateController : PlayerControllerBase
    {
        private InputHandler _inputHandler;

        private BulletShootController _bulletShoot;

        public override void Init()
        {
            _inputHandler = GameObject.Find(GameConstants.Object.INPUT).GetComponent<GameSystem.InputHandler>();

            _bulletShoot = playerObject.GetComponent<BulletShootController>();

            currentState = State.STILLNESS;

            currentDir = Direction.RIGHT;
        }

        public override void Update()
        {
            if ((currentState & State.RUN) == (int)State.NOT_STATE &&
                (currentState & State.JUMP) == (int)State.NOT_STATE)
            {
                ShootUpdate();
            }

            if ((currentState & State.SHOOT) == (int)State.NOT_STATE)
            {
                RunUpdate();

                JumpUpdate();
            }
        }

        private void RunUpdate()
        {
            // ˆÚ“®Žž‚Ìˆ—
            if (_inputHandler.IsActionPressing(InputConstants.Action.LEFTMOVE) &&
                playerTransform.position.x > GameConstants.LowerLeft.x)
            {
                currentState = currentState & ~State.STILLNESS;
                currentState = currentState | State.RUN;
                currentDir = Direction.LEFT;
            }
            else if (_inputHandler.IsActionPressing(InputConstants.Action.RIGHTMOVE) &&
                playerTransform.position.x < GameConstants.TopRight.x)
            {
                currentState = currentState & ~State.STILLNESS;
                currentState = currentState | State.RUN;
                currentDir = Direction.RIGHT;
            }
            else
            {
                currentState = currentState & ~State.RUN;
                currentState = currentState | State.STILLNESS;
            }
        }

        private void JumpUpdate()
        {
            // ƒWƒƒƒ“ƒvŽž‚Ìˆ—
            if (_inputHandler.IsActionPressed(InputConstants.Action.JUMP) &&
                (currentState & State.JUMP) == (int)State.NOT_STATE)
            {
                currentState = currentState & ~State.STILLNESS;
                currentState = currentState | State.JUMP;
            }
        }

        private void ShootUpdate()
        {
            if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT))
            {
                currentState = currentState & ~State.STILLNESS;
                currentState = currentState | State.SHOOT;
            }
            else
            {
                currentState = currentState & ~State.SHOOT;
                currentState = currentState | State.STILLNESS;
            }
        }

        //private void ShootUpdate()
        //{
        //    if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.South))
        //    {

        //    }
        //    else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.SouthEast))
        //    {

        //    }
        //}
    }
}