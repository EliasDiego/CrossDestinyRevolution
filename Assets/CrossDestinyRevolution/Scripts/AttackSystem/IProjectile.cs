using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.StateSystem;
using CDR.ActionSystem;
using CDR.MovementSystem;
using CDR.ObjectPoolingSystem;

namespace CDR.AttackSystem
{
	public interface IProjectile : IPoolable
	{
		IController controller { get; }

		public Collider HitBox { get; }
		public float Lifetime { get; }
	}
}

