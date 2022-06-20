using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MovementSystem;

namespace CDR.MapSystem
{
    public interface IMap
    {
        IFlightPlane flightPlane { get; }
    }
}