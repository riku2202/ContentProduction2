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
        private float Speed = 10.0f;

        [SerializeField]
        private bool IsActive;

        //private InputManager Input;

        private void Start()
        {
            IsActive = true;
            //Input = GameObject.Find("InputManager").GetComponent<InputManager>();
        }

        private void FixedUpdate()
        {
            if (!IsActive) { return; }

            //StageLoader stage_loader = GameObject.Find("StageLoader").GetComponent<StageLoader>();

            GameDataManager gamedata_manager = GameDataManager.Instance;

            //Vector3 top_right = stage_loader.GetStageData(gamedata_manager.GetCurrentStageIndex()).TopRight;

            Vector3 top_right = new Vector3(100.0f, 100.0f, 100.0f);

            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && transform.position.x <= top_right.x + GameConstants.TopRigid_Puls.x)
            {
                transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                transform.Translate(0, 0, Speed * Time.deltaTime);
            }

            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && transform.position.x >= GameConstants.LowerLeft.x)
            {
                transform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
                transform.Translate(0, 0, Speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) && transform.position.y <= top_right.y + GameConstants.TopRigid_Puls.y)
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