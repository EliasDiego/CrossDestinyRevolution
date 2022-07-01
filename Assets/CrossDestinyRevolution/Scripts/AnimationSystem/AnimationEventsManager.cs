using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.ActionSystem;

namespace CDR.AnimationSystem
{
    public class AnimationEventsManager : MonoBehaviour
    {
        private Dictionary<string, IAnimationEvent[]> _AnimationEvents = new Dictionary<string, IAnimationEvent[]>();

        public void AddAnimationEvent<T>(string name, T[] animationEvents) where T : IAnimationEvent
        {
            AddAnimationEvent(name, animationEvents);
        }

        public void AddAnimationEvent(string name, params IAnimationEvent[] animationEvents)
        {
            if(_AnimationEvents.ContainsKey(name))
                _AnimationEvents[name] = _AnimationEvents[name].Union(animationEvents).ToArray();

            else
                _AnimationEvents.Add(name, animationEvents?.Cast<IAnimationEvent>().ToArray());
        }

        public void RemoveAnimationEvent<T>(string name, T[] animationEvents) where T : IAnimationEvent
        {
            RemoveAnimationEvent(name, animationEvents);
        }

        public void RemoveAnimationEvent(string name, params IAnimationEvent[] animationEvents)
        {
            if(_AnimationEvents.ContainsKey(name))
                _AnimationEvents[name] = _AnimationEvents[name].Except(animationEvents).ToArray();
        }

        public IAnimationEvent[] GetAnimationEvents(string name)
        {
            if(_AnimationEvents.ContainsKey(name))
                return _AnimationEvents[name];

            return null;
        }
    }
}