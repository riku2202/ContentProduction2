using Game.GameSystem;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game
{
    public class TestController : MonoBehaviour
    {
        private float speed = 0.01f;

        private void Update()
        {
            Vector3 vec = transform.position;

            vec.x += speed;

            transform.position = vec;
        }
    }
}