using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;
using CDR.ObjectPoolingSystem;

namespace CDR.AttackSystem
{
	public class RangeAttack : CooldownAction , IRangeAttack
	{
		[SerializeField] ObjectPooling _pool;

		[SerializeField] float FireRate;
		[SerializeField] GameObject GunPoint;
		[SerializeField] float attackRange;

		public float range => attackRange;

		protected override void Awake()
		{
			if(_pool != null)
				_pool.Initialize();
		}

		public override void Update()
		{
			base.Update();
			_cooldownDuration = FireRate;
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
			
			var bullet = _pool.GetPoolable();

			bullet.GetComponent<HomingBullet>().target = target.activeCharacter;
			bullet.GetComponent<HomingBullet>().playerAttackRange = attackRange;
			bullet.GetComponent<HomingBullet>().transform.position = GunPoint.transform.position;
			bullet.GetComponent<HomingBullet>().originPoint = GunPoint.transform.position;

			bullet.SetActive(true);
		}

		

		public override void End()
		{
			base.End();
		}

		public override void ForceEnd()
		{
			base.ForceEnd();

			_pool.ReturnAll();
		}

		private void OnDrawGizmos()
		{
			//Gizmos.color = Color.red;
			//Gizmos.DrawLine(transform.position, Character.targetHandler.GetCurrentTarget().activeCharacter.position);
			//Gizmos.color = Color.green;
			//Gizmos.DrawWireSphere(transform.position, attackRange);
		}
	}
}

