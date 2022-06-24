using System;
using System.Linq;
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

        public float radius { get => _Radius; set => _Radius = value; }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            Gizmos.DrawSphere(_Center, _Radius);
        }

        protected override HitEnterData[] GetHitData(Vector3 velocity)
        {
            return Physics.SphereCastAll(position, _Radius, velocity.normalized, velocity.magnitude * Time.fixedDeltaTime, hitLayer)?.
                Select(r => new HitEnterData(this, r.collider.GetComponent<IHurtShape>(), r))?.Where(h => h.hurtShape != null)?.ToArray();
        }
    }
}