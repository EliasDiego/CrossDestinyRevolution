using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;

namespace CDR.AttackSystem
{
	public class Bullet : Projectile
	{
		[SerializeField] public float BulletSpeed;
		Rigidbody _rigidBody;

		public override void Start()
		{
			base.Start();

			_rigidBody = GetComponent<Rigidbody>();

		}

		private void FixedUpdate()
		{
			MoveBullet();
			//RotateBullet();
		}

		void MoveBullet()
		{
			//_rigidBody.velocity = transform.forward * BulletSpeed;
			_rigidBody.velocity = transform.forward * BulletSpeed;
		}

		void RotateBullet()
		{
			//_rigidBody.MoveRotation(Quaternion.LookRotation(projectileTarget));
		}

		/*Vector3 SetPredictiveTarget() //Real predictive
		{
			//var currentTarget = Character.targetHandler.GetCurrentTarget();
			//var targetVelocity = currentTarget.activeCharacter.controller.velocity;
			//var targetPos = currentTarget.activeCharacter.position;

			if (targetVelocity != Vector3.zero) //Adds offset of targeting based on velocity of target
			{
				var direction = ((targetPos + targetVelocity) - transform.position).normalized;
				return direction;
			}
			else // if target is not moving
			{
				return targetPos;
			}
		}

		Vector3 PredictiveTarget(float bulletSpeed, float targetSpeed) //test
		{
			var bulletInitialPos = transform.position;
			var bulletSpeedSquared = Mathf.Pow(bulletSpeed, 2);

			var target = Character.targetHandler.GetCurrentTarget();
			var targetSpeedSquared = Mathf.Pow(targetSpeed, 2);
			var targetInitialPos = target.activeCharacter.position;
			var targetVelocity = target.activeCharacter.controller.velocity;

			var distanceOfTarget = Vector3.Distance(targetInitialPos, bulletInitialPos);
			var distanceOfTargetSquared = Mathf.Pow(distanceOfTarget, 2);

			var cosAngle = Vector3.Dot((targetInitialPos - bulletInitialPos).normalized, targetVelocity.normalized);

			var a = (bulletSpeedSquared - targetSpeedSquared);
			var b = distanceOfTarget * targetSpeed * Mathf.Cos(cosAngle);
			var c = -distanceOfTarget;
			var t = (-2 * b + Mathf.Abs(((Mathf.Sqrt(Mathf.Pow(2 * b, 2)))) + 4 * a * Mathf.Pow(c, 2))) / 2 * a;

			var bulletTrajectory = (distanceOfTargetSquared) + (targetSpeed * t) - 2 * distanceOfTarget * (targetSpeed * t) * (Mathf.Cos(cosAngle));
			var desiredAimDirection = targetSpeed + ((distanceOfTarget) / t);

			if (targetVelocity != Vector3.zero) //Adds offset of targeting based on velocity of target
			{
				var direction = ((targetInitialPos + targetVelocity) - bulletInitialPos).normalized;
				return direction;
			}
			else // if target is not moving
			{
				return targetInitialPos;
			}
		}*/


		private void OnCollisionEnter(Collision collision)
		{
			//Check for Collided Character damage function and deal damage
			if (collision == null)
			{
				//collision.GetComponent<>();
			}

		}
	}
}

