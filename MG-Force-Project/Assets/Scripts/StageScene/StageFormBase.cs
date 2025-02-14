using Newtonsoft.Json;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.StageScene
{
    public class StageFormBase : MonoBehaviour
    {
        protected enum ObjectType
        {
            NOT_OBJ,
            NOT_FIXED_BLOCK,
            N_FIXED_BLOCK,
            S_FIXED_BLOCK,
            CAN_FIXED_BLOCK,
            NOT_MOVING_1_BLOCK,
            NOT_MOVING_2_BLOCK,
            NOT_MOVING_3_BLOCK,
            CAN_MOVING_BLOCK,
            N_MOVING_BLOCK,
            S_MOVING_BLOCK,
        }

        protected enum SpecialObjectType
        {
            PARENT_OBJ,
            PLAYER_OBJ = -1,
            GOAL_OBJ = -2,
            BUTTON_OBJ = -3,
            GIMMICK_BLOCK = -4,
            MOVING_FLOOR = -5,
            CAN_UP = -6,
        }

        protected const int MAX_ROWS = 25;
        protected const int MAX_COLS = 38;

        // 生成時の初期値
        protected const float INIT_X = 1.0f;
        protected const float INIT_Y = 1.0f;
        protected const float INIT_Z = 0.0f;

        public struct ScaleData
        {
            public int _row;
            public int _col;

            public ScaleData(int row, int col)
            {
                _row = row;
                _col = col;
            }
        }

        [System.Serializable]
        protected class Item
        {
            public int color;  // オブジェクトの判別
            public int power;  // 磁力の強さ
        }

        [System.Serializable]
        protected class ItemWrapper
        {
            public string key;
            public Item value;
        }

        [System.Serializable]
        protected class RootObject
        {
            [JsonProperty("items")]
            public List<ItemWrapper> items;
        }
    }
}