using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VFXSystem
{
    public abstract class BoostVFXHandler : MonoBehaviour, IVFXHandler
    {
        public abstract bool isActive { get; }

        public abstract void Activate();

        public abstract void Deactivate();
    }
}