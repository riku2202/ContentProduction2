using UnityEngine;
using Game.StageScene.Magnet;
using Game.GameSystem;

namespace Game.StageScene.Player
{
    public class PlayerStateController : PlayerControllerBase
    {
        private InputHandler _inputHandler;

        private BulletShootController _bulletShoot;

        public override void OnStart()
        {
            _inputHandler = GameObject.Find(GameConstants.Object.INPUT).GetComponent<GameSystem.InputHandler>();
            _bulletShoot = playerObject.GetComponent<BulletShootController>();

            currentState = State.STILLNESS;
            currentDir = Direction.RIGHT;
        }

        public override void OnUpdate()
        {
            if ((currentState & State.STILLNESS) != (int)State.NOT_STATE)
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
            // ¶ˆÚ“®Žž
            if (_inputHandler.IsActionPressing(InputConstants.Action.LEFTMOVE) &&
                playerTransform.position.x > GameConstants.LowerLeft.x)
            {
                currentState = currentState & ~State.STILLNESS;
                currentState = currentState | State.RUN;
                currentDir = Direction.LEFT;
            }
            // ‰EˆÚ“®Žž
            else if (_inputHandler.IsActionPressing(InputConstants.Action.RIGHTMOVE) &&
                playerTransform.position.x < GameConstants.TopRight.x)
            {
                currentState = currentState & ~State.STILLNESS;
                currentState = currentState | State.RUN;
                currentDir = Direction.RIGHT;
            }
            // ˆÚ“®’âŽ~Žž
            else
            {
                currentState = currentState & ~State.RUN;

                if ((currentState & State.JUMP) == (int)State.NOT_STATE)
                {
                    currentState = currentState | State.STILLNESS;
                }
            }
        }

        private void JumpUpdate()
        {
            if (isGrounded)
            {
                // ƒWƒƒƒ“ƒvŽž
                if (_inputHandler.IsActionPressed(InputConstants.Action.JUMP) &&
                    (currentState & State.JUMP) == (int)State.NOT_STATE)
                {
                    currentState = currentState & ~State.STILLNESS;
                    currentState = currentState | State.JUMP;
                }
                else
                {
                    currentState = currentState & ~State.JUMP;

                    if ((currentState & State.RUN) == (int)State.NOT_STATE)
                    {
                        currentState = currentState | State.STILLNESS;
                    }
                }
            }
        }

        private void ShootUpdate()
        {
            if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT) && !_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_CANCEL))
            {
                if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.North))
                {
                    shootDir = 0;
                }
                else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.NorthEast))
                {
                    currentDir = Direction.RIGHT;
                    shootDir = 45;
                }
                else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.East))
                {
                    currentDir = Direction.RIGHT;
                    shootDir = 90;
                }
                else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.SouthEast))
                {
                    currentDir = Direction.RIGHT;
                    shootDir = 135;
                }
                else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.NorthWest))
                {
                    currentDir = Direction.LEFT;
                    shootDir = 45;
                }
                else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.West))
                {
                    currentDir = Direction.LEFT;
                    shootDir = 90;
                }
                else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.SouthWest))
                {
                    currentDir = Direction.LEFT;
                    shootDir = 135;
                }

                currentState = currentState | State.SHOOT;
            }
            else
            {
                currentState = currentState & ~State.SHOOT;
            }
        }
    }
}