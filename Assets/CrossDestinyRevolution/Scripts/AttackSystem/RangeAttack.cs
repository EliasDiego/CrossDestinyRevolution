using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;

namespace CDR.AttackSystem
{
	public class RangeAttack : CooldownAction , IRangeAttack
	{
		[SerializeField] float FireRate;

		[SerializeField] GameObject GunPoint; 
		[SerializeField] public GameObject Target; 

		[SerializeField] GameObject BulletProjectile;

		public IProjectile projectile => throw new System.NotImplementedException();

		public float range => throw new System.NotImplementedException();

		private void Start()
		{
			
		}

		public override void Update()
		{
			base.Update();
			_cooldownDuration = FireRate;
		}

		public override void Use()
		{
			base.Use();

			//BulletProjectile.GetComponent<Projectile>().currentTarget = Character.targetHandler.GetCurrentTarget().activeCharacter.;
			BulletProjectile.GetComponent<Projectile>().currentTarget = Target;

			var direction = Target.transform.position - GunPoint.transform.position;
			Instantiate(BulletProjectile, GunPoint.transform.position, Quaternion.LookRotation(direction));
			//Instantiate(BulletProjectile, GunPoint.transform.position, Quaternion.LookRotation(Character.targetHandler.GetCurrentTarget().activeCharacter.position));

			End();
		}

		public override void End()
		{
			base.End();
		}

		

		void SetProjectile()
		{
			//interchange between what projectile to fire
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, Target.transform.position);
		}
	}
}

