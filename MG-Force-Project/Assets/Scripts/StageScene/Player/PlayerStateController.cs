using Game.GameSystem;
using Game.StageScene.Magnet;
using UnityEngine;

// @yu-ki-rohi
// おそらくは自機をステージと同時に生成するという都合上、
// こういう形の実装になったのかな、と見ていますが
// 自機が必ず存在しており、ステージごとに変わることもないので
// 最初からシーンに設置しておいた方が作りやすいように思います。
// その方がPlayerInputのEventsに直接処理を登録できるので。
// その場合でもStageCreaterから座標をいじれば、遜色なくつかえるかと。

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
            // 移動時の処理
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
            // ジャンプ時の処理
            if (_inputHandler.IsActionPressed(InputConstants.Action.JUMP) &&
                (currentState & State.JUMP) == (int)State.NOT_STATE)
            {
                currentState = currentState & ~State.STILLNESS;
                currentState = currentState | State.JUMP;
            }
            else
            {
                currentState = currentState & ~State.JUMP;
                currentState = currentState | State.STILLNESS;
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