using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameSystem
{
    public class DeviceManager : MonoBehaviour
    {
        #region -------- シングルトンの設定 --------

        // シングルトンインスタンス
        public static DeviceManager Instance { get; private set; }

        private void Awake()
        {
            // シングルトンの設定
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        // メッセージ用定数
        private const string GAMEPAD_IN_MESSAGE = "コントローラーが接続されました";
        private const string GAMEPAD_OUT_MESSAGE = "コントローラーが切断されました";

        // システムメッセージ表示用クラス
        private  SystemMessageManager _systemMessage;

        // ゲームパッドかどうか
        public bool isGamepad { get; private set; }

        /// <summary>
        /// イベントの追加
        /// </summary>
        private void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        /// <summary>
        /// イベントの削除
        /// </summary>
        private  void OnDisable()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;
        }

        /// <summary>
        /// 現在のデバイス接続チェック
        /// </summary>
        /// <param name="device"></param>
        /// <param name="change"></param>
        private  void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            _systemMessage = GameObject.Find(GameConstants.Object.SYSTEM_MESSAGE).GetComponent<SystemMessageManager>();

            if (change == InputDeviceChange.Added)
            {
                isGamepad = true;

                _systemMessage.DrawMessage($"{GAMEPAD_IN_MESSAGE}");
            }
            else if (change == InputDeviceChange.Removed)
            {
                isGamepad = false;

                _systemMessage.DrawMessage($"{GAMEPAD_OUT_MESSAGE}");
            }
        }

        private void Start()
        {
            foreach (var device in InputSystem.devices)
            {
                if (device is Gamepad)
                {
                    isGamepad = true;
                    _systemMessage = GameObject.Find(GameConstants.Object.SYSTEM_MESSAGE).GetComponent<SystemMessageManager>();
                    _systemMessage.DrawMessage($"{GAMEPAD_IN_MESSAGE}");
                    break;
                }
            }
        }
    }
}