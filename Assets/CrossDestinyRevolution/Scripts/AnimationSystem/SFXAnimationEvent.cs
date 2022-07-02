using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.AudioSystem;

namespace CDR.AnimationSystem
{
    [System.Serializable]
    public struct SFXAnimationEvent : IAnimationEvent
    {
        [SerializeField]
        private float _TimeValue;
        [SerializeField]
        private bool _IsNormalized;
        [SerializeField]
        AudioClipPreset _ClipPreset;
        public float timeValue => _TimeValue;

        public bool isNormalized => _IsNormalized;

        public void OnEventTime(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _ClipPreset?.PlayOneShot(animator.GetComponent<AudioSource>());
        }

        public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
    }
}