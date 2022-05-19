using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MovementSystem
{
    public class RbMovement : Movement
    {
        private Rigidbody rb;
        private bool ClampSpeed = true;

        public RbMovement(float speed, Transform trans, Rigidbody rigid) : base(speed, trans)
        {
            rb = rigid;
        }

        public override void Move(Vector3 direction)
        {
            rb.AddForce(direction, ForceMode.VelocityChange);

            if(ClampSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, Speed);
            }
        }
        public override bool IsMoving()
        {
            return Mathf.Round(rb.velocity.magnitude) != 0f;
        }

        public void EnableClamp()
        {
            ClampSpeed = true;
        }

        public void DisableClamp()
        {
            ClampSpeed = false;
        }
    }
}
