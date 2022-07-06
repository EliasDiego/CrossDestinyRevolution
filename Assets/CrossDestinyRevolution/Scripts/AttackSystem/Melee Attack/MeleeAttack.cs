using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;
using CDR.StateSystem;
using CDR.MechSystem;
using CDR.ObjectPoolingSystem;
using CDR.AnimationSystem;
using CDR.VFXSystem;

namespace CDR.AttackSystem
{
    public class MeleeAttack : CooldownAction, IMeleeAttack
    {
		[SerializeField] HitBox _hitBox;
		[SerializeField] float _speed;
		[SerializeField] float _meleeDamage;
		[SerializeField] float _distanceToTarget;

		// VFX
		[SerializeField] MeleeAttackVFXHandler _meleeVfx;
		[SerializeField] BoostVFXHandler[] _boostVfx;
		BladeWingHitVFXPoolable _meleeHitVFX;

		// Animation Handler
		[SerializeField] MeleeAttackAnimationHandler _animHandler;

		// Cooldown
		[SerializeField] float _meleeAttackCoolDown;

		// Object Pool
		[SerializeField] ObjectPooling _knockbackPool;
		[SerializeField] ObjectPooling _hitVfxPool;

		// State
		IMech sender;
		IMech receiver;

		[SerializeField] bool isHoming;

        public IHitShape hitbox => _hitBox;
        public float speed => _speed;

		protected override void Awake()
		{
			base.Awake();

			if(_knockbackPool != null)
				_knockbackPool.Initialize();

			if(_hitVfxPool != null)
				_hitVfxPool.Initialize();
		}

		private void Start()
		{
			_cooldownDuration = _meleeAttackCoolDown;
		}

		public override void Use()
		{
			base.Use();
			
			isHoming = true;

			_meleeVfx.Activate();

			for(int i =0; i < _boostVfx.Length; i++)
			{
				_boostVfx[i].Activate();
			}

			_animHandler.PlayAttackAnim();
			_hitBox.onHitEnter += HitEnter;

			Character.input.DisableInput();
			Character.movement.End();
			//Character.shield.End();
		}

		public override void End()
		{
			base.End();

			_animHandler.EndAttackAnim();
			_meleeVfx.Deactivate();

			for(int i =0; i < _boostVfx.Length; i++)
			{
				_boostVfx[i].Deactivate();
			}

			_hitBox.onHitEnter -= HitEnter;

			Character.input.EnableInput();
			Character.movement.Use();
			//Character.shield.Use();
		}

		public override void ForceEnd()
		{
			base.ForceEnd();

			isHoming = false;
			_animHandler.EndAttackAnim();
			_meleeVfx.Deactivate();

			for(int i =0; i < _boostVfx.Length; i++)
			{
				_boostVfx[i].Deactivate();
			}

			_hitBox.onHitEnter -= HitEnter;
		}

		void HitEnter(IHitData hitData)
		{
			Debug.LogWarning("Hit!!! " + hitData.hurtShape.character);
			Character.controller.SetVelocity(Vector3.zero);

			GameObject hitVfx = _hitVfxPool.GetPoolable();
			hitVfx.transform.SetParent(this.transform);
			hitVfx.SetActive(true);

			_meleeHitVFX = hitVfx.GetComponent<BladeWingHitVFXPoolable>();
			_meleeHitVFX.PlayVfx();

			sender = (IMech)Character;
			receiver = (IMech)hitData.hurtShape.character;

			// Enemy only takes damage/changes state if not using shield
			if(!receiver.shield.isActive)
			{
				receiver.health.TakeDamage(_meleeDamage);

				GameObject kb = _knockbackPool.GetPoolable();
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


		void CheckDistanceToTarget()
		{
			float distance = Vector3.Distance(Character.position, Character.targetHandler.GetCurrentTarget().activeCharacter.position);

			if(distance <= _distanceToTarget)
			{
				Debug.Log("distance reached");
				isHoming = false;
				_hitBox.enabled = true;
				_animHandler.ResumeAnimation();
				_meleeVfx.Deactivate();
			}
		}
	}
}