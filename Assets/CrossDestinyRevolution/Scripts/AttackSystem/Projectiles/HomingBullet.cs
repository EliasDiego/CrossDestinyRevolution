using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class HomingBullet : HomingProjectile
    {
        float originDistanceFromProjectile; //From origin point to the current position of the projectile
        float originDistanceFromTarget; // From origin point to the current position of the target

        public override void OnEnable()
		{
            base.OnEnable();

            if(playerAttackRange < originDistanceFromTarget)
			{
                isHoming = false;
			}

            if (target != null)
            {
                transform.LookAt(target.position);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (target != null)
            {
                originDistanceFromProjectile = Vector3.Distance(transform.position, originPoint);
                originDistanceFromTarget = Vector3.Distance(target.position, originPoint);
            }

            if (isHoming)
            {
                RotateProjectile();
            }

            if (originDistanceFromProjectile > originDistanceFromTarget || originDistanceFromTarget > playerAttackRange)
            {
                isHoming = false;
            }
        }

		public override void Start()
		{
            base.Start();
        }

		public override void RotateProjectile()
        {
            var heading = target.position - transform.position;
            //var heading = _deviatedPrediction - transform.position;
            var rotation = Quaternion.LookRotation(heading);
            Rotate(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
            //_rigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
            //_rigidBody.MoveRotation(rotation);
        }

		private void OnDrawGizmos()
		{
            //Gizmos.color = Color.red;
            //Gizmos.DrawLine(transform.position, _standardPrediction);
            //Gizmos.color = Color.green;
            //Gizmos.DrawLine(_standardPrediction, _deviatedPrediction);
        }
    }
}

