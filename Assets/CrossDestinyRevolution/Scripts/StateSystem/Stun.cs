using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.StateSystem
{
    public class Stun : State, IStun
    {
        float _time;

        public float time => _time;

        
    }
}
