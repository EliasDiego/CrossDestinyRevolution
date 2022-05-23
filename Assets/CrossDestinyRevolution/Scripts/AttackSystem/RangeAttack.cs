using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;

namespace CDR.AttackSystem
{
	public class RangeAttack : CooldownAction , IRangeAttack
	{
		[SerializeField] float FireRate;

		[SerializeField] GameObject GunPoint; //Invisible gameobject for the location of the gun barrel
		[SerializeField] public Transform TargetPoint; //Predictive Targeting system

		[SerializeField] GameObject BulletProjectile;

		public IProjectile projectile => throw new System.NotImplementedException();

		public float range => throw new System.NotImplementedException();

		private void Start()
		{
			_cooldownDuration = FireRate;
		}

		public override void Update()
		{
			base.Update();
			
			BulletProjectile.GetComponent<Projectile>().projectileTarget = SetPredictiveTarget();
			BulletProjectile.GetComponent<Projectile>().projectileOriginPoint = GunPoint.transform.position;
		}

		public override void Use()
		{
			base.Use();

			Instantiate(BulletProjectile, GunPoint.transform.position, Quaternion.identity);

			End();
		}

		public override void End()
		{
			base.End();
		}

		Vector3 SetPredictiveTarget()
		{
			var currentTarget = Character.targetHandler.GetCurrentTarget();
			var targetVelocity = currentTarget.activeCharacter.controller.velocity;
			
			//var currentTarget = TargetPoint;
			//var targetVelocity = TargetPoint.GetComponent<TestVelocity>();

			if (targetVelocity != Vector3.zero) //Adds offset of targeting based on velocity of target
			{
				var direction = (((currentTarget.activeCharacter.position - GunPoint.transform.position)) + targetVelocity).normalized;
				return direction;
			}
			else // if target is not moving
			{
				return currentTarget.activeCharacter.position;
			}
		}

		void SetProjectile()
		{
			//interchange between what projectile to fire
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, TargetPoint.position);
		}
	}
}

