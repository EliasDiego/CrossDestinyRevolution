using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CDR.ActionSystem
{
    public interface ICooldownAction
    {
        float cooldownDuration { get; }
        float currentCooldown { get; }
        bool isCoolingDown { get; }

        event Action<ICooldownAction> onCoolDown;

        void EndCoolDown();
    }
}

