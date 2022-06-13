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

        protected struct HitData
        {
            public IHurtShape hurtShape  { get; set; }
            public RaycastHit hit { get; set; }
        }

        private void FixedUpdate() 
        {
            Vector3 velocity = _Controller?.velocity ?? _MinimumVelocity;

            HitData[] hitDatas = GetHitData(velocity);

            IEnumerable<HitData> sortedHitDatas = GetHitData(velocity).Where(h => h.hurtShape.character == null && character == null || h.hurtShape.character != character);

            IHurtShape[] exitedHurtShapes = _EnteredHurtShapes?.Except(sortedHitDatas?.Select(h => h.hurtShape))?.ToArray();
            HitData[] enteredHitData = sortedHitDatas?.Where(h => !_EnteredHurtShapes.Contains(h.hurtShape)).ToArray();//?.Except(_EnteredHurtShapes)?.ToArray();

            foreach(IHurtShape h in exitedHurtShapes)
            {
                HitExitData data = new HitExitData(this, h);

                h.HitExit(data);

                onHitExit?.Invoke(data);

                _EnteredHurtShapes.Remove(h);
            }

            foreach(HitData h in enteredHitData)
            {
                HitEnterData data = new HitEnterData(this, h.hurtShape, h.hit);

                h.hurtShape.HitEnter(data);

                onHitEnter?.Invoke(data);

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

        protected abstract HitData[] GetHitData(Vector3 velocity);
        
    }
}