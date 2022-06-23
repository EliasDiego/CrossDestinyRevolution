using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MovementSystem;

namespace CDR.MapSystem
{
    public class Map : MonoBehaviour, IMap
    {
        [SerializeField]
        FlightPlane _FlightPlane;

        public IFlightPlane flightPlane => _FlightPlane;
    }
}