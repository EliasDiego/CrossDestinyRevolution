using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VFXSystem
{
    public class BladeWingBoostVFXHandler : BoostVFXHandler
    {
        private ParticleSystem _ParticleSystem;

        public override bool isActive => _ParticleSystem.isPlaying;

        private void Awake() 
        {
            _ParticleSystem = GetComponent<ParticleSystem>();    
        }

        public override void Activate()
        {
            _ParticleSystem.Play();
        }

        public override void Deactivate()
        {
            _ParticleSystem.Stop();
        }
    }
}