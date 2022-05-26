using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;

namespace CDR.AttackSystem
{
<<<<<<< HEAD
	public class RangeAttack : CooldownAction
=======
	public class RangeAttack : CooldownAction , IRangeAttack
>>>>>>> d119aa0fce11f2afa10cef419cd99ea93b1811fa
	{
		[SerializeField] float FireRate;

		[SerializeField] GameObject GunPoint; //Invisible gameobject for the location of the gun barrel
		[SerializeField] public Transform TargetPoint; //Predictive Targeting system

		[SerializeField] GameObject BulletProjectile;

<<<<<<< HEAD
=======
		public IProjectile projectile => throw new System.NotImplementedException();

		public float range => throw new System.NotImplementedException();

>>>>>>> d119aa0fce11f2afa10cef419cd99ea93b1811fa
		private void Start()
		{
			_cooldownDuration = FireRate;
		}

		public override void Update()
		{
			base.Update();
<<<<<<< HEAD

			BulletProjectile.GetComponent<Projectile>().projectileTarget = TargetPoint.transform.position;
			BulletProjectile.GetComponent<Projectile>().projectileOriginPoint = GunPoint.transform.position;

			if (Input.GetMouseButton(0) && !_isCoolingDown) //test
			{
				Use();
			}
=======
			
			BulletProjectile.GetComponent<Projectile>().projectileTarget = SetPredictiveTarget();
			BulletProjectile.GetComponent<Projectile>().projectileOriginPoint = GunPoint.transform.position;
>>>>>>> d119aa0fce11f2afa10cef419cd99ea93b1811fa
		}

		public override void Use()
		{
			base.Use();

			Instantiate(BulletProjectile, GunPoint.transform.position, Quaternion.identity);

			End();
		}

		public override void End()
		{
			base.End();
		}

		Vector3 SetPredictiveTarget()
		{
<<<<<<< HEAD
			var direction = (TargetPoint.position - GunPoint.transform.position).normalized;
			return direction;
=======
			var currentTarget = Character.targetHandler.GetCurrentTarget();
			var targetVelocity = currentTarget.activeCharacter.controller.velocity;
			
			//var currentTarget = TargetPoint;
			//var targetVelocity = TargetPoint.GetComponent<TestVelocity>();

			if (targetVelocity != Vector3.zero) //Adds offset of targeting based on velocity of target
			{
				var direction = (((currentTarget.activeCharacter.position - GunPoint.transform.position)) + targetVelocity).normalized;
				return direction;
			}
			else // if target is not moving
			{
				return currentTarget.activeCharacter.position;
			}
>>>>>>> d119aa0fce11f2afa10cef419cd99ea93b1811fa
		}

		void SetProjectile()
		{
			//interchange between what projectile to fire
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, TargetPoint.position);
		}
	}
}

