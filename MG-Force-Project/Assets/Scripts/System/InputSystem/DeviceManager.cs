using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameSystem
{
    public class DeviceManager : MonoBehaviour
    {
        #region -------- �V���O���g���̐ݒ� --------

        // �V���O���g���C���X�^���X
        public static DeviceManager Instance { get; private set; }

        private void Awake()
        {
            // �V���O���g���̐ݒ�
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        // ���b�Z�[�W�p�萔
        private const string GAMEPAD_IN_MESSAGE = "�R���g���[���[���ڑ�����܂���";
        private const string GAMEPAD_OUT_MESSAGE = "�R���g���[���[���ؒf����܂���";

        // �V�X�e�����b�Z�[�W�\���p�N���X
        private  SystemMessageManager _systemMessage;

        // �Q�[���p�b�h���ǂ���
        public bool isGamepad { get; private set; }

        /// <summary>
        /// �C�x���g�̒ǉ�
        /// </summary>
        private void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        /// <summary>
        /// �C�x���g�̍폜
        /// </summary>
        private  void OnDisable()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;
        }

        /// <summary>
        /// ���݂̃f�o�C�X�ڑ��`�F�b�N
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