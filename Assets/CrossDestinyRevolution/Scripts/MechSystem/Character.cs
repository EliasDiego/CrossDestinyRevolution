using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MechSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] Animator _animator;
        AudioSource _audioSource;

        public Animator animator => _animator;
        public AudioSource audioSource =>  _audioSource;

        protected virtual void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }
}

