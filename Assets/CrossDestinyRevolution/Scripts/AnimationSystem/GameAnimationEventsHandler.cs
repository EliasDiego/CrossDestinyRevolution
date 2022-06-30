using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AnimationSystem
{
    public class GameAnimationEventsHandler : AnimationEventsHandler
    {
        [SerializeField]
        private string _Name;

        protected override IAnimationEvent[] GetAnimationEvents(Animator animator)
        {
            AnimationEventsManager manager = animator.GetComponent<AnimationEventsManager>();

            if(!manager)
                return null;

            return manager.GetAnimationEvents(_Name);
        }
    }
}