using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VFXSystem
{
    public class BladeWingBoostVFXHandler : MonoBehaviour, IVFXHandler
    {
        private bool _IsActive = false;

        public bool isActive => _IsActive;

        public void Activate()
        {
            _IsActive = true;
        }

        public void Deactivate()
        {
            _IsActive = false;
        }
    }
}