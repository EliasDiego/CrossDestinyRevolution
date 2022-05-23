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

        private Controller controller;
        private Vector3 currentDir = Vector3.zero;
        private Vector3 targetDiection;

        [SerializeField]
        private bool clampSpeed = false;

        protected override void Awake()
        {
            base.Awake();
            controller = GetComponent<Controller>();
        }

        private void Start()
        {
            ITargetData targetData = Character.targetHandler.GetCurrentTarget();
            targetDiection = targetData.activeCharacter.position - transform.position;
        }

        public float speed => _speed;

        public float gravity => _gravity;

        public override void End()
        {
            base.End();
        }

        private void Update()
        {
            RotateObject();

            controller.MoveRb(transform.rotation * currentDir);

            if (clampSpeed)
            {
                controller.ClampVelocity(speed);
            }
        }

        private void RotateObject()
        {
            ITargetData targetData = Character.targetHandler.GetCurrentTarget();

            var look = Quaternion.LookRotation(-targetData.direction);
            var quat = Quaternion.RotateTowards(transform.rotation, look, 50f);
            quat.x = 0f;
            quat.z = 0f;

            controller.Rotate(Quaternion.RotateTowards(transform.rotation, quat, 50f));
        }

        public void SetClamp(bool boo)
        {
            clampSpeed = boo;
        }

        public void Move(Vector2 direction)
        {
            var dir = new Vector3(direction.x, 0f, direction.y);
            // dir = transform.rotation * dir;
            currentDir = dir;
        }

        public override void Use()
        {
            base.Use();
        }
    }
}
