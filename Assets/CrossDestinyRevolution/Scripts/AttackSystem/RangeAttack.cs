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

