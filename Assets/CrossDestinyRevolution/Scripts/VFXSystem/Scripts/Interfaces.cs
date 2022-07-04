using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VFXSystem
{
    public interface IVFXHandler
    {
        bool isActive { get; }
        
        void Activate();
        void Deactivate();
    }
}