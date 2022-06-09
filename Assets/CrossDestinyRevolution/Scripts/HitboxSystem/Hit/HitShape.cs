using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public abstract class HitShape : CollisionShape, IHitShape
    {
        [SerializeField]
        private LayerMask _HitLayer;

        private List<IHurtShape> _EnteredHurtShapes = new List<IHurtShape>();

        public LayerMask hitLayer { get => _HitLayer; set => _HitLayer = value; }

        public override event Action<IHitEnterData> onHitEnter;
        public override event Action<IHitExitData> onHitExit;

        private void FixedUpdate() 
        {
            IEnumerable<IHurtShape> currentHurtShapes = GetColliders()?.Where(c => c.TryGetComponent(out IHurtShape h))?.
                Select(c => c.GetComponent<IHurtShape>()).Where(h => h.character == null && character == null || h.character != character);

            IHurtShape[] exitedHurtShapes = _EnteredHurtShapes?.Except(currentHurtShapes)?.ToArray();
            IHurtShape[] enteredHurtShapes = currentHurtShapes?.Except(_EnteredHurtShapes)?.ToArray();

            foreach(IHurtShape h in exitedHurtShapes)
            {
                HitExitData data = new HitExitData(this, h);

                h.HitExit(data);

                onHitExit?.Invoke(data);

                _EnteredHurtShapes.Remove(h);
            }

            foreach(IHurtShape h in enteredHurtShapes)
            {
                if(Physics.Raycast(position, (h.position - position).normalized, out RaycastHit hit, float.MaxValue, hitLayer))
                {
                    HitEnterData data = new HitEnterData(this, h, hit);

                    h.HitEnter(data);

                    onHitEnter?.Invoke(data);
 
                    _EnteredHurtShapes.Add(h);
                }
            }
        }

        #if UNITY_EDITOR
        protected override void OnDrawGizmos() 
        {
            base.OnDrawGizmos();
            
            Gizmos.color = _EnteredHurtShapes.Count > 0 ? Color.red : Color.green;
        }
        #endif  

        protected abstract Collider[] GetColliders();
        
    }
}