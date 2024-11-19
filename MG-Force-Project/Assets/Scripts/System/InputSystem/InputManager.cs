using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static InputManager Instance { get; private set; }

    // InputAction�A�Z�b�g
    [SerializeField] private InputActionAsset inputActions;

    // �A�N�V������Ԃ̎���
    private Dictionary<string, bool> actionStates = new Dictionary<string, bool>();

    // Unity�W���̓��͏��
    private Dictionary<string, KeyCode[]> unityInputBindings = new Dictionary<string, KeyCode[]>();

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

        // Input System�̏�����
        InitializeInputSystem();
        InitializeUnityInputBindings();
    }

    private void InitializeInputSystem()
    {
        foreach (var map in inputActions.actionMaps)
        {
            foreach (var action in map.actions)
            {
                actionStates[action.name] = false;
                action.performed += ctx => actionStates[action.name] = true;
                action.canceled += ctx => actionStates[action.name] = false;
                action.Enable();
            }
        }
    }

    private void InitializeUnityInputBindings()
    {
        unityInputBindings["Jump"] = new[] { KeyCode.Space, KeyCode.JoystickButton0 }; // X�{�^�� or �X�y�[�X�L�[
        unityInputBindings["Attack"] = new[] { KeyCode.E, KeyCode.JoystickButton1 };   // E�L�[ or �Q�[���p�b�hB�{�^��
    }

    /// <summary>
    /// Input System�o�R�ŃA�N�V������������Ă��邩���擾
    /// </summary>
    public bool IsActionPressed(string actionName)
    {
        return actionStates.ContainsKey(actionName) && actionStates[actionName];
    }

    /// <summary>
    /// Unity�W����InputManager�o�R�ŃA�N�V������������Ă��邩���擾
    /// </summary>
    public bool IsUnityInputPressed(string actionName)
    {
        if (unityInputBindings.TryGetValue(actionName, out var keys))
        {
            foreach (var key in keys)
            {
                if (Input.GetKey(key)) return true;
            }
        }
        return false;
    }
}
