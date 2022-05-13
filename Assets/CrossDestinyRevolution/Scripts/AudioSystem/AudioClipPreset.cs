using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;

namespace CDR.AudioSystem
{
    [CreateAssetMenu(menuName = "AudioSystem/Audio Clip Preset")]
    public class AudioClipPreset : ScriptableObject
    {
        [SerializeField]
        AudioClip _AudioClip;
        [SerializeField]
        AudioMixerGroup _OutputAudioMixerGroup;
        [SerializeField, Range(0, 256)]
        int _Priority = 128;
        [SerializeField, Range(0, 1)]
        float _Volume = 1;
        [SerializeField, Range(-3, 3)]
        float _Pitch = 1;
        [SerializeField]
        bool _IsRandomPitch = false;
        [SerializeField]
        float _MinPitch = 1;
        [SerializeField]
        float _MaxPitch = 1;

        float _RandomPitch = 1;

        public AudioClip audioClip => _AudioClip;
        public AudioMixerGroup outputAudioMixerGroup => _OutputAudioMixerGroup;
        public int priority => _Priority;
        public float volume => _Volume;
        public float pitch => _Pitch;
        public float randomPitch => _RandomPitch;

        private void SetAudioSourcePreset(AudioSource audioSource)
        {
            audioSource.outputAudioMixerGroup = outputAudioMixerGroup;
            audioSource.priority = priority;
            audioSource.volume = volume;

            if(_IsRandomPitch)
            {
                _RandomPitch = Random.Range(_MinPitch, _MaxPitch);

                audioSource.pitch = _RandomPitch;
            }

            else
                audioSource.pitch = _Pitch;
        }

        public void Play(AudioSource audioSource)
        {
            SetAudioSourcePreset(audioSource);

            audioSource.clip = audioClip;
            audioSource.Play();
        }

        public void PlayOneShot(AudioSource audioSource)
        {
            SetAudioSourcePreset(audioSource);

            audioSource.PlayOneShot(_AudioClip);
        }
    }
}