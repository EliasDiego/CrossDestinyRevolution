using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.InputSystem;
using CDR.MovementSystem;

namespace CDR.VersusSystem
{
    public class AIParticipantData : ParticipantData
    {
        public AIMechInputSettings settings { get; set; }

        public override IParticipant GetParticipant(Vector3 startPosition, Quaternion startRotation, IFlightPlane flightPlane)
        {
            name = mechData.mechName + " (AI)";

            IParticipant participant = base.GetParticipant(startPosition, startRotation, flightPlane);

            AIMechInput input = InputUtilities.AssignAIInput<AIMechInput>((participant.mech as Mech).gameObject);

            input.settings = settings;

            participant.mech.input = input;

            return new AIParticipant(participant.name, participant.mech, startPosition, startRotation);
        }
    }
}