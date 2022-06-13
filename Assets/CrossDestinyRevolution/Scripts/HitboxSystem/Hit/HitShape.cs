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
        [SerializeReference]
        private Controller _Controller;
        [SerializeField]
        private LayerMask _HitLayer;

        private List<IHurtShape> _EnteredHurtShapes = new List<IHurtShape>();

        private readonly Vector3 _MinimumVelocity = Vector3.one * 0.001f;

        public LayerMask hitLayer { get => _HitLayer; set => _HitLayer = value; }
        public IController controller { get => _Controller; set => _Controller = (Controller)value; }

        public override event Action<IHitEnterData> onHitEnter;
        public override event Action<IHitExitData> onHitExit;

        private void FixedUpdate() 
        {
            Vector3 velocity = _Controller?.velocity ?? _MinimumVelocity;

            HitEnterData[] hitDatas = GetHitData(velocity);

            IEnumerable<HitEnterData> sortedHitDatas = GetHitData(velocity).Where(h => h.hurtShape.character == null && character == null || h.hurtShape.character != character);

            IHurtShape[] exitedHurtShapes = _EnteredHurtShapes?.Except(sortedHitDatas?.Select(h => h.hurtShape))?.ToArray();
            HitEnterData[] enteredHitData = sortedHitDatas?.Where(h => !_EnteredHurtShapes.Contains(h.hurtShape)).ToArray();//?.Except(_EnteredHurtShapes)?.ToArray();

            foreach(IHurtShape h in exitedHurtShapes)
            {
                HitExitData data = new HitExitData(this, h);

                h.HitExit(data);

                onHitExit?.Invoke(data);

                _EnteredHurtShapes.Remove(h);
            }

            foreach(HitEnterData h in enteredHitData)
            {
                h.hurtShape.HitEnter(h);

                onHitEnter?.Invoke(h);

                _EnteredHurtShapes.Add(h.hurtShape);
            }
        }

        #if UNITY_EDITOR
        protected override void OnDrawGizmos() 
        {
            base.OnDrawGizmos();
            
            Gizmos.color = _EnteredHurtShapes.Count > 0 ? Color.red : Color.green;
        }
        #endif  

        protected abstract HitEnterData[] GetHitData(Vector3 velocity);
        
    }
}