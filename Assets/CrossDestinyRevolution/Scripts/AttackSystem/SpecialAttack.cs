using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;
using CDR.ObjectPoolingSystem;

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
		}

		public override void End()
		{
			base.End();
		}

		public override void ForceEnd()
		{
			base.ForceEnd();

			for(int i = 0; i < _pool.Length; i++)
			{
				if(_pool[i] != null)
				{
					_pool[i].ReturnAll();
				}
			}
		}
	}
}

