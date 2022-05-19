using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MechSystem;

namespace CDR.TargetingSystem
{
    public class TargetData : MonoBehaviour
    {
        ActiveCharacter activeCharacter;

        float dist;
        Vector3 dir;

        public float Distance => dist;
        public Vector3 Direction => dir;

        private void Start() {
            dist = Vector3.Distance(this.transform.position, activeCharacter.transform.position);
            dir = (this.transform.position - activeCharacter.transform.position).normalized;
        }
    }
}