using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.VFX;

namespace CDR.VFXSystem
{
    public class NightmareStalkerVFXHandler : MonoBehaviour, IVFXHandler
    {
        private VisualEffect _MuzzleFlashEffect;
        
        public bool isActive => false;

        private void Awake() 
        {
            _MuzzleFlashEffect = GetComponent<VisualEffect>();
        }

        public void Activate()
        {
            _MuzzleFlashEffect.Play();
        }

        public void Deactivate()
        {
            _MuzzleFlashEffect.Stop();
        }
    }
}