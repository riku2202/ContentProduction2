using System.IO;
using UnityEngine;

using Game.Stage;

namespace Game.GameSystem
{
    /// <summary>
    /// �X�e�[�W�f�[�^�̃��[�h�Ǘ��N���X
    /// </summary>
    public class StageDataLoader
    {
        // �X�e�[�W�f�[�^�̃f�B���N�g���[
        private const string STAGEDATA_DIRECTORY = "StageData";

        // Json�^�̃t�@�C���`��
        private const string JSON_FORMAT = ".json";

#if UNITY_EDITOR // �X�e�[�W�f�[�^�̍X�V�̓f�o�b�N���̂�

        /// <summary>
        /// �X�e�[�W�f�[�^�̎擾
        /// </summary>
        /// <returns></returns>
        public static void LoadStageData()
        {
            // �ݒ�t�@�C���p�X�L�[
            const string CONFIG_FILE_KEY = "Editor/StageDataFilePath.json";

            // �ݒ�t�@�C���p�X
            string config_filepath = Path.Combine(Application.dataPath, CONFIG_FILE_KEY);

            // �ݒ�t�@�C��
            string config_file;

            // �t�@�C�������݂���ꍇ
            if (File.Exists(config_filepath))
            {
                config_file = File.ReadAllText(config_filepath);
            }
            else
            {
                return;
            }

            // �X�e�[�W�f�[�^�̃t�@�C���p�X�擾
            string stagedata_filepath = config_file.Trim();

            // �_�u���N�H�[�e�[�V���������O����
            stagedata_filepath = stagedata_filepath.Trim('"');

            // null�`�F�b�N
            if (string.IsNullOrEmpty(stagedata_filepath)) return;

            for (int i = 0; i < GameConstants.STAGE_MAX_NUM; i++)
            {
                // ���݂̃X�e�[�W�擾
                GameConstants.Stage current_stage = (GameConstants.Stage)((int)i);

                // �X�e�[�W�̃f�[�^�p�X
                string stage_filepath = Path.Combine(stagedata_filepath, current_stage.ToString() + JSON_FORMAT);

                if (File.Exists(stage_filepath))
                {
                    // �ۑ�����f�B���N�g���[
                    string save_directory = Path.Combine(Application.streamingAssetsPath, STAGEDATA_DIRECTORY);

                    // �f�B���N�g�������݂��Ȃ��ꍇ�͍쐬
                    if (!Directory.Exists(save_directory))
                    {
                        Directory.CreateDirectory(save_directory);
                    }

                    // �ۑ���̃t�@�C���p�X
                    string assets_filepath = Path.Combine(save_directory, current_stage.ToString() + JSON_FORMAT);

                    // Json�`���̃f�[�^�擾
                    string fileContent = File.ReadAllText(stage_filepath);

                    // Json�`���̃X�e�[�W�f�[�^�쐬
                    File.WriteAllText(assets_filepath, fileContent);

                    DebugManager.LogMessage($"�X�e�[�W�f�[�^�̓ǂݍ��݂ɐ������܂����F{current_stage.ToString()}�F{stagedata_filepath} ");
                }
                else
                {
                    DebugManager.LogMessage($"�X�e�[�W�f�[�^�̓ǂݍ��݂Ɏ��s���܂����F{stagedata_filepath}", DebugManager.MessageType.Warning);

                }
            }
        }

#endif

        /// <summary>
        /// �X�e�[�W�f�[�^�̌Ăяo��
        /// </summary>
        /// <param name="stage_index"></param>
        public static string CellStageData(int stage_index)
        {
            // ���݂̃X�e�[�W��
            GameConstants.Stage current_stage = (GameConstants.Stage)(stage_index);

            // �t�@�C����
            string filename = new string(current_stage + JSON_FORMAT);

            // �t�@�C���p�X
            string filepath = Path.Combine(Application.streamingAssetsPath, STAGEDATA_DIRECTORY);

            // �X�e�[�W�f�[�^�̃p�X
            string stagedata_file = Path.Combine(filepath, filename);

            if (File.Exists(stagedata_file))
            {
                string json = File.ReadAllText(stagedata_file);

                return json;
            }
            else
            {
                return null;
            }
        }
    }
}