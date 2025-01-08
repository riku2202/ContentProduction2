using UnityEngine;
using Game.GameSystem;

namespace Game.StageScene 
{
    public class StageSelectButtonManager : MonoBehaviour
    {
        private GameDataManager _gameDataManager;
        private SceneLoader _sceneLoader;

        private void Start()
        {
            _gameDataManager = GameDataManager.Instance;
            _sceneLoader = SceneLoader.Instance;
        }

        public void StageSelect(int stage_index)
        {
            if (_gameDataManager != null)
            {
                _gameDataManager.SetCurrentStageIndex(stage_index);

                _sceneLoader.LoadScene(GameConstants.Scene.Stage.ToString());
            }
            else
            {
                DebugManager.LogMessage("GameDataManager‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ", DebugManager.MessageType.Error);
            }
        }
    }
}