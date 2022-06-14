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

		public Vector3 staticTargetPoint;

		public override void Start()
		{
			base.Start();

			isInPosition = false;
		}

		public override void OnEnable()
		{
			base.OnEnable();

			transform.LookAt(targetPoint);
			isInPosition = false;
		}

		public virtual void FixedUpdate()
		{
			if (target != null)
				distanceFromTarget = Vector3.Distance(transform.position, target.position);

			isInPosition = CheckIfInPosition();

			if (!isInPosition)
			{
				MoveProjectile();
			}
		}

		public virtual void MoveProjectile()
		{
			//_rigidBody.MovePosition(staticTargetPoint);
			//_rigidBody.velocity = transform.forward * bulletSpeed;

			float step = bulletSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetPoint, step);
		}

		bool CheckIfInPosition()
		{
			if ((targetPoint - transform.position).magnitude < 1)
			{
				//transform.position = targetPoint;
				return true;
			}
				

			return false;
		}
	}
}



