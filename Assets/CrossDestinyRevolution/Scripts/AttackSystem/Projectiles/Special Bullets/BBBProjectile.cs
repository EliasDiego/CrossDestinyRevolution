using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class BBBProjectile : Projectile
    {
		[SerializeField] public float bulletSpeed = 15f;

		[SerializeField] float bulletSplitSpeed = 2f;

		public Quaternion generalDirection;

		public Vector3 towardsSplitPoint;

		public bool isInSplitPoint;

		public float magnitude;

		public override void Start()
		{
			base.Start();
		}

		public override void OnEnable()
		{
			base.OnEnable();

			transform.rotation = generalDirection;
		}


		public virtual void FixedUpdate()
		{
			MoveProjectile();

			isInSplitPoint = CheckIfInSplitPos();
		}

		public virtual void MoveProjectile()
		{
			SetVelocity(transform.forward * bulletSpeed);

			// (isInSplitPoint)
			//{
			//	towardsSplitPoint = transform.position;	
			//}

			if (!isInSplitPoint)
			{
				transform.position = Vector3.MoveTowards(transform.position, towardsSplitPoint, bulletSplitSpeed * Time.deltaTime);
			}
		}

		protected override void ProcessLifetime()
		{
			if (hasLifeTime)
			{
				float deltaTime = Time.deltaTime;

				if (LifetimeCountDown(deltaTime))
				{
					ResetObject();

					projectileHitBox.onHitEnter -= OnHitEnter;

					Return();
				}
			}
		}

		protected override void OnHitEnter(IHitData hitData)
		{
			base.OnHitEnter(hitData);
		}


		bool CheckIfInSplitPos()
		{
			magnitude = (towardsSplitPoint - transform.position).magnitude;

			if (magnitude < 1f)
			{
				transform.position = towardsSplitPoint;
				return true;
			}
			return false;
		}

		public override void ResetObject() //Parameters reset
		{
			base.ResetObject();

			hasLifeTime = true;
			towardsSplitPoint = Vector3.zero;
			isInSplitPoint = false;
			generalDirection = Quaternion.identity;
		}
	}
}


