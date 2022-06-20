using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.MovementSystem;

namespace CDR.VersusSystem
{
    public interface ICameraParticipant : IParticipant
    {
        Camera camera { get; }
    }

    public interface IParticipant
    {
        string name { get; }
        int score { get; set; }
        
        IMech mech { get; }

        void Start();
        void Reset();
    }

    public interface IParticipantData
    { 
        IMechData mechData { get; set; }
        IParticipant GetParticipant(Vector3 startPosition, Quaternion startRotation, IFlightPlane flightPlane);
    }
}