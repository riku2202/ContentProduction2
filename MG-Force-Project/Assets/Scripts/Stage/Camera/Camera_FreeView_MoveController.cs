using UnityEditor.Rendering;
using UnityEngine;

namespace Stage 
{

    public class Camera_FreeView_MoveController : MonoBehaviour
    {
        [SerializeField]
        private int Speed;

        private void OnEnable()
        {
            transform.position = GameObject.Find("playerPrefab").GetComponent<Transform>().position;
        }

        private void Update()
        {
            Vector3 position = transform.position;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                position.x -= Speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                position.x += Speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                position.y += Speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                position.y -= Speed * Time.deltaTime;
            }

            position.z = -10;

            transform.position = position;
        }
    }
}