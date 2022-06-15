using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AnimationSystem
{
    public class TestAnimationeventHandler : AnimationEventHandler
    {
        protected override IAnimationEventCaller GetCaller(Animator animator)
        {
            return animator.GetComponent<IAnimationEventCaller>();
        }
    }
}