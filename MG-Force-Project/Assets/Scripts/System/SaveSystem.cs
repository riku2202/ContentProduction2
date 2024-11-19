using UnityEngine;
using System.IO;

namespace Game.GameSystem
{
    /// <summary>
    /// �f�[�^�̃Z�[�u�ƃ��[�h�Ǘ�
    /// </summary>
    public class SaveSystem
    {
        // �t�@�C���p�X�̎w��
        private static readonly string FilePath = Application.persistentDataPath + "/gamedata.json";

        /// <summary>
        /// �Q�[���f�[�^�̃Z�[�u
        /// </summary>
        public static void SaveManager()
        {
            // �Q�[���f�[�^�̎擾
            GameData data = GameDataManager.Instance.GetGameData();

            // �Q�[���f�[�^�������̏ꍇ
            if (data == null)
            {
                Debug.Log("�ySystem�z�Z�[�u����f�[�^������܂��� : ");
                return;
            }

            // �Q�[���f�[�^�̕ۑ�
            SaveGameData(data);
        }

        /// <summary>
        /// �Q�[���f�[�^�̃��[�h
        /// </summary>
        public static void LoadManager()
        {
            // �Q�[���f�[�^�̃��[�h
            GameData loaddata = LoadGameData();

            // �Q�[���f�[�^�������̏ꍇ
            if (loaddata == null)
            {
                Debug.Log("�ySystem�z���[�h����f�[�^������܂��� : ");
            }

            // �Q�[���f�[�^��ݒ�
            GameDataManager.Instance.SetGameData(loaddata);
        }

        /// <summary>
        /// �f�[�^���t�@�C���ɕۑ�
        /// </summary>
        /// <param name="data"></param>
        private static void SaveGameData(GameData data)
        {
            // GameData��Json�`���ɕϊ�
            string json = JsonUtility.ToJson(data);

            // Json�f�[�^���t�@�C���ɏ�������
            File.WriteAllText(FilePath, json);
            Debug.Log("�ySystem�z�Q�[���f�[�^���ۑ�����܂��� : " + FilePath);
        }

        /// <summary>
        /// �t�@�C���̃f�[�^�����[�h
        /// </summary>
        private static GameData LoadGameData()
        {
            // �t�@�C�������݂���ꍇ
            if (File.Exists(FilePath))
            {
                string Json = File.ReadAllText(FilePath);

                GameData data = JsonUtility.FromJson<GameData>(Json);

                Debug.Log("�ySystem�z�Q�[���f�[�^���ǂݍ��܂�܂��� : " + FilePath);
                return data;
            }
            // �t�@�C�������݂��Ȃ��ꍇ
            else
            {
                Debug.Log("�ySystem�z�Q�[���f�[�^��������܂��� : " + FilePath);
                return null;
            }
        }
    }
}