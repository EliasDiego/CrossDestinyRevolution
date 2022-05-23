using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MovementSystem
{
    public class Controller : MonoBehaviour , IController
    {
        [SerializeField]
        private FlightPlane _flightPlane;

        private Rigidbody rb;

        public Vector3 Velocity
        {
            get { return rb.velocity; }
        }

        public IFlightPlane flightPlane => _flightPlane;

        public Vector3 velocity => throw new System.NotImplementedException();

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void SetVelocity(Vector3 value)
        {
            rb.velocity = value;
        }

        public void ClampVelocity(float magnitude)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, magnitude);
        }

        public void MoveRb(Vector3 force)
        {
            rb.AddForce(force, ForceMode.VelocityChange);
        }

        public void Translate(Vector3 direction, float magnitude)
        {
            transform.Translate(direction * magnitude);
        }

        public void Rotate(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
    }
}

