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
        public string name { get; set; }
        public IMechData mechData { get; set; }

        public virtual IParticipant GetParticipant(Vector3 startPosition, Quaternion startRotation, IFlightPlane flightPlane)
        {
            GameObject mechObject = GameObject.Instantiate(mechData.mechPrefab, startPosition, startRotation); 
            IMech mech = mechObject.GetComponent<IMech>();

            mechObject.name = name;
            
            mech.controller.flightPlane = flightPlane;

            return new Participant(name, mech, startPosition, startRotation);
        }
    }
}