
using Game.GameSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.StageScene.Magnet
{
    public class BulletLineController : MonoBehaviour
    {
        private static InputHandler _inputHandler;

        private Transform _playerTransform;
        
        private LineRenderer _lineRenderer;

        private static Vector3 _currentDirection = Vector3.zero;

        private void Start()
        {
            _inputHandler = InputHandler.Instance;

            _playerTransform = GameObject.Find(GameConstants.PLAYER_OBJ).GetComponent<Transform>();

            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            Vector3 start_point = _playerTransform.position;
            start_point.y += 1.0f;

            float maxDistance = 10f;

            _currentDirection = GetDirection();

            if (Physics.Raycast(start_point, _currentDirection, out RaycastHit hit, maxDistance))
            {
                if (hit.collider.isTrigger == false)
                {
                    _lineRenderer.SetPosition(0, start_point);
                    _lineRenderer.SetPosition(1, hit.point);
                }
                else
                {
                    _lineRenderer.SetPosition(0, start_point);
                    _lineRenderer.SetPosition(1, start_point + _currentDirection * maxDistance);
                }
            }
            else
            {
                _lineRenderer.SetPosition(0, start_point);
                _lineRenderer.SetPosition(1, start_point + _currentDirection * maxDistance);
            }
        }

        public static Vector3 GetDirection()
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

            return _currentDirection;
        }
    }
}