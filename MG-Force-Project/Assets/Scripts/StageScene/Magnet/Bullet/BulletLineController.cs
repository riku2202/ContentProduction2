
using Game.GameSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.StageScene.Magnet
{
    public class BulletLineController : MonoBehaviour
    {
        private InputHandler _inputHandler;

        private Transform _player;
        
        private LineRenderer _lineRenderer;

        private Vector3 _currentDirection = Vector3.zero;

        private void Start()
        {
            _inputHandler = InputHandler.Instance;

            _player = GameObject.Find(GameConstants.PLAYER_OBJ).GetComponent<Transform>();

            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            float maxDistance = 10f;

            _currentDirection = GetDirection();

            if (Physics.Raycast(_player.position, _currentDirection, out RaycastHit hit, maxDistance, (int)GameConstants.Layer.DEFAULT))
            {
                if (hit.collider.CompareTag(GameConstants.Tag.FIXED.ToString()) || 
                    hit.collider.CompareTag(GameConstants.Tag.MOVING.ToString()))
                {
                    _lineRenderer.SetPosition(0, _player.position);
                    _lineRenderer.SetPosition(1, hit.point);
                }
            }
            else
            {
                _lineRenderer.SetPosition(0, _player.position);
                _lineRenderer.SetPosition(1, _player.position + _currentDirection * maxDistance);
            }
        }

        private Vector3 GetDirection()
        {
            if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.North))
            {
                return InputConstants.ActionVector.North;
            }
            else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.NorthEast))
            {
                return InputConstants.ActionVector.NorthEast;
            }
            else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.East))
            {
                return InputConstants.ActionVector.East;
            }
            else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.SouthEast))
            {
                return InputConstants.ActionVector.SouthEast;
            }
            else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.NorthWest))
            {
                return InputConstants.ActionVector.NorthWest;
            }
            else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.West))
            {
                return InputConstants.ActionVector.West;
            }
            else if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT_ANGLE, InputConstants.ActionVector.SouthWest))
            {
                return InputConstants.ActionVector.SouthWest;
            }

            Debug.Log("Test");

            return _currentDirection;
        }
    }
}