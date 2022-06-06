using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.StateSystem;
using CDR.ActionSystem;
using CDR.MovementSystem;
using CDR.ObjectPoolingSystem;
using CDR.HitboxSystem;

namespace CDR.AttackSystem
{
	public interface IProjectile : IPoolable
	{
		IController controller { get; }

		public HitBox HitBox { get; }
		public HitSphere HitSphere { get; }
		public float Lifetime { get; }
		public float Damage { get; }
	}
}

