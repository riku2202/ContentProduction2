using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Game.GameSystem;

namespace Game.StageSelect
{
    /// <summary>
    /// �X�e�[�W�I���V�[���̊Ǘ��N���X
    /// </summary>
    public class StageSelectSceneController : MonoBehaviour
    {
        [SerializeField]
        private StageData SelectStage;

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("StageSelect");
            }
        }
    }
}