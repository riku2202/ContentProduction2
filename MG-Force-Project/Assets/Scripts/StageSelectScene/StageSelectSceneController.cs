using UnityEngine;
using Game.GameSystem;

namespace Game.StageScene
{
    /// <summary>
    /// ステージ選択シーンの管理クラス
    /// </summary>
    public class StageSelectSceneController : MonoBehaviour
    {
        private InputHandler _inputHandler;
        private SceneLoader _sceneLoader;

        [SerializeField] private GameObject[] _labObject;

        [SerializeField] private GameObject _menuObject;
        [SerializeField] private GameObject _stageSelectObject;

        public bool isMenuScreen { get; private set; }

        public bool isStageSelectScreen { get; private set; }

        private CrystalController _crystalController;

        private void Awake()
        {
            _inputHandler = InputHandler.Instance;
            _sceneLoader = SceneLoader.Instance;

            SetActive(true, false, false);

            _crystalController = GameObject.Find("Crystal_Model_Prefab(Clone)").GetComponent<CrystalController>();
        }

        private void Update()
        {
            if (isStageSelectScreen)
            {
                StageSelectScreenUpdate();
            }
            else
            {
                ShortCutCheck();

                if (isMenuScreen)
                {
                    MenuScreenUpdate();
                }
            }

            if (_crystalController.IsGoalEvent)
            {
                _sceneLoader.LoadScene(GameConstants.Scene.Clear.ToString());
            }
        }

        private void StageSelectScreenUpdate()
        {
            if (!_stageSelectObject.activeSelf) { _stageSelectObject.SetActive(true); }

            if (_inputHandler.IsActionPressed(InputConstants.Action.MENU_BACK))
            {
                SetActive(true, false, false);
            }
        }

        private void MenuScreenUpdate()
        {
            if (!_menuObject.activeSelf) { _menuObject.SetActive(true); }
        }

        private void ShortCutCheck()
        {
            if (_inputHandler.IsActionPressed(InputConstants.Action.SHORTCUT_1))
            {
                _sceneLoader.LoadScene(GameConstants.Scene.Title.ToString());
            }
            else if (_inputHandler.IsActionPressed(InputConstants.Action.SHORTCUT_2))
            {
                _sceneLoader.LoadScene(GameConstants.Scene.StageSelect.ToString());
            }
            else if (_inputHandler.IsActionPressed(InputConstants.Action.SHORTCUT_3))
            {
                SetActive(false, false, true);
            }
            else if (_inputHandler.IsActionPressed(InputConstants.Action.SHORTCUT_4))
            {
                isMenuScreen = !isMenuScreen;
            }
        }

        private void SetActive(bool lab, bool menu, bool stage_select)
        {
            for (int i = 0; i < _labObject.Length; i++)
            {
                _labObject[i].SetActive(lab);
            }

            _menuObject.SetActive(menu);
            _stageSelectObject.SetActive(stage_select);
        }
    }
}