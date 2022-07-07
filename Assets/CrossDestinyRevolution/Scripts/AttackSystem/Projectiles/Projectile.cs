using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MovementSystem;
using CDR.ObjectPoolingSystem;
using CDR.MechSystem;
using CDR.VFXSystem;
using CDR.AudioSystem;


namespace CDR.AttackSystem
{
	[RequireComponent(typeof(AudioSource))]
	public class Projectile : ProjectileController, IProjectile
	{
		IPool _pool;

		[SerializeField] protected ObjectPooling ProjectileHitVFX;

		[SerializeField] public HitBox projectileHitBox;

		[SerializeField] AudioClipPreset audioClipPreset;

		public float projectileDamage;

		public bool hasLifeTime = true;
		public float projectileLifetime;
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

		protected AudioSource audioSource;

		public virtual void Start()
		{
			projectileController = GetComponent<ProjectileController>();
			projectileLifetime = projectileMaxLifetime;

			audioSource = GetComponent<AudioSource>();

			if (ProjectileHitVFX != null)
			{
				ProjectileHitVFX.Initialize();
			}
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

					if (projectileHitBox != null)
					{
						projectileHitBox.onHitEnter -= OnHitEnter;
					}

					Return();
				}
			}
		}

		protected bool LifetimeCountDown(float deltaTime)
		{
			projectileLifetime = Mathf.Max(projectileLifetime - deltaTime, 0f);
			return projectileLifetime <= 0f;
		}

		protected virtual void OnHitEnter(IHitData hitData) //Hitbox Response
		{
			if (ProjectileHitVFX != null)
			{
				var projectileHitVFX = ProjectileHitVFX.GetPoolable();

				projectileHitVFX.transform.position = transform.position;

				if (projectileHitVFX.transform.position == transform.position)
				{
					projectileHitVFX.SetActive(true);
					projectileHitVFX.GetComponent<HitGunVFXPoolable>().PlayVFX();
				}
			}

			audioClipPreset.PlayOneShot(audioSource);

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
