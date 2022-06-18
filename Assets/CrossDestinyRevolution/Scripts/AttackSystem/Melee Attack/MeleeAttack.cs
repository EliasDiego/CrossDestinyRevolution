using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;
using CDR.StateSystem;
using CDR.MechSystem;
using CDR.ObjectPoolingSystem;

namespace CDR.AttackSystem
{
    public class MeleeAttack : CooldownAction, IMeleeAttack
    {
		[SerializeField] HitBox _hitBox;
		[SerializeField] float _speed;
		[SerializeField] float _meleeDamage;

		// Cooldown
		[SerializeField] float _meleeAttackCoolDown;

		// Timer
		[SerializeField] float _meleeAttackDuration;
		[SerializeField] float _timer;

		// Object Pool
		[SerializeField] ObjectPooling _pool;

		// State
		IMech sender;
		IMech receiver;

		[SerializeField] bool isHoming;

        public IHitShape hitbox => _hitBox;
        public float speed => _speed;

		protected override void Awake()
		{
			base.Awake();

			if(_pool != null)
				_pool.Initialize();
		}

		private void Start()
		{
			_cooldownDuration = _meleeAttackCoolDown;
			_timer = _meleeAttackDuration;
		}

		public override void Use()
		{
			base.Use();
			
			isHoming = true;
			//_hitBox.enabled = true;
			_hitBox.onHitEnter += HitEnter;

			Character.input.DisableInput();
			Character.movement.End();
			//Character.shield.End();
		}

		public override void End()
		{
			base.End();

			_timer = _meleeAttackDuration;
			//_hitBox.enabled = false;
			_hitBox.onHitEnter -= HitEnter;

			Character.input.EnableInput();
			Character.movement.Use();
			//Character.shield.Use();
		}

		void HitEnter(IHitEnterData hitData)
		{
			isHoming = false;
			Character.controller.SetVelocity(Vector3.zero);
			Debug.LogWarning("Hit!!! " + hitData.hurtShape.character);

			hitData.hurtShape.character.health.TakeDamage(_meleeDamage);
		
			sender = (IMech)Character;
			receiver = (IMech)hitData.hurtShape.character;
			
			GameObject kb = _pool.GetPoolable();
			kb.transform.SetParent(((ActiveCharacter)receiver).transform);
			kb.SetActive(true);

			receiver.currentState = kb.GetComponent<IState>();
			receiver.currentState.sender = sender;
			receiver.currentState.receiver = receiver;
			receiver.currentState.StartState();

			End();
		}

		public override void Update()
		{
			base.Update();

			if(isHoming)
			{
				CheckAttackTimer();
			}
		}

		private void FixedUpdate()
		{
			if(isHoming)
			{
				Vector3 targetPos = Character.targetHandler.GetCurrentTarget().activeCharacter.position;
				Vector3 dir = (Character.position - targetPos).normalized;
				Quaternion lookRot = Quaternion.LookRotation(targetPos - Character.position);

				Character.controller.Rotate(Quaternion.Slerp(transform.rotation, lookRot, speed * Time.fixedDeltaTime));
				Character.controller.AddRbForce(-dir * speed);
			}
		}

		void CheckAttackTimer()
		{
			_timer -= Time.deltaTime;

			if(_timer < 0)
			{
				isHoming = false;
				Character.controller.SetVelocity(Vector3.zero);
				End();
			}
		}
	}
}

