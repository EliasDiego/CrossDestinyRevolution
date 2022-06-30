using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;

namespace CDR.AttackSystem
{
    public abstract class CollisionShape : MonoBehaviour, ICollisionShape
    {
        [SerializeField]
        ActiveCharacter _ActiveCharacter;

        public abstract new Collider collider { get; }

        public IActiveCharacter character { get => _ActiveCharacter; set => _ActiveCharacter = (ActiveCharacter)value; }
        public abstract Vector3 center { get; set; }

        public abstract event Action<IHitData> onHitEnter;
        public abstract event Action<IHitData> onHitExit;
        
        protected virtual void OnDrawGizmos() 
        {
            Gizmos.matrix = transform.localToWorldMatrix;
        }
    }
}