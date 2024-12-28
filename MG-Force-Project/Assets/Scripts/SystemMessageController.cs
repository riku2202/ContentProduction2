using UnityEngine;

namespace Game 
{
    public class SystemMessageController : MonoBehaviour 
    {
        private const float FINISH_POS_X = -1920.0f;

        private float _speed = 4.0f;

        private Vector3 _initPos = new Vector3(1920.0f, 990.0f, 0.0f);

        private Vector3 _currentPos = Vector3.zero;

        private void OnEnable()
        {
            transform.position = _initPos;
        }

        private void Update()
        {
            _currentPos = transform.position;

            if (transform.position.x > FINISH_POS_X)
            {
                _currentPos.x -= _speed;
            }

            transform.position = _currentPos;

        }
    }
}