using System;
using System.Collections.Generic;
using UnityEngine;

// This class handles FlightPlane methods and events for resize, move, and rotate.

namespace CDR.MovementSystem
{
    public class FlightPlane : MonoBehaviour, IFlightPlane
    {
        private List<CharacterController>_activeControllers = new List<CharacterController>();

        public List<CharacterController> activeControllers => _activeControllers;

        Vector3 IFlightPlane.position => transform.position;
        Quaternion IFlightPlane.rotation => transform.rotation;

        public void AddCharacterController(CharacterController controller)
        {
            if(_activeControllers.Contains(controller))
            {
                _activeControllers.Add(controller);
            }
        }
    }
}

