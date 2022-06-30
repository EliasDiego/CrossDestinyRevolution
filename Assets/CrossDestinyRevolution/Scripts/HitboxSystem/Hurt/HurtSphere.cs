using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    [RequireComponent(typeof(SphereCollider))]
    public class HurtSphere : HurtShape, ISphere
    {
        SphereCollider _SphereCollider;
        
        public override Vector3 center { get => _SphereCollider.center; set => _SphereCollider.center = value; }
        public float radius { get => _SphereCollider.radius; set => _SphereCollider.radius = value; }

        public override Collider collider => _SphereCollider;

        private void Awake() 
        {
            _SphereCollider = GetComponent<SphereCollider>();    
        }

        #if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            if(!UnityEditor.EditorApplication.isPlaying && !_SphereCollider)
                _SphereCollider = GetComponent<SphereCollider>();    

            Gizmos.DrawSphere(_SphereCollider.center, _SphereCollider.radius);
        }
        #endif
    }
}