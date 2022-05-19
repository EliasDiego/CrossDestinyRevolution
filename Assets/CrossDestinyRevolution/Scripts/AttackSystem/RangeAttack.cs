using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;

namespace CDR.AttackSystem
{
	public class RangeAttack : Action
	{
		[SerializeField] float FireRate;
		float currentFireRate;
		bool isEnabletoFire = true;

		GameObject GunPoint; //Invisible gameobject for the location of the gun barrel
		Transform TargetPoint;

		public void Update()
		{
			if (!isEnabletoFire)
			{
				isEnabletoFire = CalculateFireRate();
			}

			if (Input.GetMouseButton(0) && isEnabletoFire) //test
			{
				Use();
			}
		}

		public override void Use()
		{
			base.Use();

			//Instantiate

			Debug.Log("Bullet spawned");

			End();
		}

		public override void End()
		{
			base.End();

			currentFireRate = FireRate;
			isEnabletoFire = false;

			Debug.Log("Start Cooldown");
		}

		bool CalculateFireRate()
		{
			float deltaTime = Time.deltaTime;

			currentFireRate = Mathf.Max(currentFireRate - deltaTime, 0f);

			return currentFireRate <= 0f;
		}
	}
}

