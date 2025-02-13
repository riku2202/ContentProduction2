using UnityEngine;

namespace Game.StageScene.Camera
{

    public class CameraFreeViewMoveController : MonoBehaviour
    {
        /*
        　　【やることリスト】カメラ
        　　・ステージのカメラは一旦置いておくとする
        　　・ビューモードの処理を作成
        　　・ステージシーンのカメラの実装を返る
        */

        [SerializeField]
        private int speed;

        private Vector3 initPos;

        private void OnEnable()
        {
            initPos = GameObject.Find(GameConstants.MAIN_CAMERA).GetComponent<Transform>().position;

            transform.position = initPos;
        }

        private void Update()
        {
            Vector3 position = transform.position;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                position.x -= speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                position.x += speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                position.y += speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                position.y -= speed * Time.deltaTime;
            }

            position.z = -10;

            transform.position = position;
        }
    }
}