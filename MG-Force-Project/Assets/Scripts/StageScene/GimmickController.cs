using Game.StageScene;
using UnityEngine;

public class GimmickController : MonoBehaviour
{
    private ButtonController _button;

    [SerializeField] private GameObject _fixedBox;

    private void Start()
    {
        _button = GameObject.Find("Button(Clone)").GetComponent<ButtonController>();
    }

    private void Update()
    {
        if (_button != null)
        {
            _fixedBox.SetActive(_button.GetIsUpButton());
        }
        else
        {
            _button = GameObject.Find("Button(Clone)").GetComponent<ButtonController>();
        }
    }
}