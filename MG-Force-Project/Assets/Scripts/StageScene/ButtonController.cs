using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StageScene
{
    /*
    　　【やることリスト】ボタンの処理
    
    　　・ボタンによるON OFFのデータ管理
    　　・ボタンによるON OFFのオブジェクト変化
          ・上に動くブロックもしくは、プレイヤーが乗ったら押す
    　　　・離れたら戻るか、そのままか切り替えるための条件分岐を追加
    　　・同じグループの共通化
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