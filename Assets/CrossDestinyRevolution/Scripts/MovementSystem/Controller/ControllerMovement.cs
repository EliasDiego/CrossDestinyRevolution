using CDR.MechSystem;
using System;
using UnityEngine;

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

        [SerializeField]
        private bool clampSpeed = false;

        protected override void Awake()
        {
            base.Awake();
            controller = GetComponent<Controller>();
        }

        public float speed => _speed;

        public float gravity => _gravity;

        public override void End()
        {
            base.End();
        }

        private void Update()
        {
            controller.MoveRb(currentDir);
            if (clampSpeed)
            {
                controller.ClampVelocity(speed);
            }
        }

        public void SetClamp(bool boo)
        {
            clampSpeed = boo;
        }

        public void Move(Vector2 direction)
        {
            currentDir = new Vector3(direction.x, 0f, direction.y);     
        }

        public override void Use()
        {
            base.Use();
        }
    }
}
