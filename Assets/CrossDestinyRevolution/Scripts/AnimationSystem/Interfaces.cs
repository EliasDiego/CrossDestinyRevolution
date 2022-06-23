using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.ActionSystem;
using CDR.AttackSystem;
using CDR.MovementSystem;

namespace CDR.AnimationSystem
{
    public interface IAnimationEvent
    {
        float timeValue { get; }
        bool isNormalized { get; }
        System.Action onEventTime { get; }
        System.Action onStateEnter { get; }
        System.Action onStateExit { get; }
    }
}