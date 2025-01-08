using UnityEngine;

namespace Game.StageScene.Camera
{

    public class CameraFreeViewMoveController : MonoBehaviour
    {
        /*
         *　カメラを複数用意するとaudioだったりで処理がよくないので 
         *  一つのカメラに二つのモードを管理させるようにする
         *  ・両方管理クラスと個別管理クラス*2の合計3つ
         *  ・ステージ選択画面でも使用できるような仕様にする
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