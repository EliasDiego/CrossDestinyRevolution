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

        public AnimationEvent(float timeValue, bool isNormalized, System.Action onEventTime)
        {
            _TimeValue = timeValue;
            _IsNormalized = isNormalized;
            this.onEventTime = onEventTime;
        }

        public float timeValue => _TimeValue;
        public bool isNormalized => _IsNormalized;
        public System.Action onEventTime { get; set; }
    }
}