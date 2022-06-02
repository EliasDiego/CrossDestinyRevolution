using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class HomingBullet : HomingProjectile
    {
        float projectileDistanceFromOrigin;
        float originPointDistanceFromTarget;

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, distanceFromTarget);

            projectileDistanceFromOrigin = Vector3.Distance(transform.position, originPoint);
            originPointDistanceFromTarget = Vector3.Distance(target.position, originPoint);

            if (distanceFromTarget < _maxDistancePredict && isHoming)
            {
                //isHoming = true;
                PredictMovement(leadTimePercentage);
                AddDeviation(leadTimePercentage);
            }
            if (projectileDistanceFromOrigin > _maxDistancePredict || projectileDistanceFromOrigin > originPointDistanceFromTarget || playerAttackRange < originPointDistanceFromTarget)
            {
                isHoming = false;
            }

            RotateProjectile();
        }

		public override void Start()
		{
            base.Start();
        }

        public override void RotateProjectile()
        {
            base.RotateProjectile();

            if (distanceFromTarget < _maxDistancePredict && isHoming)
            {
                var heading = _deviatedPrediction - transform.position;
                var rotation = Quaternion.LookRotation(heading);
                _rigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
            }
        }
	}
}

