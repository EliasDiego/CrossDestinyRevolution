using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class HomingMissile : Projectile
    {
        [SerializeField] public float bulletSpeed = 15f;
        [SerializeField] public float rotateSpeed = 5f;

        [Header("PREDICTION")]
        [SerializeField] protected float _maxDistancePredict = 45;
        [HideInInspector] protected float _minDistancePredict = 5;
        [HideInInspector] protected float _maxTimePrediction = 5;

        [Header("DEVIATION")]
        [SerializeField] protected float _deviationAmount = 50;
        [HideInInspector] protected float _deviationSpeed = 2;

        protected Vector3 _standardPrediction, _deviatedPrediction;

        public void FixedUpdate()
        {

            var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, distanceFromTarget);

            PredictMovement(leadTimePercentage);
            AddDeviation(leadTimePercentage);

            RotateProjectile();
        }


        public override void Start()
        {
            base.Start();
        }

        public void RotateProjectile()
        {
            var heading = _deviatedPrediction - transform.position;
            var rotation = Quaternion.LookRotation(heading);

            Rotate(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
            //_rigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
        }

        protected virtual void PredictMovement(float leadTimePercentage)
        {
            var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);

            if (target != null)
            {
                _standardPrediction = target.position + target.controller.velocity * predictionTime;
            }
        }

        protected virtual void AddDeviation(float leadTimePercentage)
        {
            var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);
            var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;
            _deviatedPrediction = _standardPrediction + predictionOffset;
        }
    }
}
