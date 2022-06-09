using System.Collections;
using System.Collections.Generic;
using CDR.MechSystem;
using UnityEngine;

namespace CDR.VersusSystem
{
    public class AIParticipant : Participant
    {
        public AIParticipant(IMech mech, Vector3 startPosition, Quaternion startRotation) : base(mech, startPosition, startRotation)
        {
            
        }
    }
}