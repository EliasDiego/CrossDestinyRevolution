using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
	public class Bullet : Projectile
	{
		[SerializeField] float BulletSpeed;
		public Transform TargetDirection;

		public override void Update()
		{
			base.Update();
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

