using UnityEngine;

namespace Game
{
    public class GameFlagManager : MonoBehaviour
    {
        #region -------- シングルトンの設定 --------

        public static GameFlagManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        #endregion

        public enum ErrorFlag
        {
            INPUT,
            LOAD,
            EVENT,
            SCENE,
            PLAYER,
            MAGNET,
            DEBUG,

            MAX,
        }

        [NamedSerializeField(
            new string[]
            {
                "TitleScene",
                "StageSelectScene",
                "StageScene",
                "ClearScene",
            }
        )]
        [SerializeField]
        private bool[] CheckSceneFlag = new bool[(int)GameConstants.Scene.Max];

        [NamedSerializeField(
            new string[]
            {
                "Input",
                "Load",
                "Event",
                "Scene",
                "Player",
                "Magnet",
                "Debug",
            }
        )]
        [SerializeField]
        private bool[] CheckErrorFlag = new bool[(int)ErrorFlag.MAX];

        private void Update()
        {
//#if UNITY_EDITOR

//                // エディターの終了
//                UnityEditor.EditorApplication.isPlaying = false;

//#else // 実際のビルド版実行時
      
//                // アプリケーションの終了
//                Application.Quit();

//#endif
        }

        public void SetFlag(GameConstants.Scene scene, bool truth)
        {
            CheckSceneFlag[(int)scene] = truth;
        }

        public void SetFlag(ErrorFlag error, bool truth)
        {
            CheckErrorFlag[(int)error] = truth;
        }

        public bool GetFlag(GameConstants.Scene scene)
        {
            return CheckSceneFlag[(int)scene];
        }

        public bool GetFlag(ErrorFlag error)
        {
            return CheckErrorFlag[(int)error];
        }
    }
}