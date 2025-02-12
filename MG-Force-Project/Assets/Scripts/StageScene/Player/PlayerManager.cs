using System.Collections.Generic;
using UnityEngine;

namespace Game.StageScene.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerControllerBase _playerControllerBase;

        private List<PlayerControllerBase> playerControllers = new List<PlayerControllerBase>();

        private bool _isActive;

        private Vector3 _currentPos;

        private void Start()
        {
            _isActive = true;
            
            _playerControllerBase = new PlayerControllerBase(gameObject);

            playerControllers.Add(new PlayerStateController());
            playerControllers.Add(new PlayerMoveController());
            playerControllers.Add(new PlayerAnimationController());

            foreach (var controller in playerControllers)
            {
                controller.OnStart();
            }
        }

        private void Update()
        {
            if (!_isActive) { return; }

            foreach (var controller in playerControllers)
            {
                controller.OnUpdate();
            }

            PosAdjustment();
        }

        private void PosAdjustment()
        {
            _currentPos = transform.position;

            _currentPos.x = RoundToPrecision(_currentPos.x, 3);
            _currentPos.y = RoundToPrecision(_currentPos.y, 3);
            _currentPos.z = RoundToPrecision(_currentPos.z, 3);

            transform.position = _currentPos;
        }

        private float RoundToPrecision(float value, int precision)
        {
            float factor = Mathf.Pow(10, precision);
            return Mathf.Round(value * factor) / factor;
        }
    }
}