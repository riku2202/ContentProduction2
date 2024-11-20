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
                DebugManager.LogMessage("�Z�[�u����f�[�^������܂���F" + FilePath, DebugManager.MessageType.Warning);
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
                DebugManager.LogMessage("���[�h����f�[�^������܂���F" + FilePath, DebugManager.MessageType.Warning);
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
            DebugManager.LogMessage("�Q�[���f�[�^���ۑ�����܂����F" + FilePath);
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

                DebugManager.LogMessage("�Q�[���f�[�^���ǂݍ��܂�܂����F" + FilePath);

                return data;
            }
            // �t�@�C�������݂��Ȃ��ꍇ
            else
            {
                DebugManager.LogMessage("�Q�[���f�[�^��������܂���F" + FilePath, DebugManager.MessageType.Warning);

                return null;
            }
        }
    }
}