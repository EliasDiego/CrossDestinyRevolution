using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;
using CDR.StateSystem;
using CDR.MechSystem;
using CDR.ObjectPoolingSystem;
using CDR.AnimationSystem;

namespace CDR.AttackSystem
{
    public class MeleeAttack : CooldownAction, IMeleeAttack
    {
		[SerializeField] HitBox _hitBox;
		[SerializeField] float _speed;
		[SerializeField] float _meleeDamage;
		[SerializeField] float _distanceToTarget;

		// Animation Handler
		[SerializeField] MeleeAttackAnimationHandler _animHandler;

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
			_animHandler.PlayAttackAnim();
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

		public override void ForceEnd()
		{
			base.ForceEnd();

			_hitBox.onHitEnter -= HitEnter;
		}

		void HitEnter(IHitData hitData)
		{
			_animHandler.EndAttackAnim();
			Character.controller.SetVelocity(Vector3.zero);
			Debug.LogWarning("Hit!!! " + hitData.hurtShape.character);

		
			sender = (IMech)Character;
			receiver = (IMech)hitData.hurtShape.character;

			if(!receiver.shield.isActive)
			{
				receiver.health.TakeDamage(_meleeDamage);

				GameObject kb = _pool.GetPoolable();
				kb.transform.SetParent(((ActiveCharacter)receiver).transform);
				kb.SetActive(true);

				receiver.currentState = kb.GetComponent<IState>();
				receiver.currentState.sender = sender;
				receiver.currentState.receiver = receiver;
				receiver.currentState.StartState();
			}

			End();
		}

		public override void Update()
		{
			base.Update();

			if(isHoming)
			{
				CheckAttackTimer();
				CheckDistanceToTarget();
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
				_animHandler.EndAttackAnim();
				Character.controller.SetVelocity(Vector3.zero);
				End();
			}
		}

		void CheckDistanceToTarget()
		{
			float distance = Vector3.Distance(Character.position, Character.targetHandler.GetCurrentTarget().activeCharacter.position);

			if(distance <= _distanceToTarget)
			{
				isHoming = false;
				_hitBox.enabled = true;
				_animHandler.ResumeAnimation();
			}
		}
	}
}