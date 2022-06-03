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

        private Boost boost;
        private Controller controller;
        private Vector3 currentDir = Vector3.zero;
        private ITargetData currentTarget;

        public float speed => _speed;

        public float gravity => _gravity;

        protected override void Awake()
        {
            base.Awake();
            controller = GetComponent<Controller>();
            boost = GetComponent<Boost>();
        }

        private void Start()
        {
            currentTarget = Character.targetHandler.GetCurrentTarget();
        }

        private void Update()
        {
            RotateObject();
            if (clampSpeed)
            {
                controller.ClampVelocity(speed);
            }
        }

        private void FixedUpdate()
        {       
            controller.AddRbForce(MoveDirection());           
        }       

        private Vector3 MoveDirection()
        {
            if(currentDir.magnitude == 0f)
            {
                return Vector3.zero;
            }

            var current = (transform.rotation * currentDir).normalized;
            current += transform.forward * 0.35f;
            //current.z -= (transform.position - currentTarget.activeCharacter.position).normalized.z;
            return current;
        }
         
        public void SetClamp(bool boo)
        {
            clampSpeed = boo;
        }

        private void ClampSpeed()
        {
            SetClamp(true);
        }

        private void UnclampSpeed()
        {
            SetClamp(false);
        }

        private void RotateObject()
        {
            currentTarget = Character.targetHandler.GetCurrentTarget();
            var look = Quaternion.LookRotation(-currentTarget.direction);
            var quat = Quaternion.RotateTowards(transform.rotation, look, 50f);
            quat.x = 0f;
            quat.z = 0f;

            controller.Rotate(Quaternion.RotateTowards(transform.rotation, quat, 50f));
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
        }

        public override void End()
        {
            base.End();
        }
        #endregion        
    }
}
