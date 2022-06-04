using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.StateSystem
{
    public class Stun : State, IStun
    {
        [SerializeField] float _duration;

        public float duration => _duration;

        IEnumerator StartStun(float duration)
        {
            StartState();

            yield return new WaitForSeconds(duration);

            EndState();
        }
    }
}
