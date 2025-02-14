using Game.GameSystem;
using Game.StageScene.Magnet;
using UnityEngine;
using static Game.StageScene.Magnet.MagnetData;

namespace Game.StageScene
{
    public class StageCreater : StageFormBase
    {
        private AcquireStageData _acquireStageData;

        // オブジェクト
        [NamedSerializeField(
            new string[]
            {
                "NotFixed_Block",
                "NFixed_Block",
                "SFixed_Block",
                "CanFixed_Block",
                "NotMoving1_Block",
                "NotMoving2_Block",
                "NotMoving3_Block",
                "CanMoving_Block",
                "NMoving_Block",
                "SMoving_Block",
            }
        )]
        [SerializeField]
        private GameObject[] _objects;

        // 特殊オブジェクト
        [NamedSerializeField(
            new string[]
            {
                "Parent_Object",
                "Player_Object",
                "Goal_Object",
                "Button_Object",
                "Gimmick_Block",
                "MovingFloor_Block",
                "CanUp_Block",
            }
        )]
        [SerializeField]
        private GameObject[] _specialObjects;

        // 背景オブジェクト
        [NamedSerializeField(
            new string[]
            {
                "",
                "",
                "",
                "",
            }
        )]
        [SerializeField]
        private GameObject[] _bgObjects;

        // 情報場所管理用
        private int _currentRow = MAX_ROWS - 1;
        private int _currentCol = 0;

        // Data管理用
        private int[,] _colorArray = new int[MAX_ROWS, MAX_COLS];
        private int[,] _powerArray = new int[MAX_ROWS, MAX_COLS];
        private ScaleData[,] _scaleArray = new ScaleData[MAX_ROWS, MAX_COLS];
        private Transform[,] _transforms = new Transform[MAX_ROWS, MAX_COLS];

        private ScaleData _scaleZero = new ScaleData(0, 0);

        // プレイヤーの生存フラグ
        private bool _isPlayerCreate = true;

        // ステージオブジェクト
        private GameObject _stageObject = null;
        // グループオブジェクト
        private GameObject _currentGroupObject = null;

        private void Start()
        {
            _acquireStageData = GetComponent<AcquireStageData>();
        }

        private void StageObjectCreate()
        {
            Vector3 init_position = GameConstants.LowerLeft;

            _stageObject = new GameObject("StageObject");
            _stageObject.transform.position = init_position;
        }

        #region -------- 大きさチェック --------

        private int _currentColor = 0;
        private int _rowConter = 0;
        private int _colConter = 0;

        private void CheckObjectScale()
        {
            for (int i = 0; i < MAX_ROWS; i++)
            {
                for (int j = 0; j < MAX_COLS; j++)
                {
                    _currentColor = _colorArray[i, j];

                    _rowConter = 0;
                    _colConter = 0;

                    // 空白と特殊オブジェクトはスキップ
                    if (_currentColor == (int)ObjectType.NOT_OBJ || _currentColor > (int)SpecialObjectType.GIMMICK_BLOCK) continue;

                    // オブジェクトの大きさが0ならスキップ
                    if (_scaleArray[i, j]._row == _scaleZero._row && _scaleArray[i, j]._col == _scaleZero._col) continue;

                    // 一番右上のブロックはスキップ
                    if (j == MAX_COLS - 1 && i == MAX_ROWS - 1) continue;

                    // 縦横の一致数のチェック
                    bool row_flag = CheckRowPiece(i, j);
                    bool col_flag = CheckColPiece(i, j);

                    // 右と上のブロックが同じブロックではなければスキップ
                    if (!row_flag && !col_flag) continue;

                    while (true)
                    {
                        if (CheckScale(i, j))
                        {
                            SetObjectScale(i, j);
                            break;
                        }
                        else
                        {
                            _rowConter--;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 縦の範囲チェック
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool CheckRowPiece(int row, int col)
        {
            for (int i = row; i < MAX_ROWS; i++)
            {
                if (_currentColor != _colorArray[i, col]) break;

                _rowConter++;
            }

            return _rowConter != 1;
        }

        /// <summary>
        /// 横の範囲チェック
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool CheckColPiece(int row, int col)
        {
            for (int i = col; i < MAX_COLS; i++)
            {
                if (_currentColor != _colorArray[row, i]) break;

                _colConter++;
            }

            return _colConter != 1;
        }

        /// <summary>
        /// 範囲のチェック
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool CheckScale(int row, int col)
        {
            for (int i = row; i < row + _rowConter; i++)
            {
                for (int j = col; j < col + _colConter; j++)
                {
                    if (_currentColor != _colorArray[i, j]) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 大きさの設定
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void SetObjectScale(int row, int col)
        {
            for (int i = row; i < row + _rowConter; i++)
            {
                for (int j = col; j < col + _colConter; j++)
                {
                    _scaleArray[i, j] = _scaleZero;
                }
            }

            _scaleArray[row, col] = new ScaleData(_rowConter, _colConter);
        }

        #endregion


        private GameObject ObjectCreater(int color, int power)
        {
            if (color == (int)ObjectType.NOT_OBJ) return null;

            if (color <= -5 || color > (int)ObjectType.S_MOVING_BLOCK) return null;

            switch (color)
            {
                case (int)ObjectType.N_FIXED_BLOCK:

                    GameObject n_fixed = Instantiate(_objects[color - 1]);
                    PowerSet(n_fixed, power);
                    return n_fixed;

                case (int)ObjectType.S_FIXED_BLOCK:

                    GameObject s_fixed = Instantiate(_objects[color - 1]);
                    PowerSet(s_fixed, power);
                    return s_fixed;

                case (int)SpecialObjectType.PLAYER_OBJ:

                    if (_isPlayerCreate)
                    {
                        int player_value = (int)SpecialObjectType.PLAYER_OBJ * (int)GameConstants.INVERSION;
                        GameObject player = Instantiate(_specialObjects[player_value]);
                        PowerSet(player, power);
                        _isPlayerCreate = false;
                        return player;
                    }

                    return null;

                case (int)SpecialObjectType.GOAL_OBJ:

                    int goal_value = (int)SpecialObjectType.GOAL_OBJ * (int)GameConstants.INVERSION;
                    GameObject goal = Instantiate(_specialObjects[goal_value]);
                    return goal;

                case (int)SpecialObjectType.BUTTON_OBJ:

                    int gimmick_value = (int)SpecialObjectType.BUTTON_OBJ * (int)GameConstants.INVERSION;
                    GameObject gimmick = Instantiate(_specialObjects[gimmick_value]);
                    return gimmick;

                case (int)SpecialObjectType.GIMMICK_BLOCK:

                    int p_gimmick_value = (int)SpecialObjectType.GIMMICK_BLOCK * (int)GameConstants.INVERSION;
                    GameObject p_gimmick = Instantiate(_specialObjects[p_gimmick_value]);
                    return p_gimmick;

                case (int)SpecialObjectType.MOVING_FLOOR:

                    int moving_floor_value = (int)SpecialObjectType.MOVING_FLOOR * (int)GameConstants.INVERSION;
                    GameObject moving_floor = Instantiate(_specialObjects[moving_floor_value]);
                    return moving_floor;

                case (int)SpecialObjectType.CAN_UP:

                    int canup_value = (int)SpecialObjectType.CAN_UP * (int)GameConstants.INVERSION;
                    GameObject canup = Instantiate(_specialObjects[canup_value]);
                    return canup;

                default:

                    GameObject obj = Instantiate(_objects[color - 1]);
                    return obj;
            }
        }

        /// <summary>
        /// 磁力のセット
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="power"></param>
        private void PowerSet(GameObject obj, int power)
        {
            MagnetObjectManager magnet = obj.GetComponent<MagnetObjectManager>();

            if (magnet == null) return;

            magnet.SetObjectPower(power);
        }

        public void Create()
        {
            if (_acquireStageData == null) _acquireStageData = GetComponent<AcquireStageData>();

            _acquireStageData.GetStageData(_colorArray, _powerArray, _scaleArray);

            StageObjectCreate();

            _isPlayerCreate = true;

            CheckObjectScale();

            for (int i = 0; i < MAX_ROWS; i++)
            {
                for (int j = 0; j < MAX_COLS; j++)
                {
                    // オブジェクト生成
                    GameObject obj = ObjectCreater(_colorArray[i, j], _powerArray[i, j]);

                    // nullチェック
                    if (obj == null) continue;

                    if (_colorArray[i, j] == (int)SpecialObjectType.PLAYER_OBJ)
                    {
                        obj.transform.position = new Vector3(2.0f, 0.5f, 0.0f);
                    }
                    else
                    {
                        // 座標設定
                        obj.transform.position = new Vector3(
                            INIT_X * j + ((obj.transform.localScale.x - 1) * 0.5f),
                            INIT_Y * i + ((obj.transform.localScale.y - 1) * 0.5f),
                            INIT_Z
                        );
                    }

                    if (_colorArray[i, j] == (int)SpecialObjectType.PLAYER_OBJ ||
                        _colorArray[i, j] == (int)SpecialObjectType.BUTTON_OBJ ||
                        _colorArray[i, j] == (int)SpecialObjectType.GOAL_OBJ)
                    {
                        obj.transform.SetParent(_stageObject.transform, false);
                        continue;
                    }

                    // 親オブジェクトの作成
                    if (_scaleArray[i, j]._row != _scaleZero._row && _scaleArray[i, j]._col != _scaleZero._col)
                    {
                        _currentGroupObject = Instantiate(
                            _specialObjects[(int)SpecialObjectType.PARENT_OBJ], obj.transform);

                        BoxCollider box_collider = _currentGroupObject.GetComponent<BoxCollider>();
                        box_collider.size = new Vector3(_scaleArray[i, j]._col, _scaleArray[i, j]._row, 1.0f);

                        Vector3 new_group_position = box_collider.center;
                        new_group_position.x += (_scaleArray[i, j]._col - 1) * 0.5f;
                        new_group_position.y += (_scaleArray[i, j]._row - 1) * 0.5f;
                        box_collider.center = new_group_position;

                        _currentGroupObject.transform.SetParent(_stageObject.transform, false);

                        BlockObjectManager block_manager = _currentGroupObject.GetComponent<BlockObjectManager>();
                        block_manager.SetObjectType(_colorArray[i, j], obj.layer, obj.tag);
                    }

                    obj.transform.SetParent(_currentGroupObject.transform, false);
                }
            }
        }

        public void BGCreate()
        {
            GameDataManager data = GameDataManager.Instance;

            int current_index = data.GetCurrentStageIndex();

            Transform transform = GameObject.Find(GameConstants.MAIN_CAMERA).transform;

            Vector3 bg_position = new Vector3(transform.position.x, transform.position.y, 1.0f);

            Instantiate(_bgObjects[current_index], bg_position, Quaternion.identity, transform);
        }
    }
}