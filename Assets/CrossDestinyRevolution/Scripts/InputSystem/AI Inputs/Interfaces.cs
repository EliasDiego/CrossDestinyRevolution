using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;

namespace CDR.InputSystem
{
    public interface IAILogic
    {
        bool Evaluate(IActiveCharacter character);
    }
}