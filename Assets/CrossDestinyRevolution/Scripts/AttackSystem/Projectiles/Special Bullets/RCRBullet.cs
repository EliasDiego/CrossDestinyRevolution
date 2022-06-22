using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class RCRBullet : Projectile
    {
        [Header("Unique properties")]
        [SerializeField]
        private Transform pivot;
        [SerializeField]
        private float maxLength;
        [SerializeField]
        private float timeToScale;

        public override void OnEnable()
        {
            base.OnEnable();
            HitBox.onHitEnter += OnHitEnter;
        }

        private void OnDestroy()
        {       
            HitBox.onHitEnter -= OnHitEnter;            
        }

        private void FixedUpdate()
        {
            AddRbForce(transform.forward);
        }

        protected override void OnHitEnter(IHitEnterData hitData)
        {
            Debug.Log("ALSKNd");
        }
    }
}
