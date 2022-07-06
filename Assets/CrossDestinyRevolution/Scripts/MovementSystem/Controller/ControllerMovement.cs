using CDR.MechSystem;
using System;
using UnityEngine;
using CDR.TargetingSystem;
using CDR.AnimationSystem;

// This class handles movement of a controller through a rigidbody.

namespace CDR.MovementSystem
{
    public class ControllerMovement : ActionSystem.Action, IMovement
    {
        [SerializeField]
        private float leanTime = 0.2f;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _gravity;
        [SerializeField]
        private bool clampSpeed = false;
        [SerializeField]
        private SFXAnimationEvent[] sfx;

        private Vector3 currentDir = Vector3.zero;
        private ITargetData currentTarget;
        private float distanceToTarget;

        public float speed => _speed;
        public float gravity => _gravity;

        protected override void Awake()
        {
            base.Awake();
            Character.animator.GetComponent<AnimationEventsManager>().AddAnimationEvent("Move");
            Character.animator.GetComponent<AnimationEventsManager>().AddAnimationEvent("Move", sfx);

        }

        private void Start()
        {
            Character.controller.flightPlane.AddCharacterController((CharacterController)Character.controller);
            
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

        private Vector3 CentripetalForce()
        {
            float cForce = Mathf.Pow(Character.controller.velocity.magnitude, 2) / distanceToTarget;
            var force = currentTarget.direction * -cForce;
            force.y = 0f;
            return force;
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
            current.y = 0f;
            return current;           
        }

        private void RotateObject()
        {
            currentTarget = Character.targetHandler.GetCurrentTarget();
            var look = Quaternion.LookRotation(-currentTarget.direction);
            var quat = Quaternion.RotateTowards(Character.rotation, look, 50f);
            quat.x = 0f;
            quat.z = 0f;

            Character.controller.Rotate(Quaternion.RotateTowards(Character.rotation, quat.normalized, 50f));
        }

        #region INTERFACE_Methods
        public void Move(Vector2 direction)
        {
            var dir = new Vector3(direction.x, 0f, direction.y);
            currentDir = dir;
          
            LeanTween.value(Character.animator.GetFloat("MoveX"), direction.x, leanTime).setOnUpdate((float f) =>
            {
                Character.animator.SetFloat("MoveX", f);
            });
            LeanTween.value(Character.animator.GetFloat("MoveY"), direction.y, leanTime).setOnUpdate((float f) =>
            {
                Character.animator.SetFloat("MoveY", f);
            });

            if (direction.magnitude == 0f)
            {
                //Character.animator.SetInteger("MoveType", (int)MoveType.None);
                currentDir = Vector3.zero;
            }
        }

        public override void Use()
        {
            base.Use();
            currentTarget = Character.targetHandler.GetCurrentTarget();
            distanceToTarget = Vector3.Distance(Character.position, currentTarget.activeCharacter.position);
            Character.animator.SetInteger("MoveType", (int)MoveType.Movement);
        }

        public override void End()
        {
            base.End();
            Character.animator.SetInteger("MoveType", (int)MoveType.None);
        }

        public override void ForceEnd()
        {
            base.ForceEnd();
            currentDir = Vector3.zero;
            Character.controller.SetVelocity(Vector3.zero);
            Character.animator.SetFloat("MoveX", 0f);
            Character.animator.SetFloat("MoveY", 0f);
            Character.animator.SetInteger("MoveType", (int)MoveType.None);
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
