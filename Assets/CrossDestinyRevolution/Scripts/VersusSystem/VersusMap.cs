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
        Vector3 _Player1Position;
        [SerializeField]
        Vector3 _Player2Position;
        
        public Vector3 player1Position => transform.position + _Player1Position;
        public Vector3 player2Position => transform.position + _Player2Position;

        private void OnDrawGizmos() 
        {
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_Player1Position, 1);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_Player2Position, 1);    
        }
    }
}