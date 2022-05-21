using CDR.ActionSystem;
using CDR.MechSystem;
using UnityEngine;
using System;

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
        ActiveCharacter activeCharacter { get; }
        float distance { get; }
        Vector3 direction { get; }
    }
}
