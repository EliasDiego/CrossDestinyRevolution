using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;
using CDR.AttackSystem.New;

namespace CDR.AttackSystem
{
    public class MeleeAttack : CooldownAction
    {
		[SerializeField] HitBox _hitBox;
		bool MeleeHitboxActive = false;


		[SerializeField] float MeleeDamage;


		public override void Use()
		{
			base.Use();
			_hitBox.enabled = true;
			_hitBox.onHitEnter += TestHit;

			End();
		}

		public override void End()
		{
			base.End();

			_hitBox.enabled = false;
			_hitBox.onHitEnter -= TestHit;
		}

		public void DoMeleeAttack()
		{
			if(!_isCoolingDown)
			{
				//On input, do animation and Hitbox active damage true
				//After animation set hitbox damage inactive and set cooldown of melee
			}
		}

		void TestHit(IHitEnterData hitData)
		{
			//kb
			//end melee atk
		}

		private void OnCollisionEnter(Collision collision)
		{
			//find health of character script of collided then do damage based on Melee Damage
		}
	}
}

