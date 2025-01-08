using UnityEngine;

namespace Game.StageScene.Camera
{

    public class CameraFreeViewMoveController : MonoBehaviour
    {
        /*
         *�@�J�����𕡐��p�ӂ����audio��������ŏ������悭�Ȃ��̂� 
         *  ��̃J�����ɓ�̃��[�h���Ǘ�������悤�ɂ���
         *  �E�����Ǘ��N���X�ƌʊǗ��N���X*2�̍��v3��
         *  �E�X�e�[�W�I����ʂł��g�p�ł���悤�Ȏd�l�ɂ���
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