using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Stage.Player 
{
    /// <summary>
    /// プレイヤーの動作管理クラス(アニメーションやそのほかの処理は別の場所で行う)
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