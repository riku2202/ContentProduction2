using UnityEngine;
using Game.GameSystem;

namespace Game.Title
{
    /// <summary>
    /// タイトルシーンの管理クラス
    /// </summary>
    public class TitleSceneController : MonoBehaviour
    {
        // ゲームデータ管理クラスの呼び出し
        private GameDataManager _manager = GameDataManager.Instance;

        // 入力管理クラスの呼び出し
        private InputHandler _input;

        private DeviceManager _deviceManager = null;

        private SceneLoader _sceneLoader = SceneLoader.Instance;

        // ロード管理フラグ
        private static bool isLoadGameData = false;

        // タイトルシーンのステップ
        private enum TitleStep
        {
            TITLE,
            GAME_MENU,
            START_MENU,
            CONFIG_MENU,
            MAX_STEP,
        }

        // 現在のメニュー
        private TitleStep _currentStep;

        [SerializeField]
        private GameObject[] _menuObjects = new GameObject[(int)TitleStep.MAX_STEP];

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            // 外部データの読み込み
            StageDataLoader.LoadStageData();

            // ゲームデータの生成
            _manager.NewGameData();

            // 実行して一度のみロードする
            if (!isLoadGameData)
            {
                // ゲームデータのロード
                SaveSystem.LoadManager();

                isLoadGameData = true;
            }

            // 入力管理クラスの呼び出し
            _input = GameObject.Find(GameConstants.Object.INPUT).GetComponent<InputHandler>();

            _deviceManager = GameObject.Find(GameConstants.Object.DEVICE_MANAGER).GetComponent<DeviceManager>();

            _sceneLoader = SceneLoader.Instance;

            // ステップを初期化
            _currentStep = TitleStep.TITLE;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            if (_input.IsActionPressed(GameConstants.Input.Action.MENU_DECISION))
            {
                _sceneLoader.LoadScene(GameConstants.Scene.StageSelect.ToString());
            }

            switch(_currentStep)
            {
                case TitleStep.TITLE:
                    TitleUpdate();
                    break;

                case TitleStep.GAME_MENU:
                    GameMenuUpdate();
                    break;

                case TitleStep.START_MENU:
                    StartMenuUpdate(); 
                    break;

                case TitleStep.CONFIG_MENU:
                    ConfigMenuUpdate(); 
                    break;
            }
        }

        private void TitleUpdate()
        {

        }

        private void GameMenuUpdate()
        {

        }

        private void StartMenuUpdate()
        {

        }

        private void ConfigMenuUpdate()
        {

        }

        /// <summary>
        /// ゲームデータの削除
        /// </summary>
        public void GameDataErase()
        {
            // ゲームデータのリセット
            _manager.ReSetGameData();

#if UNITY_EDITOR // UnityEditorでの実行時(デバック用)

            // ゲームデータのセーブ(デバック時はデータを上書きする)
            SaveSystem.SaveManager();

#else // 実際のビルド版実行時

            // ゲームデータの削除
            SaveSystem.DeleteGameData();

#endif
        }
    }
}