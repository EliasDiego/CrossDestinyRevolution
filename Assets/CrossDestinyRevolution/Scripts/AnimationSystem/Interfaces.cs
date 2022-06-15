using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AnimationSystem
{
    public interface IAnimationEventCaller
    {
        Action[] animationEvents { get; }
    }
}