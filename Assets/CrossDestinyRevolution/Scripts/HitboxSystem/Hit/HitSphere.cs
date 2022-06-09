using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class HitSphere : HitShape
    {
        [SerializeField]
        Vector3 _Center;
        [SerializeField]
        float _Radius;

        public override Vector3 position => transform.position + _Center;

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            Gizmos.DrawSphere(_Center, _Radius);
        }

        protected override Collider[] GetColliders()
        {
            return Physics.OverlapSphere(position, _Radius, hitLayer);
        }
    }
}