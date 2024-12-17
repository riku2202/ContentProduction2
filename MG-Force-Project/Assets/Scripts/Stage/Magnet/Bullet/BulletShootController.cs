using UnityEditor;
using UnityEngine;

namespace Game.Stage.Magnet
{
    public class BulletShootController : MonoBehaviour 
    {
        private MagnetManager magnet;

        [SerializeField]
        private GameObject bulletPrefab;

        private void Start()
        {
            magnet = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();
        }

        private void Update()
        {
            if (magnet.IsMagnetBoot) return;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameObject gb = Instantiate(bulletPrefab);
                Vector3 init_position = gameObject.transform.position;
                init_position.y += 1;

                gb.transform.position = init_position;
            }
        }
    }
}