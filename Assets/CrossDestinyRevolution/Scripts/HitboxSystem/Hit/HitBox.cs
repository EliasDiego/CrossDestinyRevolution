using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CDR.AttackSystem
{
    [RequireComponent(typeof(BoxCollider))]
    public class HitBox : HitShape, IBox
    {
        BoxCollider _BoxCollider;
        
        public override Vector3 center { get => _BoxCollider.center; set => _BoxCollider.center = value; }
        public Vector3 size { get => _BoxCollider.size; set => _BoxCollider.size = value; }

        public override Collider collider => _BoxCollider;

        protected override void Awake()
        {
            _BoxCollider = GetComponent<BoxCollider>();
            
            base.Awake();
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            if(!_BoxCollider)
                _BoxCollider = GetComponent<BoxCollider>();

            Gizmos.DrawCube(_BoxCollider.center, _BoxCollider.size);
        }
    }
}