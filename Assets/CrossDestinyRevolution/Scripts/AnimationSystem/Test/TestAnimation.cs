using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace CDR.AnimationSystem
{
    public class TestAnimation : MonoBehaviour
    {
        [SerializeField]
        AnimationEvent _AnimationEvent;

        Animator _Animator;

        private void Awake() 
        {
            _Animator = GetComponent<Animator>();

            AnimationEventsManager manager = GetComponent<AnimationEventsManager>();

            _AnimationEvent.onEventTime += OnEventTime;
            
            manager.AddAnimationEvent("SAttack2", _AnimationEvent);
        }

        void OnEventTime()
        {
            
        }
    }
}