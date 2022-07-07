using System.Collections;
using System.Collections.Generic;
using CDR.MechSystem;
using UnityEngine;

namespace CDR.VersusSystem
{
    public class AIParticipant : Participant
    {
        public AIParticipant(string name, IMech mech, Vector3 startPosition, Quaternion startRotation) : base(name, mech, startPosition, startRotation)
        {
            
        }
    }
}