using Game.GameSystem;
using TMPro;
using UnityEngine;

namespace Game.StageScene.Camera
{

    public class FreeViewCameraController : MonoBehaviour
    {
        [SerializeField] private int speed;

        private InputHandler _input;

        private Vector3 initPos;

        private void Start()
        {
            _input = InputHandler.Instance;
        }

        private void OnEnable()
        {
            initPos = transform.position;
        }

        private void OnDisable()
        {
            transform.position = initPos;
        }

        private void Update()
        {
            Vector3 position = transform.position;

            if (_input.IsActionPressing(InputConstants.Action.VIEW_MOVE_LEFT))
            {
                position.x -= speed * Time.deltaTime;
            }

            if (_input.IsActionPressing(InputConstants.Action.VIEW_MOVE_RIGHT))
            {
                position.x += speed * Time.deltaTime;
            }

            if (_input.IsActionPressing(InputConstants.Action.VIEW_MOVE_UP))
            {
                position.y += speed * Time.deltaTime;
            }

            if (_input.IsActionPressing(InputConstants.Action.VIEW_MOVE_DOWN))
            {
                position.y -= speed * Time.deltaTime;
            }

            position.z = -10;

            transform.position = position;
        }
    }
}