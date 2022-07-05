using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VFXSystem
{
    public class RCRTestVFXHandler : MonoBehaviour, IVFXHandler
    {
        [SerializeField]
        RCRLaserVFXHandler _LaserVFXHandler;
        [SerializeField]
        RCRMuzzleFlashVFXHandler _MuzzleFlashVFXHandler;

        private bool _IsActive = false;

        public bool isActive => _IsActive;

        public void Activate()
        {
            _LaserVFXHandler.Activate();
            _MuzzleFlashVFXHandler.Activate();
            _IsActive = true;
        }

        public void Deactivate()
        {
            _LaserVFXHandler.Deactivate();
            _MuzzleFlashVFXHandler.Deactivate();
            _IsActive = false;
        }
    }
}