using CDR.MechSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.ActionSystem
{
	public class Action : MonoBehaviour, IAction
	{
		private bool isActionActive;
		private ActiveCharacter activeCharacter;

		public event System.Action onUse;
		public event System.Action onEnd;

		public bool isActive => isActionActive;
		public ActiveCharacter Character => activeCharacter;

		public virtual void Use()
		{
			isActionActive = true;
		}

		public virtual void End()
		{
			isActionActive = false;
		}
	}
}

