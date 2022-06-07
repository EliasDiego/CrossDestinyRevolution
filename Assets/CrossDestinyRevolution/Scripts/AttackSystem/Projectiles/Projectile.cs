using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MovementSystem;
using CDR.ObjectPoolingSystem;
using CDR.MechSystem;
using CDR.HitboxSystem;


namespace CDR.AttackSystem
{
	public class Projectile : MonoBehaviour, IProjectile, IHitResponder
	{
		[SerializeField] IPool _pool;

		[SerializeField] HitBox projectileHitBox;
		[SerializeField] HitSphere projectileHitSphere;

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
		public HitSphere HitSphere => projectileHitSphere;
		public float Lifetime => projectileMaxLifetime;
		float IProjectile.Damage => projectileDamage;
		public IController controller => projectileController;

		public IPool pool { get => _pool; set => _pool = value; }

		float IHitResponder.Damage => projectileDamage;

		public virtual void Start()
		{
			projectileController = GetComponent<ProjectileController>();

			_rigidBody = GetComponent<Rigidbody>();

			projectileLifetime = projectileMaxLifetime;

			if (projectileHitBox != null)
			{
				projectileHitBox.HitResponder = this;
			}

			if (projectileHitSphere != null)
			{
				projectileHitSphere.HitResponder = this;
			}
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

			if (projectileHitBox != null)
			{
				projectileHitBox.CheckHit();
			}

			if (projectileHitSphere != null)
			{
				projectileHitSphere.CheckHit();
			}
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

		public bool CheckHit() //Hitbox CheckHit 
		{
			return true;
		}

		public void Response() //Hitbox Response
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
