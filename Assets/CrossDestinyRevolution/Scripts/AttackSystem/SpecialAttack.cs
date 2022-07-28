using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;
using CDR.ObjectPoolingSystem;
using CDR.MechSystem;
using CDR.AnimationSystem;

namespace CDR.AttackSystem
{
    public class SpecialAttack : CooldownAction, ISpecialAttack
    {
		[SerializeField] protected ObjectPooling[] _pool; //1st Stage Bullets, make a list for multiple bullets

		[SerializeField] protected GameObject[] bulletSpawnPoint; // Spawn point of 1st Stage bullets

		[SerializeField] float SpecialAbilityCooldown;



		void Start()
		{
			_cooldownDuration = SpecialAbilityCooldown;

		}

		public override void Update()
		{
			base.Update();
			//_cooldownDuration = SpecialAbilityCooldown;
		}

		public override void Use()
		{
			base.Use();

			Character.animator.SetFloat("ActionSMultiplier", 1);

			IMech mech = (IMech)Character;

			mech.movement?.ForceEnd();
			mech.rangeAttack?.End();
			//mech.shield?.ForceEnd();
			//mech.meleeAttack?.ForceEnd();
			mech.boost?.ForceEnd();

			mech.input?.DisableInput();
		}

		public override void End()
		{
			base.End();

			Character.animator.SetFloat("ActionSMultiplier", 1);

			Character.input?.EnableInput();
			Character.movement?.Use();
		}

		public override void ForceEnd()
		{
			base.ForceEnd();

			Character.animator.SetFloat("ActionSMultiplier", 1);
		}

		public override void Stop()
		{
			for (int i = 0; i < _pool.Length; i++)
			{
				if(_pool[i] != null)
				{
					_pool[i].ReturnAll();
				}
			}
		}
	}
}

