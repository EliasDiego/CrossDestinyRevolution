using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;

namespace CDR.AttackSystem
{
    public class MeleeAttack : CooldownAction
    {
		[SerializeField] Collider MeleeHitbox;
		bool MeleeHitboxActive = false;


		[SerializeField] float MeleeDamage;


		public override void Use()
		{
			base.Use();



			End();
		}

		public override void End()
		{
			base.End();
		}

		public void DoMeleeAttack()
		{
			if(!_isCoolingDown)
			{
				//On input, do animation and Hitbox active damage true
				//After animation set hitbox damage inactive and set cooldown of melee
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			//find health of character script of collided then do damage based on Melee Damage
		}
	}
}

