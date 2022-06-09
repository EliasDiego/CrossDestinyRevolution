using CDR.MechSystem;
using System;
using UnityEngine;
using CDR.TargetingSystem;

// This class handles movement of a controller through a rigidbody.

namespace CDR.MovementSystem
{
    public class ControllerMovement : ActionSystem.Action, IMovement
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _gravity;
        [SerializeField]
        private bool clampSpeed = false;

        private Vector3 currentDir = Vector3.zero;
        private ITargetData currentTarget;
        private float distanceToTarget;

        public float intensity = 1f;

        public float speed => _speed;
        public float gravity => _gravity;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            if(!isActive)
            {
                return;
            }
            if (clampSpeed)
            {
                Character.controller.ClampVelocity(speed);
            }
            RotateObject();
        }

        private void FixedUpdate()
        {       
            if(!isActive)
            {
                return;
            }
            Character.controller.AddRbForce(MoveDirection());
            Character.controller.AddRbForce(CentripetalForce(), ForceMode.Acceleration);
        }       

        Vector3 CentripetalForce()
        {
            float cForce = Mathf.Pow(Character.controller.velocity.magnitude, 2) / distanceToTarget;
            return currentTarget.direction * -cForce;
        }

        private Vector3 MoveDirection()
        {
            if(currentDir.magnitude == 0f)
            {
                return Vector3.zero;
            }

            if(currentDir.z != 0f)
            {
                var distance = Vector3.Distance(Character.position, currentTarget.activeCharacter.position);
                SetDistanceToTarget(distance);
                currentTarget?.activeCharacter?.movement?.SetDistanceToTarget(distance);
            }


            var current = (Character.rotation * currentDir).normalized;
            return current;           
        }

        private void RotateObject()
        {
            currentTarget = Character.targetHandler.GetCurrentTarget();
            var look = Quaternion.LookRotation(-currentTarget.direction);
            var quat = Quaternion.RotateTowards(Character.rotation, look, 50f);
            quat.x = 0f;
            quat.z = 0f;

            Character.controller.Rotate(Quaternion.RotateTowards(Character.rotation, quat, 50f));
        }

        #region INTERFACE_Methods
        public void Move(Vector2 direction)
        {
            var dir = new Vector3(direction.x, 0f, direction.y);
            currentDir = dir;    
            
            if(direction.magnitude == 0f)
            {
                currentDir = Vector3.zero;
            }
        }

        public override void Use()
        {
            base.Use();
            currentTarget = Character.targetHandler.GetCurrentTarget();
            distanceToTarget = Vector3.Distance(Character.position, currentTarget.activeCharacter.position);
        }

        public override void End()
        {
            base.End();
        }

        public void SetSpeedClamp(bool isClamped)
        {
            clampSpeed = isClamped;
        }

        public void SetDistanceToTarget(float distance)
        {
            distanceToTarget = distance;            
        }
        #endregion
    }
}
