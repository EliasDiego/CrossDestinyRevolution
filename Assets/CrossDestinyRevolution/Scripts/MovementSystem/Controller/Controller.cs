using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MovementSystem
{
    public class Controller : MonoBehaviour, IController
    {
        [SerializeField]
        Rigidbody _Rigidbody;

        public Rigidbody RigidBody
        {
            get => _Rigidbody;
            set => _Rigidbody = value;
        }

        public Vector3 velocity => _Rigidbody.velocity;

        public virtual void SetVelocity(Vector3 value)
        {
            _Rigidbody.velocity = value;
        }

        public virtual void ClampVelocity(float magnitude)
        {
            _Rigidbody.velocity = Vector3.ClampMagnitude(_Rigidbody.velocity, magnitude);
        }

        public virtual void AddRbForce(Vector3 force, ForceMode mode = ForceMode.VelocityChange)
        {
            _Rigidbody.AddForce(force, mode);
        }

        public virtual void AddRelativeForce(Vector3 force, ForceMode mode = ForceMode.VelocityChange)
        {
            _Rigidbody.AddRelativeForce(force, mode);
        }

        public virtual void Translate(Vector3 direction, float magnitude)
        {
            _Rigidbody.MovePosition(_Rigidbody.position + direction * magnitude);
        }

        public virtual void Rotate(Quaternion rotation)
        {
            _Rigidbody.MoveRotation(rotation);
        }
    }
}

