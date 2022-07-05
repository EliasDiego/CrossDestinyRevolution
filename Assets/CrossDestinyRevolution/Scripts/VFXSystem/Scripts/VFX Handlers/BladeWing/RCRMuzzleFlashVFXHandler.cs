using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.VFX;

namespace CDR.VFXSystem
{
    public class RCRMuzzleFlashVFXHandler : MonoBehaviour, IVFXHandler
    {
        [SerializeField]
        VisualEffect[] _VisualEffects;

        public bool isActive => false;

        public void Activate()
        {
            foreach(VisualEffect v in _VisualEffects)
                v.Play();
        }

        public void Deactivate()
        {
            foreach(VisualEffect v in _VisualEffects)
                v.Stop();
        }
    }
}