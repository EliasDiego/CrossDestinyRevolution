using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
	public interface IProjectile
	{
		public Collider HitBox { get; }
		public float Lifetime { get; }
	}
}

