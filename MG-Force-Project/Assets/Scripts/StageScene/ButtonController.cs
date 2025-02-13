using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StageScene
{
    /*
    �@�@�y��邱�ƃ��X�g�z�{�^���̏���
    
    �@�@�E�{�^���ɂ��ON OFF�̃f�[�^�Ǘ�
    �@�@�E�{�^���ɂ��ON OFF�̃I�u�W�F�N�g�ω�
          �E��ɓ����u���b�N�������́A�v���C���[��������牟��
    �@�@�@�E���ꂽ��߂邩�A���̂܂܂��؂�ւ��邽�߂̏��������ǉ�
    �@�@�E�����O���[�v�̋��ʉ�
    */

    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonUp;
        [SerializeField] private GameObject _buttonDown;

        private bool isDownButton = false;

        private void Update()
        {
            if (isDownButton)
            {
                Debug.Log("test");
            }
            else
            {

            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag(GameConstants.Tag.MOVING) ||
                collision.gameObject.CompareTag(GameConstants.Tag.PLAYER))
            {
                isDownButton = true;
            }
        }
    }
}