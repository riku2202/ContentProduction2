using Game.GameSystem;
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
        private float Speed = 5.0f;

        [SerializeField]
        private bool isActive;

        private void Start()
        {
            isActive = true;
        }

        private void FixedUpdate()
        {
            if (!isActive) return;

            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && transform.position.x <= GameConstants.TopRight.x)
            {
                transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                transform.Translate(0, 0, Speed * Time.deltaTime);
            }

            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && transform.position.x >= GameConstants.LowerLeft.x)
            {
                transform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
                transform.Translate(0, 0, Speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) && transform.position.y <= GameConstants.TopRight.y)
            {
                transform.Translate(0, Speed * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && transform.position.y >= GameConstants.LowerLeft.y)
            {
                transform.Translate(0, -Speed * Time.deltaTime, 0);
            }
        }
    }
}