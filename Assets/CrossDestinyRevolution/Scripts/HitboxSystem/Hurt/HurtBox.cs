using System;
using System.Collections;
using System.Collections.Generic;
using CDR.MechSystem;
using UnityEngine;

namespace CDR.AttackSystem
{
    [RequireComponent(typeof(BoxCollider))]
    public class HurtBox : HurtShape
    {
        BoxCollider _BoxCollider;

        public override Vector3 position => transform.position + _BoxCollider.center;

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