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
        void OnEventTime(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
        void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
        void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    }
}