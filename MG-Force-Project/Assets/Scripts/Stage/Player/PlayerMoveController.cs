using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage.Player 
{
    /// <summary>
    /// �v���C���[�̓���Ǘ��N���X(�A�j���[�V�����₻�̂ق��̏����͕ʂ̏ꏊ�ōs��)
    /// </summary>
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField]
        private float Speed;

        [SerializeField]
        private bool IsActive;

        private InputManager Input;

        private void Start()
        {
            Input = GameObject.Find("InputManager").GetComponent<InputManager>();
        }

        private void Update()
        {
            if (!IsActive) { return; }

            if (Input.IsActionPressed("Action"))
            {

            }
        }
    }
}