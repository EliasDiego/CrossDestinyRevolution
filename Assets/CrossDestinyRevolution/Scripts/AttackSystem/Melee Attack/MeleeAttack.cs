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
		[SerializeField] float _attackCoolDown;
		[SerializeField] float _meleeDamage;
		[SerializeField] Vector3 _followOffset;
		[SerializeField] GameObject _knockbackPrefab;
		IMech sender;
		IMech receiver;

		[SerializeField] bool isHoming;

        public IHitShape hitbox => _hitBox;
        public float speed => _speed;

		public override void Use()
		{
			base.Use();
			
			isHoming = true;
			_hitBox.enabled = true;
			_hitBox.onHitEnter += HitEnter;

			Character.input.DisableInput();
			Character.movement.End();
			//Character.shield.End();

			Debug.Log("Melee atk use");
		}

		public override void End()
		{
			base.End();

			_hitBox.enabled = false;
			_hitBox.onHitEnter -= HitEnter;

			Character.input.EnableInput();
			Character.movement.Use();
			//Character.shield.Use();
			Debug.Log("Melee atk end");
		}

		void HitEnter(IHitEnterData hitData)
		{
			isHoming = false;
			Debug.LogWarning("Hit!!!" + hitData.hurtShape.character);
			hitData.hurtShape.character.health.TakeDamage(_meleeDamage);
		

			sender = (IMech)Character;
			receiver = (IMech)hitData.hurtShape.character;

			GameObject kb = Instantiate(_knockbackPrefab, ((ActiveCharacter)receiver).transform);

			receiver.currentState = kb.GetComponent<IState>();
			receiver.currentState.sender = sender;
			receiver.currentState.receiver = receiver;
			receiver.currentState.StartState();

			End();
		}

		public override void Update()
		{
			base.Update();
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
				//Character.controller.Translate((targetPos - _followOffset), speed * Time.fixedDeltaTime);
			}
		}
	}
}

