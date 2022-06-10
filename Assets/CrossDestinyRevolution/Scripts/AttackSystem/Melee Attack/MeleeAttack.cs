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
		[SerializeField] float _meleeDamage;

		[SerializeField] GameObject _knockbackPrefab;
		IMech sender;
		IMech receiver;

        public IHitShape hitbox => _hitBox;
        public float speed => _speed;
		

		public override void Use()
		{
			base.Use();
			
			_hitBox.enabled = true;
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
				GameObject kb = Instantiate(_knockbackPrefab);

				sender = (IMech)Character;
				receiver = (IMech)hitData.hurtShape.character;

				receiver.currentState = kb.GetComponent<IState>();
				receiver.currentState.sender = sender;
				receiver.currentState.receiver = receiver;
				receiver.currentState.StartState();
			}
		}

		public void DoMeleeAttack()
		{
			if(!_isCoolingDown)
			{
				//On input, do animation and Hitbox active damage true
				//After animation set hitbox damage inactive and set cooldown of melee
			}
		}
		
	}
}

