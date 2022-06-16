using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
	public class NGProjectile : Projectile
	{
		[SerializeField] public float bulletSpeed = 15f;

		public Vector3 targetPoint;

		public Quaternion targetPlayerDir;

		public bool isInPosition;

		public bool secondPhaseStart = false;

		public override void Start()
		{
			base.Start();

			
		}

		public override void OnEnable()
		{
			base.OnEnable();

			transform.LookAt(targetPoint);

			isInPosition = false;
			secondPhaseStart = false;
		}

		public virtual void FixedUpdate()
		{
			if (target != null)
				distanceFromTarget = Vector3.Distance(transform.position, target.position);


			if(!isInPosition)
			{
				isInPosition = CheckIfInPosition();
			}
			

			if (!isInPosition)
			{
				MoveProjectile();
			}

			if(secondPhaseStart)
			{
				MoveToPlayer();
			}
		}

		public virtual void MoveProjectile()
		{
			float step = bulletSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetPoint, step);
		}

		void MoveToPlayer()
		{
			Rotate(targetPlayerDir);

			float step = bulletSpeed * Time.deltaTime;
			SetVelocity(transform.forward * bulletSpeed);
		}

		protected override void OnHitEnter(IHitEnterData hitData)
		{
			base.OnHitEnter(hitData);
		}

		public override void ResetObject() //Parameters reset
		{
			base.ResetObject();


			

			isInPosition = false;
			secondPhaseStart = false;
			targetPlayerDir = Quaternion.identity;
			targetPoint = Vector3.zero;
		}

		bool CheckIfInPosition()
		{
			if ((targetPoint - transform.position).magnitude < 1)
			{
				return true;
			}
				

			return false;
		}
	}
}



