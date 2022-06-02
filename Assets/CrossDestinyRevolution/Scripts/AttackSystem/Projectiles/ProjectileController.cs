using CDR.MovementSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class ProjectileController : MonoBehaviour, IProjectileController
    {
		Rigidbody _rigidBody;

		public Vector3 velocity => _rigidBody.velocity;

		public void Rotate(Quaternion rotation)
		{
			_rigidBody.rotation = rotation;
		}

        public void SetVelocity(Vector3 velocity)
        {
            throw new System.NotImplementedException();
        }

        public void Translate(Vector3 direction, float magnitude)
		{
			_rigidBody.transform.position = direction;
		}

		void Awake()
		{
			_rigidBody = GetComponent<Rigidbody>();
		}

		
    }
}

