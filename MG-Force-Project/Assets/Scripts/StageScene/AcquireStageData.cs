using Game.GameSystem;
using Newtonsoft.Json;
using System;

namespace Game.StageScene
{
    public class AcquireStageData : StageFormBase
    {
        // ゲームデータ管理クラス
        private GameDataManager _data = GameDataManager.Instance;

        private MessageWindow _messageWindow;
        
        private string GetJSONData()
        {
            int current_stage_index = _data.GetCurrentStageIndex();

            string json = StageDataLoader.CellStageData(current_stage_index);

            if (string.IsNullOrEmpty(json))
            {
                _messageWindow.ErrorWindow(ErrorCode.E5001);
                return null;
            }

            return json;
        }

        public void GetStageData(int[,] color, int[,] power, ScaleData[,] scale)
        {
            _messageWindow = MessageWindow.GetInstance();

            string json = GetJSONData();

            RootObject root_object = JsonConvert.DeserializeObject<RootObject>(json);

            int row = MAX_ROWS - 1;
            int col = 0;

            foreach (var itemWrapper in root_object.items)
            {
                // nullチェック
                if (string.IsNullOrEmpty(itemWrapper.key))
                {
                    continue;
                }

                try
                {
                    // 配列にデータを格納
                    color[row, col] = itemWrapper.value.color;
                    power[row, col] = itemWrapper.value.power;
                    scale[row, col] = new ScaleData(1, 1);

                    // 次のインデックスへの移動
                    col++;

                    if (col >= MAX_COLS)
                    {
                        col = 0;
                        row--;
                    }
                }
                catch (Exception ex)
                {
                    _messageWindow.ErrorWindow(ErrorCode.E5001);

                    DebugManager.LogMessage($"例外が発生しました {itemWrapper.key}: {ex.Message}", DebugManager.MessageType.Error);
                }
            }
        }
    }
}