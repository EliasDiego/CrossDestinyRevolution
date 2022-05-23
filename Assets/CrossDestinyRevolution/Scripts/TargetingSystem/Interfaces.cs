using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.ActionSystem;
using CDR.MechSystem;

namespace CDR.TargetingSystem
{
    public interface ITargetHandler : IAction
    {
        event Action<ITargetData> onSwitchTarget;

        ITargetData GetCurrentTarget();

        void GetNextTarget();
    }

    public interface ITargetData
    {
        IActiveCharacter activeCharacter { get; }
        float distance { get; }
        Vector3 direction { get; }
    }
}