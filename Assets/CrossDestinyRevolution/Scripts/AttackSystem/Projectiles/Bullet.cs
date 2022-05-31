using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
	public class Bullet : Projectile
	{
		[SerializeField] public float BulletSpeed;
		Rigidbody _rigidBody;

		public override void Start()
		{
			base.Start();

			_rigidBody = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			MoveBullet();
			RotateBullet();
		}

		void MoveBullet()
		{
			_rigidBody.velocity = transform.forward * BulletSpeed;
		}

		void RotateBullet()
		{
			//_rigidBody.MoveRotation(Quaternion.LookRotation(target));
		}

		protected override void OnCollisionEnter(Collision other)
		{
			base.OnCollisionEnter(other);

		}
	}
}

