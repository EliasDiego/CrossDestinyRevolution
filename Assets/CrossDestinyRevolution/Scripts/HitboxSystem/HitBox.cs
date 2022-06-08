using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;

namespace CDR.AttackSystem
{
    public class HitBox : MonoBehaviour, IHitShape
    {
        [SerializeField]
        private ActiveCharacter _ActiveCharacter;
        [SerializeField]
        Bounds _Bounds;
        [SerializeField]
        private LayerMask _HitLayer;

        private List<IHurtShape> _EnteredHurtShapes = new List<IHurtShape>();

        public IActiveCharacter character => _ActiveCharacter;

        public Vector3 position => transform.position + _Bounds.center;

        public event Action<IHitEnterData> onHitEnter;
        public event Action<IHitExitData> onHitExit;

        private void FixedUpdate() 
        {
            IEnumerable<IHurtShape> currentHurtShapes = Physics.OverlapBox(position, _Bounds.extents / 2, transform.rotation, _HitLayer)?.Where(c => c.TryGetComponent(out IHurtShape h))?.
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
                if(Physics.Raycast(position, (h.position - position).normalized, out RaycastHit hit, float.MaxValue, _HitLayer))
                {
                    HitEnterData data = new HitEnterData(this, h, hit);

                    h.HitEnter(data);

                    onHitEnter?.Invoke(data);
 
                    _EnteredHurtShapes.Add(h);
                }
            }
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos() 
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = _EnteredHurtShapes?.Count <= 0 ? Color.green : Color.red;
            
            Gizmos.DrawCube(_Bounds.center, _Bounds.extents);    
        }
        #endif  
    }
}