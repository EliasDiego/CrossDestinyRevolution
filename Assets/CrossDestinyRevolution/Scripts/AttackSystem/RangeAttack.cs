using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;
using CDR.ObjectPoolingSystem;

namespace CDR.AttackSystem
{
	public class RangeAttack : CooldownAction , IRangeAttack
	{
		[SerializeField] float FireRate;
		[SerializeField] GameObject GunPoint;
		[SerializeField] float attackRange;

		public float range => attackRange;

		public override void Update()
		{
			base.Update();
			_cooldownDuration = FireRate;
		}

		public void Start()
		{

		}

		public override void Use()
		{
			base.Use();

			GetBulletFromObjectPool();
			
			End();
		}

		void GetBulletFromObjectPool()
		{
			var target = Character.targetHandler.GetCurrentTarget();
			var targetPos = target.activeCharacter.position;

			var direction = targetPos - GunPoint.transform.position;
			
			var bullet = ObjectPooling.Instance.GetPoolable("HomingBullet");

			bullet.GetComponent<Projectile>().target = target.activeCharacter;
			bullet.GetComponent<Projectile>().playerAttackRange = attackRange;
			bullet.GetComponent<Projectile>().originPoint = GunPoint.transform.position;
			bullet.SetActive(true);
		}

		public override void End()
		{
			base.End();
		}

		private void OnDrawGizmos()
		{
			//Gizmos.color = Color.red;
			//Gizmos.DrawLine(transform.position, Target.transform.position);
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, attackRange);
		}
	}
}

