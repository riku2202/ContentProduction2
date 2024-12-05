using UnityEngine;
using System.IO;
using System;

namespace Game.GameSystem
{
    /// <summary>
    /// �f�[�^�̃Z�[�u�ƃ��[�h�Ǘ��N���X
    /// </summary>
    public class SaveSystem
    {
        // �ۑ�����t�@�C���p�X�̎w��
        private static readonly string GameDataFilePath = Application.persistentDataPath + "/gamedata.json";

        // �Q�[���f�[�^�Ǘ��N���X�̌Ăяo��
        private static GameDataManager dataManager = GameDataManager.Instance;

        /// <summary>
        /// �Q�[���f�[�^�̃Z�[�u
        /// </summary>
        public static void SaveManager()
        {
            // �Q�[���f�[�^�̎擾
            GameData data = dataManager.GetGameData();

            // �Q�[���f�[�^�������̏ꍇ
            if (data == null)
            {
                DebugManager.LogMessage("�Z�[�u����f�[�^������܂���F" + GameDataFilePath, DebugManager.MessageType.Warning);
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
                DebugManager.LogMessage("���[�h����f�[�^������܂���F" + GameDataFilePath, DebugManager.MessageType.Warning);
            }

            // �Q�[���f�[�^��ݒ�
            dataManager.SetGameData(loaddata);
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
            File.WriteAllText(GameDataFilePath, json);
            DebugManager.LogMessage("�Q�[���f�[�^���ۑ�����܂����F" + GameDataFilePath);
        }

        /// <summary>
        /// �t�@�C���̃f�[�^�����[�h
        /// </summary>
        private static GameData LoadGameData()
        {
            // �t�@�C�������݂���ꍇ
            if (File.Exists(GameDataFilePath))
            {
                string Json = File.ReadAllText(GameDataFilePath);

                GameData data = JsonUtility.FromJson<GameData>(Json);

                DebugManager.LogMessage("�Q�[���f�[�^���ǂݍ��܂�܂����F" + GameDataFilePath);

                return data;
            }
            // �t�@�C�������݂��Ȃ��ꍇ
            else
            {
                DebugManager.LogMessage("�Q�[���f�[�^��������܂���F" + GameDataFilePath, DebugManager.MessageType.Warning);

                return null;
            }
        }

        public static void DeleteGameData()
        {
            if (File.Exists(GameDataFilePath))
            {
                File.Delete(GameDataFilePath);

                DebugManager.LogMessage("�Q�[���f�[�^���폜����܂����F" + GameDataFilePath);
            }
            else
            {
                DebugManager.LogMessage("�Q�[���f�[�^��������܂���F" + GameDataFilePath);
            }
        }
    }
}