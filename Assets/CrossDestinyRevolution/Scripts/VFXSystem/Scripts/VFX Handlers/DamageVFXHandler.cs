using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.VFX;

namespace CDR.VFXSystem
{
    public class DamageVFXHandler : MonoBehaviour, IVFXHandler
    {
        VisualEffect _DamageEffect;

        public bool isActive => false;

        private void Awake()
        {
            _DamageEffect = GetComponent<VisualEffect>();
        }

        public void Activate()
        {
            _DamageEffect.Play();
        }

        public void Deactivate()
        {
            _DamageEffect.Stop();
        }
    }
}