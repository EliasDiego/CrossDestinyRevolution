using System.Collections;
using System.Collections.Generic;
using CDR.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

using CDR.MechSystem;
using CDR.MovementSystem;

namespace CDR.VersusSystem
{
    public abstract class ParticipantData : IParticipantData
    {
        public IMechData mechData { get; set; }

        public virtual IParticipant GetParticipant(Vector3 startPosition, Quaternion startRotation, IFlightPlane flightPlane)
        {
            IMech mech = GameObject.Instantiate(mechData.mechPrefab, startPosition, startRotation).GetComponent<IMech>();

            mech.controller.flightPlane = flightPlane;

            return new Participant(mech, startPosition, startRotation);
        }
    }
}