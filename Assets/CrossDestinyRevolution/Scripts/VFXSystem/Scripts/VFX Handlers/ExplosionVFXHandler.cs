using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VFXSystem
{
    public class ExplosionVFXHandler : MonoBehaviour, IVFXHandler
    {
        [SerializeField]
        private ParticleSystem _ParticleSystem;

        public bool isActive => _ParticleSystem.isPlaying;

        public void Activate()
        {
            _ParticleSystem.Play();
        }

        public void Deactivate()
        {
            _ParticleSystem.Stop();
        }
    }
}