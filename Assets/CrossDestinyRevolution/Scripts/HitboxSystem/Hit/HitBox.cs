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

        protected override Collider[] GetColliders()
        {
            return Physics.OverlapBox(position, _Bounds.extents / 2, transform.rotation, hitLayer);
        }
    }
}