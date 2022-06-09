using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
	public class NGProjectile : Projectile
	{
		[SerializeField] public float bulletSpeed = 15f;

		public Vector3 targetPoint;

		public bool isInPosition;

		public override void Start()
		{
			base.Start();
		}

		public override void OnEnable()
		{
			base.OnEnable();

			transform.LookAt(targetPoint);
		}

		public virtual void FixedUpdate()
		{
			if (target != null)
				distanceFromTarget = Vector3.Distance(transform.position, target.position);

			if (!isInPosition)
			{
				MoveProjectile();
				isInPosition = CheckIfInPosition();
			}
		}

		public virtual void MoveProjectile()
		{
			_rigidBody.velocity = transform.forward * bulletSpeed;
		}

		bool CheckIfInPosition()
		{
			if (transform.position == targetPoint)
				return true;

			return false;
		}
	}
}



