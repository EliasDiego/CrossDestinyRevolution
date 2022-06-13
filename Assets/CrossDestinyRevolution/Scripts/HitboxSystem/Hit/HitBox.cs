using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;

namespace CDR.AttackSystem
{
    public class HitBox : HitShape
    {
        [SerializeField]
        Bounds _Bounds;
        public override Vector3 position => transform.position + _Bounds.center;

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.DrawCube(_Bounds.center, _Bounds.extents);
        }

        protected override HitData[] GetHitData(Vector3 velocity)
        {
            return Physics.BoxCastAll(position, _Bounds.extents / 2, velocity.normalized, transform.rotation, velocity.magnitude, hitLayer)?.
                Select(r => new HitData() { hit = r, hurtShape = r.collider.GetComponent<IHurtShape>() })?.Where(h => h.hurtShape != null)?.ToArray();
        }
    }
}