using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MovementSystem
{
    public class Controller : MonoBehaviour , ICharacterController
    {
        [SerializeField]
        private FlightPlane _flightPlane;

        private Rigidbody rb;

        public Vector3 velocity => rb.velocity;

        public IFlightPlane flightPlane 
        { 
            get => _flightPlane; 
            set => _flightPlane = (FlightPlane)value; 
        }

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

        public void AddRbForce(Vector3 force, ForceMode mode = ForceMode.VelocityChange)
        {
            rb.AddForce(force, mode);
        }

        public void AddRelativeForce(Vector3 force, ForceMode mode = ForceMode.VelocityChange)
        {
            rb.AddRelativeForce(force, mode);
        }

        public void Translate(Vector3 direction, float magnitude)
        {
            transform.position = direction * magnitude;
        }

        public void Rotate(Quaternion rotation)
        {
            transform.rotation = rotation;           
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                rb.isKinematic = true;
            }
            rb.isKinematic = false;
        }

    }
}

