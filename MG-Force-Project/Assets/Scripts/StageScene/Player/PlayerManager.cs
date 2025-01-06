using System.Collections.Generic;
using UnityEngine;

namespace Game.StageScene.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerControllerBase _playerControllerBase;

        private List<PlayerControllerBase> playerControllers = new List<PlayerControllerBase>();

        private bool _isActive;

        private void Start()
        {
            _isActive = true;
            
            _playerControllerBase = new PlayerControllerBase(gameObject);

            playerControllers.Add(new PlayerStateController());
            playerControllers.Add(new PlayerMoveController());
            playerControllers.Add(new PlayerAnimationController());

            foreach (var controller in playerControllers)
            {
                controller.Init();
            }
        }

        private void Update()
        {
            if (!_isActive) { return; }

            foreach (var controller in playerControllers)
            {
                controller.Update();
            }
        }
    }
}