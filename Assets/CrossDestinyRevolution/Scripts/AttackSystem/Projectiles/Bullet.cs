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
			_rigidBody.velocity = transform.forward * BulletSpeed;
		}

		void RotateBullet()
		{
			_rigidBody.MoveRotation(Quaternion.LookRotation(target.position));
		}
	}
}

