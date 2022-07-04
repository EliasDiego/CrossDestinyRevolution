using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VFXSystem
{
    public class BladeWingBoostVFXHandler : BoostVFXHandler
    {
        private bool _IsActive = false;

        public override bool isActive => _IsActive;

        public override void Activate()
        {
            _IsActive = true;
        }

        public override void Deactivate()
        {
            _IsActive = false;
        }
    }
}