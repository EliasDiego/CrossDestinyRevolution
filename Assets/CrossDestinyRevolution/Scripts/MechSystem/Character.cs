using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MechSystem
{
    [RequireComponent(typeof(AudioSource), typeof(Animator))]
    public class Character : MonoBehaviour, ICharacter
    {
        Animator _animator;
        AudioSource _audioSource;

        public Animator animator => _animator;

        public AudioSource audioSource =>  _audioSource;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }
    }
}

