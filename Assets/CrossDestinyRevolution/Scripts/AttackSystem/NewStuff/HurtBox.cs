using System;
using System.Collections;
using System.Collections.Generic;
using CDR.MechSystem;
using UnityEngine;

namespace CDR.AttackSystem.New
{
    [RequireComponent(typeof(BoxCollider))]
    public class HurtBox : MonoBehaviour, IHurtShape
    {
        [SerializeField]
        ActiveCharacter _ActiveCharacter;

        BoxCollider _BoxCollider;

        public IActiveCharacter character => _ActiveCharacter;

        public Vector3 position => transform.position + _BoxCollider.center;

        public event Action<IHitEnterData> onHitEnter;
        public event Action<IHitExitData> onHitExit;

        private void Awake() 
        {
            _BoxCollider = GetComponent<BoxCollider>();    
        }

        public void HitEnter(IHitEnterData hitData)
        {
            onHitEnter?.Invoke(hitData);
        }

        public void HitExit(IHitExitData hitData)
        {
            onHitExit?.Invoke(hitData);
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos() 
        {
            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.color = Color.yellow;

            if(!UnityEditor.EditorApplication.isPlaying && !_BoxCollider)
                _BoxCollider = GetComponent<BoxCollider>();    

            Gizmos.DrawCube(_BoxCollider.center, _BoxCollider.size);  
        }
        #endif  
    }
}