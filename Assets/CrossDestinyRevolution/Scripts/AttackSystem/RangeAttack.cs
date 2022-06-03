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
		[SerializeField] Owner playerOwner;
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
			
			if(playerOwner == Owner.Player1)
			{
				var bullet = Player1ObjectPooling.Instance.GetPoolable("HomingBullet", playerOwner);

				bullet.GetComponent<Projectile>().target = target.activeCharacter;
				bullet.GetComponent<Projectile>().playerAttackRange = attackRange;
				bullet.GetComponent<Projectile>().originPoint = GunPoint.transform.position;
				bullet.SetActive(true);
			}
			if(playerOwner == Owner.Player2)
			{

			}

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

