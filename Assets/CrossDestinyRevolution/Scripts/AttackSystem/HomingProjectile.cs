using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;
using CDR.ActionSystem;

namespace CDR.AttackSystem
{
    public class HomingProjectile : Projectile
    {
        [SerializeField] public float bulletSpeed;
		[SerializeField] public float rotateSpeed;

		protected Rigidbody _rigidBody;
		protected bool isHoming = true;

		public IActiveCharacter target { get; set; }
		

		public override void Start()
		{
			base.Start();
			_rigidBody = GetComponent<Rigidbody>();
		}

		public virtual void FixedUpdate()
		{
			MoveProjectile();
		}

		public virtual void MoveProjectile()
		{
			_rigidBody.velocity = transform.forward * bulletSpeed;
		}

		public virtual void RotateProjectile()
		{
			//_rigidBody.MoveRotation(Quaternion.LookRotation(projectileTarget));
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

