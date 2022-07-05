using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

namespace CDR.AnimationSystem
{
    public class AnimationEventsHandler : StateMachineBehaviour
    {
        [SerializeField]
        private string _Name;

        private IAnimationEvent[] _AnimationEvents;
        private float _CurrentTime;

        private bool _IsStateFinished = false;

        private Animator _Animator;
        private AnimatorStateInfo _CurrentStateInfo;
        private int _CurrentLayerIndex;

        private void TryInvokeEvent(IAnimationEvent animationEvent, float eventTime, float deltaTime)
        {
            if(eventTime >= 0 && eventTime <= deltaTime)
                animationEvent?.OnEventTime(_Animator, _CurrentStateInfo, _CurrentLayerIndex);
        }

        private void InvokeAnimationEvents(IAnimationEvent[] animationEvents, float length, float currentTime, float deltaTime)
        {
            float sign = Mathf.Sign(deltaTime);

            for(int i = 0; i < animationEvents.Length; i++)
            {
                float timeStamp = animationEvents[i].isNormalized ? animationEvents[i].timeValue * length : animationEvents[i].timeValue;

                float eventTime = timeStamp - currentTime;

                TryInvokeEvent(animationEvents[i], sign * eventTime, deltaTime);
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            
            AnimationEventsManager manager = animator.GetComponent<AnimationEventsManager>();

            if(!manager)
                return;

            _AnimationEvents = manager.GetAnimationEvents(_Name);

            if(_AnimationEvents == null || _AnimationEvents.Length <= 0)
                return;

            _CurrentTime = 0;

            _IsStateFinished = false;

            for(int i = 0; i < _AnimationEvents.Length; i++)
                _AnimationEvents[i].OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            if(_AnimationEvents == null || _AnimationEvents.Length <= 0)
                return;

            for(int i = 0; i < _AnimationEvents.Length; i++)
                _AnimationEvents[i].OnStateExit(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            if(_AnimationEvents == null || _AnimationEvents?.Length <= 0 || _IsStateFinished)
                return;

            float deltaTime = stateInfo.speed * stateInfo.speedMultiplier * Time.deltaTime;

            _Animator = animator;
            _CurrentStateInfo = stateInfo;
            _CurrentLayerIndex = layerIndex;

            InvokeAnimationEvents(_AnimationEvents, stateInfo.length, _CurrentTime, deltaTime);

            _CurrentTime += deltaTime;

            if(_CurrentTime >= stateInfo.length)
            {
                _IsStateFinished = !stateInfo.loop;

                _CurrentTime = 0;
            }
        }
    }
}