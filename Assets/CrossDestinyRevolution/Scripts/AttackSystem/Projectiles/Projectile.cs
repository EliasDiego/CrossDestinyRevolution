using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MovementSystem;
using CDR.ObjectPoolingSystem;
using CDR.MechSystem;


namespace CDR.AttackSystem
{
	public class Projectile : ProjectileController, IProjectile
	{
		IPool _pool;

		[SerializeField] public HitBox projectileHitBox;

		public float projectileDamage;

		[SerializeField] bool hasLifeTime = true;
		float projectileLifetime;
		public float projectileMaxLifetime;

		IProjectileController projectileController;
		public IActiveCharacter target { get; set; }

		public float playerAttackRange;
		protected float distanceFromTarget;

		public Vector3 originPoint;

		public Vector3 staticTargetPoint;

		//Increments
		public HitBox HitBox => projectileHitBox;
		public float Lifetime => projectileMaxLifetime;
		float IProjectile.Damage => projectileDamage;
		public IController controller => projectileController;

		public IPool pool { get => _pool; set => _pool = value; }

		public virtual void Start()
		{
			projectileController = GetComponent<ProjectileController>();
			projectileLifetime = projectileMaxLifetime;
		}

		public virtual void OnEnable()
		{
			transform.position = originPoint;
			projectileLifetime = projectileMaxLifetime;

			if (projectileHitBox != null)
			{
				projectileHitBox.onHitEnter += OnHitEnter;
			}
		}

		public virtual void Update()
		{
			ProcessLifetime();
		}

		void ProcessLifetime()
		{
			if(hasLifeTime)
			{
				float deltaTime = Time.deltaTime;

				if (LifetimeCountDown(deltaTime))
				{
					ResetObject();

					projectileHitBox.onHitEnter -= OnHitEnter;

					Return();
				}
			}
		}

		bool LifetimeCountDown(float deltaTime)
		{
			projectileLifetime = Mathf.Max(projectileLifetime - deltaTime, 0f);
			return projectileLifetime <= 0f;
		}

		public void OnHitEnter(IHitEnterData hitData) //Hitbox Response
		{ 
			hitData.hurtShape.character.health.TakeDamage(projectileDamage);

			ResetObject();

			projectileHitBox.onHitEnter -= OnHitEnter;

			Return();
		}

		public void ResetObject() //Parameters reset
		{
			projectileLifetime = projectileMaxLifetime;
			originPoint = Vector3.zero;
			transform.rotation = Quaternion.identity;
			distanceFromTarget = 0f;
			Rotate(Quaternion.identity);
		}

		public void Return() //Return to Object Pool
		{
			pool.ReturnObject(this);
		}
	}

}
