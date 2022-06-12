using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;
using CDR.StateSystem;
using CDR.MechSystem;

namespace CDR.AttackSystem
{
    public class MeleeAttack : CooldownAction, IMeleeAttack
    {
		[SerializeField] HitBox _hitBox;
		[SerializeField] float _speed;
		[SerializeField] float _coolDown;
		[SerializeField] float _meleeDamage;
		[SerializeField] Vector3 _followOffset;
		[SerializeField] GameObject _knockbackPrefab;
		IMech sender;
		IMech receiver;

		bool isHoming;

        public IHitShape hitbox => _hitBox;
        public float speed => _speed;
		
		private void Start()
		{
			_cooldownDuration = _coolDown;
		}

		public override void Use()
		{
			base.Use();
			
			_hitBox.enabled = true;
			isHoming = true;
			_hitBox.onHitEnter += HitEnter;

			sender.input.DisableInput();
			sender.movement.End();
			//sender.shield.End();

			End();
		}

		public override void End()
		{
			base.End();

			_hitBox.enabled = false;
			_hitBox.onHitEnter -= HitEnter;

			sender.input.EnableInput();
			sender.movement.Use();
			//sender.shield.Use();
		}

		void HitEnter(IHitEnterData hitData)
		{
			if(hitData.hurtShape.character != Character)
			{
				isHoming = false;	

				GameObject kb = Instantiate(_knockbackPrefab);

				sender = (IMech)Character;
				receiver = (IMech)hitData.hurtShape.character;

				receiver.currentState = kb.GetComponent<IState>();
				receiver.currentState.sender = sender;
				receiver.currentState.receiver = receiver;
				receiver.currentState.StartState();
			}
		}

		private void FixedUpdate()
		{
			if(isHoming)
			{
				Vector3 targetPos = Character.targetHandler.GetCurrentTarget().activeCharacter.position;
				Quaternion lookRot = Quaternion.LookRotation(targetPos - Character.position);

				Character.controller.Rotate(Quaternion.Slerp(transform.rotation, lookRot, speed * Time.fixedDeltaTime));
				Character.controller.Translate((targetPos - _followOffset), speed * Time.fixedDeltaTime);
			}
		}
	}
}

