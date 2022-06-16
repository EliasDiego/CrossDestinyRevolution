using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MovementSystem
{
    public class CharacterController : Controller , ICharacterController
    {
        [SerializeField]
        private FlightPlane _flightPlane;


        public IFlightPlane flightPlane 
        { 
            get => _flightPlane; 
            set => _flightPlane = (FlightPlane)value; 
        }
    }
}

