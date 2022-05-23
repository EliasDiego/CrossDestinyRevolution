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

			BulletProjectile.GetComponent<Projectile>().projectileTarget = TargetPoint.transform.position;
			BulletProjectile.GetComponent<Projectile>().projectileOriginPoint = GunPoint.transform.position;

			//Use();
			
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
			var direction = (TargetPoint.position - GunPoint.transform.position).normalized;
			return direction;
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

