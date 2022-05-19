using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.ActionSystem
{
	public class CooldownAction : Action, ICooldownAction
	{
		[SerializeField] float _cooldownDuration;
		float _currentCooldown;
		bool _isCoolingDown;

		public event System.Action OnStartCooldown;
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
			_currentCooldown = _cooldownDuration;
			_isCoolingDown = true;
		}

		void Update()
		{
			if(_isCoolingDown)
				ProcessCooldown();
		}

		void ProcessCooldown()
		{
			float deltaTime = Time.deltaTime;
			
			_isCoolingDown = CooldownCountDown(deltaTime);
		}

		bool CooldownCountDown(float deltaTime)
		{
			_currentCooldown = Mathf.Max(_currentCooldown - deltaTime, 0f);

			return _currentCooldown <= 0f;
		}
	}
}
