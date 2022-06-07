using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class HomingMissile : HomingProjectile
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, distanceFromTarget);

            PredictMovement(leadTimePercentage);
            //AddDeviation(leadTimePercentage);

            RotateProjectile();
        }


        public override void Start()
        {
            base.Start();
        }

        public override void RotateProjectile()
        {
            base.RotateProjectile();

            var heading = _deviatedPrediction - transform.position;
            var rotation = Quaternion.LookRotation(heading);
            _rigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
        }
    }
}
