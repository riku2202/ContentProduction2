using UnityEngine;

namespace Game.Stage.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private GameSystem.InputHandler _inputHandler;

        private PlayerControllerBase _controller;
        private PlayerMoveController _moveController;
        private PlayerAnimationController _animationController;

        private bool _isActive;

        private void Awake()
        {
            _inputHandler = GameObject.Find(GameConstants.Object.INPUT).GetComponent<GameSystem.InputHandler>();

            _controller = new PlayerControllerBase();
            _moveController = new PlayerMoveController(GetComponent<Rigidbody>());
            _animationController = new PlayerAnimationController(GetComponent<Animator>());
        }

        private void Start()
        {
            _isActive = true;

            _moveController.Init();

            _controller.ChangeState(PlayerControllerBase.State.STILLNESS);
        }

        private void Update()
        {
            if (!_isActive) { return; }

            CheckKeyState();

            _moveController.Update();
            _animationController.Update();
        }

        private void CheckKeyState()
        {
            if (_inputHandler.IsActionPressing(GameConstants.Input.Action.LEFTMOVE) && transform.position.x > GameConstants.LowerLeft.x)
            {
                _controller.ChangeState(PlayerControllerBase.State.RUN);
                _controller.SetDirState(true);
            }
            else if (_inputHandler.IsActionPressing(GameConstants.Input.Action.RIGHTMOVE) && transform.position.x < GameConstants.TopRight.x)
            {
                _controller.ChangeState(PlayerControllerBase.State.RUN);
                _controller.SetDirState(false);
            }
            else
            {
                _controller.ChangeState(PlayerControllerBase.State.STILLNESS);
            }

            if (_inputHandler.IsActionPressed(GameConstants.Input.Action.JUMP) && _controller.GetIsGranded())
            {
                _controller.ChangeState(PlayerControllerBase.State.JUMP);
            }
        }
    }
}