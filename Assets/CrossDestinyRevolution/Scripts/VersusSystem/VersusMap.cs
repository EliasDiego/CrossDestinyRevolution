using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MapSystem;
using CDR.MovementSystem;

namespace CDR.VersusSystem
{
    public class VersusMap : Map, IVersusMap
    {
        [SerializeField]
        Vector3[] _ParticipantPositions;

        public Vector3[] participantPositions => _ParticipantPositions;

        private void OnDrawGizmos() 
        {
            if(_ParticipantPositions.Length <= 0)
                return;
                
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            Gizmos.color = Color.grey;

            for(int i = 0; i < _ParticipantPositions.Length; i++)
                Gizmos.DrawSphere(_ParticipantPositions[i], 1);
        }
    }
}