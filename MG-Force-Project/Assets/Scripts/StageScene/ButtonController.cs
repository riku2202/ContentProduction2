using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StageScene
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonUp;
        [SerializeField] private GameObject _buttonDown;
        private bool isUpButton = true;

        private void Update()
        {
            if (isUpButton)
            {
                _buttonDown.SetActive(false);
                _buttonUp.SetActive(true);
            }
            else
            {
                _buttonUp.SetActive(false);
                _buttonDown.SetActive(true);
            }
        }

        private void OnTriggerStay(Collider collider)
        {
            if (collider.CompareTag(GameConstants.Tag.UNTAGGED)) return;

            if (collider.gameObject.CompareTag(GameConstants.Tag.MOVING) ||
                collider.gameObject.CompareTag(GameConstants.Tag.PLAYER))
            {
                isUpButton = false;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.CompareTag(GameConstants.Tag.UNTAGGED)) return;

            if (collider.gameObject.CompareTag(GameConstants.Tag.MOVING) ||
                collider.gameObject.CompareTag(GameConstants.Tag.PLAYER))
            {
                //isUpButton = true;
            }
        }

        public bool GetIsUpButton()
        {
            return isUpButton;
        }
    }
}