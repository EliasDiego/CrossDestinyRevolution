using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class HomingBullet : HomingProjectile
    {
        float projectileDistanceFromOrigin; //From origin point to the current position of the projectile
        float originPointDistanceFromTarget;

        public void Awake()
		{
            if(playerAttackRange < originPointDistanceFromTarget)
			{
                isHoming = false;
			}
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, distanceFromTarget);

            if (target != null)
            {
                projectileDistanceFromOrigin = Vector3.Distance(transform.position, originPoint);
                originPointDistanceFromTarget = Vector3.Distance(target.position, originPoint);
            }

            if (isHoming)
            {
                PredictMovement(leadTimePercentage);
                AddDeviation(leadTimePercentage);

                RotateProjectile();
            }
            if (projectileDistanceFromOrigin > originPointDistanceFromTarget)
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
            var heading = _deviatedPrediction - transform.position;
            var rotation = Quaternion.LookRotation(heading);
            _rigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
            //_rigidBody.MoveRotation(rotation);
        }
	}
}

