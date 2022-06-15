using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

using CDR.ActionSystem;

namespace CDR.AnimationSystem
{
    public abstract class AnimationEventHandler : StateMachineBehaviour
    {
        private IAnimationEvent[] _AnimationEvents;
        private float _CurrentTime;

        private bool _IsStateFinished = false;

        private void TryInvokeEvent(System.Action animationEvent, float eventTime, float deltaTime)
        {
            if(eventTime >= 0 && eventTime <= deltaTime)
                animationEvent?.Invoke();
        }

        private void InvokeAnimationEvents(IAnimationEvent[] animationEvents, float length, float currentTime, float deltaTime)
        {
            float sign = Mathf.Sign(deltaTime);

            for(int i = 0; i < animationEvents.Length; i++)
            {
                float timeStamp = animationEvents[i].isNormalized ? animationEvents[i].timeValue * length : animationEvents[i].timeValue;

                float eventTime = timeStamp - currentTime;

                TryInvokeEvent(animationEvents[i].onEventTime, sign * eventTime, deltaTime);
            }
        }

        protected abstract IAnimationEvent[] GetAnimationEvents(Animator animator);
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            _AnimationEvents = GetAnimationEvents(animator);

            _CurrentTime = 0;

            _IsStateFinished = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            if(_AnimationEvents == null || _AnimationEvents?.Length <= 0 || _IsStateFinished)
                return;

            float deltaTime = stateInfo.speed * stateInfo.speedMultiplier * Time.deltaTime;

            InvokeAnimationEvents(_AnimationEvents, stateInfo.length, _CurrentTime, deltaTime);

            _CurrentTime += Time.deltaTime;

            if(_CurrentTime >= stateInfo.length)
            {
                _IsStateFinished = !stateInfo.loop;

                _CurrentTime = 0;
            }
        }
    }
}