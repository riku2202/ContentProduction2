using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Title
{
    /// <summary>
    /// タイトルシーンの管理クラス
    /// </summary>
    public class TitleSceneController : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            DebugManager.LogMessage("テスト");
        }
    }
}