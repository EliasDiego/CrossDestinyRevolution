using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
	public class Bullet : Projectile
	{
		[SerializeField] public float BulletSpeed;

		public override void Start()
		{
			base.Start();
		}

		private void FixedUpdate()
		{
			MoveBullet();
			RotateBullet();
		}

		void MoveBullet()
		{
			SetVelocity(transform.forward * BulletSpeed);
			//_rigidBody.velocity = transform.forward * BulletSpeed;
		}

		void RotateBullet()
		{
			Rotate(Quaternion.LookRotation(target.position));
			//_rigidBody.MoveRotation(Quaternion.LookRotation(target.position));
		}
	}
}

