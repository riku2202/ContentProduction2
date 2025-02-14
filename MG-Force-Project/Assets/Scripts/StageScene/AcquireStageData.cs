using Game.GameSystem;
using Newtonsoft.Json;
using System;

namespace Game.StageScene
{
    public class AcquireStageData : StageFormBase
    {
        // �Q�[���f�[�^�Ǘ��N���X
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
                // null�`�F�b�N
                if (string.IsNullOrEmpty(itemWrapper.key))
                {
                    continue;
                }

                try
                {
                    // �z��Ƀf�[�^���i�[
                    color[row, col] = itemWrapper.value.color;
                    power[row, col] = itemWrapper.value.power;
                    scale[row, col] = new ScaleData(1, 1);

                    // ���̃C���f�b�N�X�ւ̈ړ�
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

                    DebugManager.LogMessage($"��O���������܂��� {itemWrapper.key}: {ex.Message}", DebugManager.MessageType.Error);
                }
            }
        }
    }
}