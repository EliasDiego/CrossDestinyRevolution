using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MovementSystem;

namespace CDR.AnimationSystem
{
    public class BoostEventHandler : AnimationEventHandler
    {
        protected override IAnimationEvent[] GetAnimationEvents(Animator animator)
        {
            AnimationEventsHolder handler = animator.GetComponent<AnimationEventsHolder>();

            return handler?.GetAnimationEvents<IBoost>();
        }
    }
}