using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class BBBProjectile : Projectile
    {
		[SerializeField] public float bulletSpeed = 15f;

		public Quaternion generalDirection;

		public Vector3 towardsSplitPoint;

		public bool isInSplitPoint;

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
			transform.rotation = generalDirection;

			MoveProjectile();
		}

		public virtual void MoveProjectile()
		{
			SetVelocity(transform.forward * bulletSpeed);
		}
	}
}


