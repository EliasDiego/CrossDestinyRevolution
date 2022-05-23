using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
	public class Bullet : Projectile
	{
		[SerializeField] public float BulletSpeed;
		Rigidbody _rigidBody;
		public Vector3 direction;

		public override void Start()
		{
			base.Start();

			_rigidBody = GetComponent<Rigidbody>();
			
		}

		private void FixedUpdate()
		{
			MoveBullet();
			//RotateBullet();
		}

		void MoveBullet()
		{
			//_rigidBody.velocity = transform.forward * BulletSpeed;
			Debug.Log(direction);
			_rigidBody.velocity = direction * BulletSpeed;
		}

		void RotateBullet()
		{
			//_rigidBody.MoveRotation(Quaternion.LookRotation(projectileTarget));
		}




		private void OnCollisionEnter(Collision collision)
		{
			//Check for Collided Character damage function and deal damage
			if (collision == null)
			{
				//collision.GetComponent<>();
			}

		}
	}
}

