using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;
using CDR.ObjectPoolingSystem;
using CDR.AnimationSystem;
using CDR.VFXSystem;

namespace CDR.AttackSystem
{
	public class RangeAttack : CooldownAction , IRangeAttack
	{
		[SerializeField] ObjectPooling _pool;

		[SerializeField] float FireRate;
		[SerializeField] GameObject GunPoint;
		[SerializeField] float attackRange;

		[SerializeField] RangeAttackVFXHandler rangeAttackVFXHandler;

		public float range => attackRange;

		[SerializeField] CDR.AnimationSystem.AnimationEvent _animationEvent;

		AnimationEventsManager _Manager;
		[SerializeField] SFXAnimationEvent[] sfxAnimationEvents;

		void Start()
		{
			_Manager = Character.animator.GetComponent<AnimationEventsManager>();

			var a = new CDR.AnimationSystem.AnimationEvent(0.29f, true, () => GetBulletFromObjectPool());
			var b = new CDR.AnimationSystem.AnimationEvent(1f, true, () => End());

			_Manager.AddAnimationEvent("RAttack", a,b);
			_Manager.AddAnimationEvent("RAttack", sfxAnimationEvents);
		}

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

			Character.animator.SetInteger("ActionType", (int)ActionType.RangeAttack);
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

			rangeAttackVFXHandler.Activate();
		}

		public override void End()
		{
			base.End();

			Character.animator.SetInteger("ActionType", (int)ActionType.None);
		}

		public override void ForceEnd()
		{
			base.ForceEnd();

			Character.animator.SetInteger("ActionType", (int)ActionType.None);

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

