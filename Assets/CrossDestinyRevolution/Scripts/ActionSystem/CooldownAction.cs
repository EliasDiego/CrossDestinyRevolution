using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.ActionSystem
{
	public class CooldownAction : Action, ICooldownAction
	{
		protected float _cooldownDuration;
		float _currentCooldown;
		protected bool _isCoolingDown;

		public event System.Action<ICooldownAction> onCoolDown;
		public event System.Action<ICooldownAction> onStartCoolDown;
		public event System.Action<ICooldownAction> onEndCoolDown;


		public float cooldownDuration => _cooldownDuration;
		public float currentCooldown => _currentCooldown;
		public bool isCoolingDown => _isCoolingDown;

		public override void Use()
		{
			base.Use();
		}
		public override void End()
		{
			base.End();
			onStartCoolDown?.Invoke(this);
			_currentCooldown = _cooldownDuration;
			_isCoolingDown = true;
		}

		public virtual void Update()
		{
			if(_isCoolingDown)
				ProcessCooldown();
		}

		void ProcessCooldown()
		{
			float deltaTime = Time.deltaTime;

			_currentCooldown = Mathf.Max(_currentCooldown - deltaTime, 0f);

			_isCoolingDown = _currentCooldown > 0f;

			onCoolDown?.Invoke(this);

			if(!_isCoolingDown)
			{
				EndCoolDown();
			}
		}

		public void EndCoolDown()
		{
			onEndCoolDown?.Invoke(this);
		}
	}
}
