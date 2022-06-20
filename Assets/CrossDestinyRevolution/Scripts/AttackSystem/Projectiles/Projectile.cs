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

		public bool hasLifeTime = true;
		protected float projectileLifetime;
		public float projectileMaxLifetime;

		IProjectileController projectileController;
		public IActiveCharacter target { get; set; }
		protected float distanceFromTarget;

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

		protected virtual void ProcessLifetime()
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

		protected bool LifetimeCountDown(float deltaTime)
		{
			projectileLifetime = Mathf.Max(projectileLifetime - deltaTime, 0f);
			return projectileLifetime <= 0f;
		}

		protected virtual void OnHitEnter(IHitEnterData hitData) //Hitbox Response
		{
			hitData.hurtShape.character.health.TakeDamage(projectileDamage);

			ResetObject();

			projectileHitBox.onHitEnter -= OnHitEnter;

			Return();
		}

		public virtual void ResetObject() //Parameters reset
		{
			projectileLifetime = projectileMaxLifetime;
			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;
			distanceFromTarget = 0f;
			transform.parent = null;

			SetVelocity(Vector3.zero);
			Rotate(Quaternion.identity);
		}

		public void Return() //Return to Object Pool
		{
			pool.ReturnObject(this);
		}
	}

}
