using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class HomingProjectile : Projectile
    {
        [SerializeField] public float bulletSpeed = 15f;
		[HideInInspector] public float rotateSpeed = 95f;

		protected Rigidbody _rigidBody;
		protected bool isHoming = true;

		[Header("PREDICTION")]
		[HideInInspector] protected float _maxDistancePredict = 100;
		[HideInInspector] protected float _minDistancePredict = 5;
		[HideInInspector] protected float _maxTimePrediction = 5;
		protected Vector3 _standardPrediction, _deviatedPrediction;

		[Header("DEVIATION")]
		[HideInInspector] protected float _deviationAmount = 50;
		[HideInInspector] protected float _deviationSpeed = 2;

		public override void Start()
		{
			base.Start();
			_rigidBody = GetComponent<Rigidbody>();
			
			//_standardPrediction = target.position;
			//_deviatedPrediction = target.position;
		}

		public override void OnEnable()
		{
			base.OnEnable();
			isHoming = true;
		}

		public virtual void FixedUpdate()
		{
			if(target != null)
				distanceFromTarget = Vector3.Distance(transform.position, target.position);

			MoveProjectile();
		}

		public virtual void MoveProjectile()
		{
			_rigidBody.velocity = transform.forward * bulletSpeed;
		}

		public virtual void RotateProjectile(){}

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

		private void OnDrawGizmos()
		{
			//Gizmos.color = Color.red;
			//Gizmos.DrawLine(transform.position, _standardPrediction);

			//Gizmos.DrawWireSphere(transform.position, _maxDistancePredict);
			//Gizmos.color = Color.clear;
		}
	}
}

