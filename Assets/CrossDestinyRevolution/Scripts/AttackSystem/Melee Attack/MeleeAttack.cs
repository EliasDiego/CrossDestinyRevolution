using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;
using CDR.StateSystem;

namespace CDR.AttackSystem
{
    public class MeleeAttack : CooldownAction, IMeleeAttack
    {
		[SerializeField] HitBox _hitBox;
		[SerializeField] float _speed;
		[SerializeField] float _meleeDamage;

		[SerializeField] GameObject _knockbackPrefab;
		IKnockback knockback;

        public IHitShape hitbox => _hitBox;
        public float speed => _speed;
		

		public override void Use()
		{
			base.Use();
			
			_hitBox.enabled = true;
			_hitBox.onHitEnter += HitEnter;

			End();
		}

		public override void End()
		{
			base.End();

			_hitBox.enabled = false;
			_hitBox.onHitEnter -= HitEnter;
		}

		void HitEnter(IHitEnterData data)
		{
			//knockback
			//end melee atk
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

