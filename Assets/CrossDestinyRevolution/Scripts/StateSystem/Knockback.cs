using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.StateSystem
{
    public class Knockback : State, IKnockback
    {
        float _distance;
        float _time;

        public float distance => _distance;

        public float time => _time;

        private void Update()
        {
            while(time > 0)
            {
                
            }
        }
    }
}

