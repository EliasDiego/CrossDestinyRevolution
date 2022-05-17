using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MechSystem
{
    [RequireComponent(typeof(AudioSource), typeof(Animator))]
    public class Character : MonoBehaviour
    {
        AudioSource audioSource;
        Animator animator;

        public AudioSource AudioSource => audioSource;
        public Animator Animator => animator;

        protected virtual void Awake() {
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }
    }
}

