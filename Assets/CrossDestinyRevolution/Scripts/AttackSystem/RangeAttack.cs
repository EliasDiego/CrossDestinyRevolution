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
		[SerializeField] float attackRange;

		public float range => attackRange;

		public override void Update()
		{
			base.Update();
			_cooldownDuration = FireRate;
		}

		public override void Use()
		{
			base.Use();

			var target = Character.targetHandler.GetCurrentTarget();
			var targetPos = target.activeCharacter.position;

			var direction = targetPos - GunPoint.transform.position;

			var bullet = Instantiate(BulletProjectile, GunPoint.transform.position, Quaternion.LookRotation(direction));
			bullet.GetComponent<HomingProjectile>().target = target.activeCharacter;

			End();
		}

		public override void End()
		{
			base.End();
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, Target.transform.position);
		}
	}
}

