using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AnimationSystem
{
    [System.Serializable]
    public struct AnimationEvent : IAnimationEvent
    {
        [SerializeField]
        private float _TimeValue;
        [SerializeField]
        private bool _IsNormalized;

        public AnimationEvent(float timeValue, bool isNormalized, Action onEventTime, Action onStateEnter, Action onStateExit)
        {
            _TimeValue = timeValue;
            _IsNormalized = isNormalized;
            this.onEventTime = onEventTime;
            this.onStateEnter = onStateEnter;
            this.onStateExit = onStateExit;
        }

        public AnimationEvent(float timeValue, bool isNormalized, Action onEventTime) : this(timeValue, isNormalized, onEventTime, null, null) { }

        public float timeValue => _TimeValue;
        public bool isNormalized => _IsNormalized;
        public Action onEventTime { get; set; }
        public Action onStateEnter { get; set; }
        public Action onStateExit { get; set; }

        public void OnEventTime(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            onEventTime?.Invoke();
        }

        public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            onStateEnter?.Invoke();
        }

        public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            onStateExit?.Invoke();
        }
    }
}