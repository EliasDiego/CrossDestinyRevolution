using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace CDR.AnimationSystem
{
    public abstract class AnimationEventHandler : StateMachineBehaviour
    {
        [SerializeField]
        private float[] _EventTimeStamp;

        private float _CurrentTime;

        private IAnimationEventCaller _Caller;

        protected abstract IAnimationEventCaller GetCaller(Animator animator);

        private void TryInvokeEvent(System.Action animationEvent, float eventTime, float deltaTime)
        {
            if(eventTime >= 0 && eventTime <= deltaTime)
                animationEvent?.Invoke();
        }

        private void InvokeAnimationEvents(IAnimationEventCaller caller, float currentTime, float deltaTime)
        {
            float sign = Mathf.Sign(deltaTime);

            for(int i = 0; i < _EventTimeStamp.Length; i++)
            {
                if(i >= caller.animationEvents.Length)
                    return;

                float eventTime = _EventTimeStamp[i] - currentTime;

                TryInvokeEvent(caller.animationEvents[i], sign * eventTime, deltaTime);
            }
        }
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            _Caller = GetCaller(animator);

            _CurrentTime = 0;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            
            if(_Caller == null)
                return;

            float deltaTime = stateInfo.speed * stateInfo.speedMultiplier * Time.deltaTime;

            InvokeAnimationEvents(_Caller, _CurrentTime, deltaTime);

            _CurrentTime += Time.deltaTime;

            if(_CurrentTime > stateInfo.length)
                _CurrentTime = 0;
        }
    }
}