using System;
using System.Collections;
using System.Collections.Generic;
using CDR.MechSystem;
using UnityEngine;

namespace CDR.AttackSystem
{
    [RequireComponent(typeof(BoxCollider))]
    public class HurtBox : HurtShape, IBox
    {
        BoxCollider _BoxCollider;

        public override Collider collider => _BoxCollider;
        
        public override Vector3 center { get => _BoxCollider.center; set => _BoxCollider.center = value; }
        public Vector3 size { get => _BoxCollider.size; set => _BoxCollider.size = value; }

        private void Awake() 
        {
            _BoxCollider = GetComponent<BoxCollider>();
        }

        #if UNITY_EDITOR
        protected override void OnDrawGizmos() 
        {
            base.OnDrawGizmos();

            if(!UnityEditor.EditorApplication.isPlaying && !_BoxCollider)
                _BoxCollider = GetComponent<BoxCollider>();    

            Gizmos.DrawCube(_BoxCollider.center, _BoxCollider.size);  
        }
        #endif  
    }
}