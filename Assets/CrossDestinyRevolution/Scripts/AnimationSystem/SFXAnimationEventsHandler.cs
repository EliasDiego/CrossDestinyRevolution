using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.AudioSystem;

namespace CDR.AnimationSystem
{
    public class SFXAnimationEventsHandler : AnimationEventsHandler
    {
        [SerializeField]
        private string _Name;

        protected override IAnimationEvent[] GetAnimationEvents(Animator animator)
        {
            AnimationEventsManager manager = animator.GetComponent<AnimationEventsManager>();

            if(!manager)
                return null;

            return manager.GetAnimationEvents(_Name)?.Where(a => a is SFXAnimationEvent)?.Cast<IAnimationEvent>()?.ToArray();
        }
    }
}