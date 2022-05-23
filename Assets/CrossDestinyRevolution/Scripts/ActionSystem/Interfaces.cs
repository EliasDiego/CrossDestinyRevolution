using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CDR.ActionSystem
{
    public interface IAction
    {
        bool isActive { get; }

        event Action onUse;
        event Action onEnd;

        void Use();
        void End();
    }

    public interface ICoolDownAction : IAction
    {
        float coolDownDuration { get; }
        float currentCoolDownTime { get; }
        bool isCoolingDown { get; }

        event Action<ICoolDownAction> onCoolDown;

        void EndCoolDown();
    }
}