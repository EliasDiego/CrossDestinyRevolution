using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

namespace CDR.AttackSystem
{
    public class HomingProjectile : Projectile
    {
        [SerializeField] public float bulletSpeed = 15f;
		[SerializeField] public float rotateSpeed = 5f;

		protected bool isHoming = true;

		public override void Start()
		{
			base.Start();
		}

		public override void OnEnable()
		{
			base.OnEnable();
			isHoming = true;

			if (target != null)
			{
				transform.LookAt(target.position);
			}

		}

		public virtual void FixedUpdate()
		{
			if(target != null)
				distanceFromTarget = Vector3.Distance(transform.position, target.position);

			MoveProjectile();
		}

		public virtual void MoveProjectile()
		{
			SetVelocity(transform.forward * bulletSpeed);
			//_rigidBody.velocity = transform.forward * bulletSpeed;
		}

		public virtual void RotateProjectile(){}

		
	}
}

