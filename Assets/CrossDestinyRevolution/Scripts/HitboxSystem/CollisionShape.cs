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

        public abstract Vector3 position { get; }

        public IActiveCharacter character { get => _ActiveCharacter; set => _ActiveCharacter = (ActiveCharacter)value; }
        
        public abstract event Action<IHitEnterData> onHitEnter;
        public abstract event Action<IHitExitData> onHitExit;
        
        protected virtual void OnDrawGizmos() 
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            
        }
    }
}