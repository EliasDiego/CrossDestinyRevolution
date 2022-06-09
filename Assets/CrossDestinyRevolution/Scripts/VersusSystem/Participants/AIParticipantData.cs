using System.Collections;
using System.Collections.Generic;
using CDR.MovementSystem;
using UnityEngine;

namespace CDR.VersusSystem
{
    public class AIParticipantData : ParticipantData
    {
        public override IParticipant GetParticipant(Vector3 startPosition, Quaternion startRotation, IFlightPlane flightPlane)
        {
            IParticipant participant = base.GetParticipant(startPosition, startRotation, flightPlane);

            return new AIParticipant(participant.mech, startPosition, startRotation);
        }
    }
}