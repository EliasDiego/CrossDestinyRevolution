using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CDR.AttackSystem
{
    public class HomingBullet : HomingProjectile
    {
        [Header("PREDICTION")]
        [SerializeField] private float _maxDistancePredict = 100;
        [SerializeField] private float _minDistancePredict = 5;
        [SerializeField] private float _maxTimePrediction = 5;
        private Vector3 _standardPrediction, _deviatedPrediction;

        [Header("DEVIATION")]
        [SerializeField] private float _deviationAmount = 50;
        [SerializeField] private float _deviationSpeed = 2;

        float distanceFromTarget;

		public override void FixedUpdate()
        {
            base.FixedUpdate();

            distanceFromTarget = Vector3.Distance(transform.position, currentTarget.transform.position);

            var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, distanceFromTarget);

            if(distanceFromTarget < _maxDistancePredict && isHoming)
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
            _standardPrediction = currentTarget.transform.position;
            _deviatedPrediction = currentTarget.transform.position;
        }

        private void PredictMovement(float leadTimePercentage)
        {
            var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);
            _standardPrediction = currentTarget.transform.position + currentTarget.GetComponent<Rigidbody>().velocity * predictionTime;
        }

        private void AddDeviation(float leadTimePercentage)
        {
            var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);
            var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;
            _deviatedPrediction = _standardPrediction + predictionOffset;
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _standardPrediction);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_standardPrediction, _deviatedPrediction);

            Gizmos.DrawWireSphere(transform.position, _maxDistancePredict);
            Gizmos.color = Color.clear;
        }
    }
}

