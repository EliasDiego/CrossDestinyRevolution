using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    [RequireComponent(typeof(SphereCollider))]
    public class HurtSphere : HurtShape
    {
        SphereCollider _SphereCollider;

        public override Vector3 position => transform.position + _SphereCollider.center;

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