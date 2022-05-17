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

        ActiveCharacter Character { get; }
    }
}

