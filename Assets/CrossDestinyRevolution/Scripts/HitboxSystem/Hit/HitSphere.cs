using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    [RequireComponent(typeof(SphereCollider))]
    public class HitSphere : HitShape, ISphere
    {
        SphereCollider _SphereCollider;
        
        public override Vector3 center { get => _SphereCollider.center; set => _SphereCollider.center = value; }
        public float radius { get => _SphereCollider.radius; set => _SphereCollider.radius = value; }

        public override Collider collider => _SphereCollider;

        protected override void Awake()
        {
            _SphereCollider = GetComponent<SphereCollider>();
            
            base.Awake();
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            if(!_SphereCollider)
                _SphereCollider = GetComponent<SphereCollider>();

            Gizmos.DrawSphere(_SphereCollider.center, _SphereCollider.radius);
        }
    }
}