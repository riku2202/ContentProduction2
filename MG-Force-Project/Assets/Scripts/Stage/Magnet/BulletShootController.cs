using UnityEditor;
using UnityEngine;

namespace Game.Stage.Magnet
{
    public class BulletShootController : MonoBehaviour 
    {
        //private InputManager Input;

        private MagnetManager Magnet;

        [SerializeField]
        private GameObject Bullet;

        private void Start()
        {
            //Input = GameObject.Find("InputManager").GetComponent<InputManager>();

            Magnet = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();
        }

        private void Update()
        {
            if (Magnet.IsMagnetBoot) { return; }

            GameObject bullet = GameObject.Find("BulletPrefab(Clone)");

            if (Input.GetKeyDown(KeyCode.Return) && bullet == null)
            {
                GameObject gb = Instantiate(Bullet);
                Vector3 init_position = gameObject.transform.position;
                init_position.y += 1;

                gb.transform.position = init_position;
            }
        }
    }
}