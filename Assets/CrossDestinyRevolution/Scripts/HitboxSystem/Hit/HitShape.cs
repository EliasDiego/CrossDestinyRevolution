using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MovementSystem;

namespace CDR.AttackSystem
{
    public abstract class HitShape : CollisionShape, IHitShape
    {
        private List<IHurtShape> _IntersectedHurtShapes = new List<IHurtShape>();

        public IHurtShape[] intersectedHurtShapes => _IntersectedHurtShapes.ToArray();

        public override event Action<IHitData> onHitEnter;
        public override event Action<IHitData> onHitExit;

        protected virtual void Awake() 
        {
            collider.isTrigger = true;
        }

        protected override void OnDrawGizmos() 
        {
            base.OnDrawGizmos();
            
            Gizmos.color = _IntersectedHurtShapes.Count > 0 ? Color.red : Color.green;
        }
        
        private void OnTriggerEnter(Collider other) 
        {
            if (!other.TryGetComponent(out IHurtShape hurtShape))
                return;
                
            HitData hitData = new HitData(null, hurtShape);

            if(!_IntersectedHurtShapes.Contains(hurtShape))
                _IntersectedHurtShapes.Add(hurtShape);

            onHitEnter?.Invoke(hitData);

            hurtShape.HitEnter(null);
        }
        
        private void OnTriggerExit(Collider other) 
        {
            if (!other.TryGetComponent(out IHurtShape hurtShape))
                return;

            HitData hitData = new HitData(null, hurtShape);

            if(_IntersectedHurtShapes.Contains(hurtShape))
                _IntersectedHurtShapes.Remove(hurtShape);

            onHitExit?.Invoke(hitData);

            hurtShape.HitExit(null);
        }
    }
}