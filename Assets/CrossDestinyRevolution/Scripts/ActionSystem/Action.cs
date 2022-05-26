using CDR.MechSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.ActionSystem
{
	public class Action : MonoBehaviour, IAction
	{
		private bool isActionActive;
		private IActiveCharacter activeCharacter;

		public event System.Action onUse;
		public event System.Action onEnd;

		public bool isActive => isActionActive;
		public IActiveCharacter Character => activeCharacter;

<<<<<<< HEAD
=======
		protected virtual void Awake()
		{
			activeCharacter = GetComponent<ActiveCharacter>();
		}

>>>>>>> d119aa0fce11f2afa10cef419cd99ea93b1811fa
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

