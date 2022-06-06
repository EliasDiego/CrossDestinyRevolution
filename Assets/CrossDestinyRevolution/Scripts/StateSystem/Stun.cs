using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.StateSystem
{
    public class Stun : State, IStun
    {
        [SerializeField] float _duration;

        public float duration => _duration;

        public override void StartState()
        {
            base.StartState();
            Debug.Log("Start Stun");
            
            StartCoroutine(StunCoroutine(duration));
        }

        public override void EndState()
        {
            base.EndState();
            Debug.Log("End Stun");
        }

        IEnumerator StunCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            EndState();
        }
    }
}
