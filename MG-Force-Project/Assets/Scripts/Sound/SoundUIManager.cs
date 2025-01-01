using UnityEngine;
using UnityEngine.UI;

namespace Game 
{ 
    public class SoundUIManager : MonoBehaviour 
    {
        // �����{
        private const float INTEGER_TIMES = 4;

        // �{�����[���萔
        private enum Volume
        {
            MIN = 0,
            HAFE = 2,
            MAX = 4,
        }

        // �{�����[��UI�Ǘ��p
        private enum VolumeUI
        {
            MIN,
            SOFT,
            LOUD,
            MAX,

            MAX_SIZE,
        }

        // BGM��UI
        [SerializeField] private Image _bgmVolume;
        // SE��UI
        [SerializeField] private Image _seVoluem;
        // BGM��AudioSorce
        [SerializeField] private AudioSource _bgmAudioSource;
        // SE��AudioSource
        [SerializeField] private AudioSource _seAudioSource;
        // UI�X�v���C�g
        [SerializeField] private Sprite[] _volumeUI = new Sprite[(int)VolumeUI.MAX_SIZE];

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update()
        {
            BGMUIUpdate();

            SEUIUpdate();
        }

        /// <summary>
        /// BGMUI�X�V
        /// </summary>
        private void BGMUIUpdate()
        {
            if (_bgmAudioSource.mute)
            {
                _bgmVolume.sprite = _volumeUI[(int)VolumeUI.MIN];
                return;
            }

            switch (CheckCurrentVolume(_bgmAudioSource.volume))
            {
                case VolumeUI.MIN:
                    _bgmVolume.sprite = _volumeUI[(int)VolumeUI.MIN];
                    break;

                case VolumeUI.SOFT:
                    _bgmVolume.sprite = _volumeUI[(int)VolumeUI.SOFT];
                    break;

                case VolumeUI.LOUD:
                    _bgmVolume.sprite = _volumeUI[(int)VolumeUI.LOUD];
                    break;

                case VolumeUI.MAX:
                    _bgmVolume.sprite = _volumeUI[(int)VolumeUI.MAX];
                    break;
            }
        }

        /// <summary>
        /// SEUI�X�V
        /// </summary>
        private void SEUIUpdate()
        {
            if (_seAudioSource.mute)
            {
                _seVoluem.sprite = _volumeUI[(int)VolumeUI.MIN];
                return;
            }

            switch (CheckCurrentVolume(_seAudioSource.volume))
            {
                case VolumeUI.MIN:
                    _seVoluem.sprite = _volumeUI[(int)VolumeUI.MIN];
                    break;

                case VolumeUI.SOFT:
                    _seVoluem.sprite = _volumeUI[(int)VolumeUI.SOFT];
                    break;

                case VolumeUI.LOUD:
                    _seVoluem.sprite = _volumeUI[(int)VolumeUI.LOUD];
                    break;

                case VolumeUI.MAX:
                    _seVoluem.sprite = _volumeUI[(int)VolumeUI.MAX];
                    break;
            }
        }

        /// <summary>
        /// ���ʃ`�F�b�N
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        private VolumeUI CheckCurrentVolume(float volume)
        {
            volume *= INTEGER_TIMES;

            if (volume == (int)Volume.MIN) return VolumeUI.MIN;

            else if (volume == (int)Volume.MAX) return VolumeUI.MAX;

            else if (volume <= (int)Volume.HAFE) return VolumeUI.SOFT;

            else return VolumeUI.LOUD;
        }
    }
}