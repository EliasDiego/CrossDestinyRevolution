using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;
using CDR.ActionSystem;

namespace CDR.AttackSystem
{
    public class HomingProjectile : Projectile
    {
        [SerializeField] public float bulletSpeed;
		[SerializeField] public float rotateSpeed;

		protected Rigidbody _rigidBody;
		protected bool isHoming = true;

		[Header("PREDICTION")]
		[SerializeField] protected float _maxDistancePredict = 100;
		[SerializeField] protected float _minDistancePredict = 5;
		[SerializeField] protected float _maxTimePrediction = 5;
		protected Vector3 _standardPrediction, _deviatedPrediction;

		[Header("DEVIATION")]
		[SerializeField] protected float _deviationAmount = 50;
		[SerializeField] protected float _deviationSpeed = 2;

		protected float distanceFromTarget;

		public IActiveCharacter target { get; set; }
		

		public override void Start()
		{
			base.Start();
			_rigidBody = GetComponent<Rigidbody>();

			_standardPrediction = target.position;
			_deviatedPrediction = target.position;

			distanceFromTarget = Vector3.Distance(transform.position, target.position);
		}

		public virtual void FixedUpdate()
		{
			MoveProjectile();
		}

		public virtual void MoveProjectile()
		{
			_rigidBody.velocity = transform.forward * bulletSpeed;
		}

		public virtual void RotateProjectile()
		{
			//_rigidBody.MoveRotation(Quaternion.LookRotation(projectileTarget));
		}

		protected virtual void PredictMovement(float leadTimePercentage)
		{
			var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);
			_standardPrediction = target.position + target.controller.velocity * predictionTime;
		}

		protected virtual void AddDeviation(float leadTimePercentage)
		{
			var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);
			var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;
			_deviatedPrediction = _standardPrediction + predictionOffset;
		}

		private void OnCollisionEnter(Collision collision)
		{
			//Check for Collided Character damage function and deal damage
			if (collision == null)
			{
				//collision.GetComponent<>();
			}

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

