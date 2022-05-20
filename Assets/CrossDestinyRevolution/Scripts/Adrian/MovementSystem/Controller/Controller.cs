using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MovementSystem
{
    public class Controller : MonoBehaviour
    {
        public ControllerMovement Movement { get; private set; }
        public Boost Boost { get; private set; }

        private Rigidbody rb;

        public Vector3 Velocity
        {
            get { return rb.velocity; }
            private set { }
        }

     
        private void Awake()
        {
            Movement = GetComponent<ControllerMovement>();
            Boost = GetComponent<Boost>();
            rb = GetComponent<Rigidbody>();

            Boost.SetRigidbody(rb);
            Movement.SetRigidbody(rb);
        }

        private void OnEnable()
        {
            Boost.OnBoostUse += Movement.UnclampSpeed;
            Boost.OnBoostEnd += Movement.ClampSpeed;
        }

        private void OnDisable()
        {
            Boost.OnBoostUse -= Movement.UnclampSpeed;
            Boost.OnBoostEnd -= Movement.ClampSpeed;          
        }

        public void SetVelocity(Vector3 value)
        {
            Velocity = value;
            rb.velocity = value;
        }

        

    }
}

