using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    public class AudioMixerController : Singleton<AudioMixerController>
    {
        [SerializeField]
        private AudioMixer _mixer;
        public AudioMixer GetMixer => _mixer;
        [SerializeField]
        private AudioSource _music;
        [SerializeField]
        private AudioSource _embient;
        [SerializeField]
        private AudioSource _ui;

        [SerializeField]
        private List<AudioClip> _backgroundMusic = new();
        private AudioClip _musicClip;

        void Start()
        {
            DontDestroyOnLoad(gameObject);

            _musicClip = _backgroundMusic[Random.Range(0, _backgroundMusic.Count)];
            PlayMusic();
        }

        public void PlayMusic()
        {
            _music.clip = _musicClip;
            _music.Play();
        }
    }
}