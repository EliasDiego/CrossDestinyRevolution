using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MovementSystem
{
    public class Controller : MonoBehaviour, IController
    {
        [SerializeField]
        Rigidbody _Rigidbody;

        public Vector3 velocity => _Rigidbody.velocity;

        public void SetVelocity(Vector3 value)
        {
            _Rigidbody.velocity = value;
        }

        public void ClampVelocity(float magnitude)
        {
            _Rigidbody.velocity = Vector3.ClampMagnitude(_Rigidbody.velocity, magnitude);
        }

        public void AddRbForce(Vector3 force, ForceMode mode = ForceMode.VelocityChange)
        {
            _Rigidbody.AddForce(force, mode);
        }

        public void AddRelativeForce(Vector3 force, ForceMode mode = ForceMode.VelocityChange)
        {
            _Rigidbody.AddRelativeForce(force, mode);
        }

        public void Translate(Vector3 direction, float magnitude)
        {
            _Rigidbody.MovePosition(_Rigidbody.position + direction * magnitude);
        }

        public void Rotate(Quaternion rotation)
        {
            _Rigidbody.MoveRotation(rotation);
        }
    }
}

