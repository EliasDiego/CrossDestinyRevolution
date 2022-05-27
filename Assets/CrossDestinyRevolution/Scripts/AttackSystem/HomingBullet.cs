using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CDR.AttackSystem
{
    public class HomingBullet : HomingProjectile
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, distanceFromTarget);

            if (distanceFromTarget < _maxDistancePredict && isHoming)
            {
                //isHoming = true;
                PredictMovement(leadTimePercentage);
                AddDeviation(leadTimePercentage);
            }
            if (distanceFromTarget > _maxDistancePredict)
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

        private void OnCollisionEnter(Collision collision)
        {
            //if (_explosionPrefab) Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            //if (collision.transform.TryGetComponent<IExplode>(out var ex)) ex.Explode();
            Destroy(gameObject);
        }
    }
}

