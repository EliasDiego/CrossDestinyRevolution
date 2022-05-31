using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.HitboxSystem
{
	public enum ColliderState
	{
		Closed,
		Open,
		Colliding
	}

	public interface IHitboxResponder
	{
		void collisionedWith(Collider collider);
	}
}
