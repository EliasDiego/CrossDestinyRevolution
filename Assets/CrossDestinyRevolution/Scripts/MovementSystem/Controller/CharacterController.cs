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

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                RigidBody.isKinematic = true;
            }
            RigidBody.isKinematic = false;
        }
    }
}

