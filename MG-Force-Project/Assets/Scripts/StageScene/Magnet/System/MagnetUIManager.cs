using UnityEngine;

namespace Game.StageScene.Magnet
{
    /// <summary>
    /// マグネット関連のUI管理クラス
    /// </summary>
    public class MagnetUIManager : MonoBehaviour
    {
        private MagnetManager Magnet;

        private enum UI
        {
            EnergyGage,
            Boot_ON,
            Boot_OFF,
            N_Magnet,
            S_Magnet,

            MAX,
        }

        [NamedSerializeField(
            new string[] 
            {
                "EnergyGage",
                "Boot_ON",
                "Boot_OFF",
                "N_Magnet",
                "S_Magnet",
            }
        )]
        [SerializeField]
        private GameObject[] _uiObjects = new GameObject[(int)UI.MAX];

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            Magnet = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();
        }

        /// <summary>
        /// 磁力のタイプ変更(Button)
        /// </summary>
        public void OnButton_ChangeMagnetType()
        {
            Magnet.ChangeMagnetType();

            DebugManager.LogMessage(Magnet.CurrentType.ToString(), DebugManager.MessageType.Normal);
        }

        /// <summary>
        /// 磁力の実行変更(Button)
        /// </summary>
        public void OnButton_ChangeMagnetBoot()
        {
            Magnet.ChangeMagnetBoot();

            DebugManager.LogMessage(Magnet.IsMagnetBoot.ToString(), DebugManager.MessageType.Normal);
        }

        /// <summary>
        /// ボタンの判定(デバック用)
        /// </summary>
        public void OnButtonClick()
        {
            DebugManager.LogMessage("pushButton!", DebugManager.MessageType.Normal);
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update()
        {
            if (Magnet.CurrentType == GameConstants.Layer.N_MAGNET)
            {
                _uiObjects[(int)UI.N_Magnet].SetActive(true);
                _uiObjects[(int)UI.S_Magnet].SetActive(false);
            }
            else if (Magnet.CurrentType == GameConstants.Layer.S_MAGNET)
            {
                _uiObjects[(int)UI.S_Magnet].SetActive(true);
                _uiObjects[(int)UI.N_Magnet].SetActive(false);
            }

            if (Magnet.IsMagnetBoot)
            {
                _uiObjects[(int)UI.Boot_ON].SetActive(true);
                _uiObjects[(int)UI.Boot_OFF].SetActive(false);
            }
            else
            {
                _uiObjects[(int)UI.Boot_OFF].SetActive(true);
                _uiObjects[(int)UI.Boot_ON].SetActive(false);
            }
        }
    }
}