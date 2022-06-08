using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MovementSystem;
using CDR.ObjectPoolingSystem;
using CDR.MechSystem;


namespace CDR.AttackSystem
{
	public class Projectile : MonoBehaviour, IProjectile
	{
		[SerializeField] IPool _pool;

		[SerializeField] HitBox projectileHitBox;

		public float projectileDamage;
		float projectileLifetime;
		public float projectileMaxLifetime;

		protected Rigidbody _rigidBody;

		IProjectileController projectileController;
		public IActiveCharacter target { get; set; }

		public float playerAttackRange;
		protected float distanceFromTarget;

		public Vector3 originPoint;

		//Increments
		public HitBox HitBox => projectileHitBox;
		public float Lifetime => projectileMaxLifetime;
		float IProjectile.Damage => projectileDamage;
		public IController controller => projectileController;

		public IPool pool { get => _pool; set => _pool = value; }


		public virtual void Start()
		{
			projectileController = GetComponent<ProjectileController>();

			_rigidBody = GetComponent<Rigidbody>();

			projectileLifetime = projectileMaxLifetime;

			if (projectileHitBox != null)
			{
				//projectileHitBox.HitResponder = this;
			}

			//if (projectileHitSphere != null)
			//{
				//projectileHitSphere.HitResponder = this;
			//}
		}

		public virtual void OnEnable()
		{
			transform.position = originPoint;
			projectileLifetime = projectileMaxLifetime;
			
			if (target != null)
			{
				transform.LookAt(target.position); ;
			}
		}

		public virtual void Update()
		{
			ProcessLifetime();
		}

		void ProcessLifetime()
		{
			float deltaTime = Time.deltaTime;

			if (LifetimeCountDown(deltaTime))
			{
				ResetObject();
				//Return();
			}
		}

		bool LifetimeCountDown(float deltaTime)
		{
			projectileLifetime = Mathf.Max(projectileLifetime - deltaTime, 0f);
			return projectileLifetime <= 0f;
		}

		public void HitBoxResponse() //Hitbox Response
		{
			ResetObject();
		}

		public void ResetObject() //Parameters reset
		{
			gameObject.SetActive(false);
			projectileLifetime = projectileMaxLifetime;
			originPoint = Vector3.zero;
			transform.rotation = Quaternion.identity;
			distanceFromTarget = 0f;
		}

		public void Return() //Return to Object Pool
		{
			pool.ReturnObject(this);
		}
	}

}
