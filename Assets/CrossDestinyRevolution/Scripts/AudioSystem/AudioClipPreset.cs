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
        int _Priority;
        [SerializeField, Range(0, 1)]
        float _Volume;
        [SerializeField, Range(-3, 3)]
        float _Pitch;
        [SerializeField]
        bool _IsRandomPitch;
        [SerializeField]
        float _MinPitch;
        [SerializeField]
        float _MaxPitch;

        public AudioClip audioClip => _AudioClip;
        public AudioMixerGroup outputAudioMixerGroup => _OutputAudioMixerGroup;
        public int priority => _Priority;
        public float volume => _Volume;
        public float pitch => _Pitch;

        private void SetAudioSourcePreset(AudioSource audioSource)
        {
            audioSource.outputAudioMixerGroup = outputAudioMixerGroup;
            audioSource.priority = priority;
            audioSource.volume = volume;
            audioSource.pitch = _IsRandomPitch ? Random.Range(_MinPitch, _MaxPitch) : _Pitch;
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