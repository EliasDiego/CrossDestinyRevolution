using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MovementSystem
{
    public class ControllerMovement : ActionSystem.Action
    {
        [SerializeField]
        private float maxSpeed = 15f;

        private Rigidbody rb;

        private bool clampSpeed = true;

        Vector3 currentDirection = Vector3.zero;

        private void Start()
        {
            Use();
        }

        private void FixedUpdate()
        {
            if (isActive)
            {
                //Move(Vector3.zero);
                //MoveRB();
                OldMove();
            }
        }

        public override void Use()
        {
            base.Use();
        }

        public override void End()
        {
            base.End();
        }

        public void SetRigidbody(Rigidbody rigid)
        {
            rb = rigid;
        }

        // Use this method for moving with new input system.
        public void Move(Vector3 direction)
        {
            currentDirection = direction;            
        }

        private void OldMove()
        {
            currentDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            MoveRB();
        }

        private void MoveRB()
        {
            rb.AddForce(currentDirection, ForceMode.VelocityChange);

            if(clampSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
        }

        public void ClampSpeed()
        {
            clampSpeed = true;
        }

        public void UnclampSpeed()
        {
            clampSpeed = false;
        }
    }
}
