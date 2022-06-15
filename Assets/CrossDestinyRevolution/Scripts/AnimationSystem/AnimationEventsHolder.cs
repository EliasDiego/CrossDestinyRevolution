using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.ActionSystem;

namespace CDR.AnimationSystem
{
    public class AnimationEventsHolder : MonoBehaviour
    {
        private Dictionary<Type, IAnimationEvent[]> _AnimationEvents = new Dictionary<Type, IAnimationEvent[]>();

        public void AddAnimationEvent(Type type, params IAnimationEvent[] animationEvents)
        {
            if(_AnimationEvents.ContainsKey(type))
                _AnimationEvents[type] = _AnimationEvents[type].Union(animationEvents).ToArray();

            else
                _AnimationEvents.Add(type, animationEvents);
        }

        public void RemoveAnimationEvent(Type type, params IAnimationEvent[] animationEvents)
        {
            if(_AnimationEvents.ContainsKey(type))
                _AnimationEvents[type] = _AnimationEvents[type].Except(animationEvents).ToArray();
        }

        public IAnimationEvent[] GetAnimationEvents<T>() where T : IAction
        {
            Type type = typeof(T);

            if(_AnimationEvents.ContainsKey(type))
                return _AnimationEvents[type];

            return null;
        }
    }
}