using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MovementSystem
{
    public class Controller : MonoBehaviour , IController
    {
        private Rigidbody rb;

        public Vector3 Velocity
        {
            get { return rb.velocity; }
            private set { }
        }

        public IFlightPlane flightPlane => throw new System.NotImplementedException();

        public Vector3 velocity => throw new System.NotImplementedException();

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void SetVelocity(Vector3 value)
        {
            Velocity = value;
            rb.velocity = value;
        }

        public void Translate(Vector3 direction, float magnitude)
        {
            throw new System.NotImplementedException();
        }

        public void Rotate(Quaternion rotation)
        {
            throw new System.NotImplementedException();
        }
    }
}

