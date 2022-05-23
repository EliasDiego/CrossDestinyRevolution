using System;
using UnityEngine;

// This class handles FlightPlane methods and events for resize, move, and rotate.

namespace CDR.MovementSystem
{
    public class FlightPlane : MonoBehaviour, IFlightPlane
    {
        Vector3 IFlightPlane.position => transform.position;

        Quaternion IFlightPlane.rotation => transform.rotation;
    }
}

