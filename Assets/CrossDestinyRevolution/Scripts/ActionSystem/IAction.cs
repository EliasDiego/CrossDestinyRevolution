using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;

namespace CDR.ActionSystem
{
    public interface IAction
    {
        bool isActive { get; }
        void Use();
        void End();

        event System.Action onUse;
        event System.Action onEnd;

        IActiveCharacter Character { get; }
    }
}

