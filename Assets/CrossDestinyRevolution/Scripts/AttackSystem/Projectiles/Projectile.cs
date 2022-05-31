using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MovementSystem;
using CDR.ObjectPoolingSystem;
using CDR.MechSystem;
using CDR.HitboxSystem;


namespace CDR.AttackSystem
{
	public class Projectile : MonoBehaviour, IProjectile, IHitboxResponder
	{
		HitBox projectileHitbox;
		public float projectileLifetime;
		public float projectileDamage;

		IProjectileController projectileController;
		public IActiveCharacter target { get; set; }

		//Increments
		public HitBox HitBox => projectileHitbox;
		public float Lifetime => projectileLifetime;
		public float Damage => projectileDamage;
		public IController controller => projectileController;
		public IPool pool => throw new System.NotImplementedException();

		public virtual void Start()
		{
			projectileController = GetComponent<ProjectileController>();
			projectileHitbox = GetComponent<HitBox>();
			projectileHitbox.setResponder(this);
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
				//wait for animation before destroy, or just disable for object pooling
				Destroy(gameObject);
			}
		}

		bool LifetimeCountDown(float deltaTime)
		{
			projectileLifetime = Mathf.Max(projectileLifetime - deltaTime, 0f);
			return projectileLifetime <= 0f;
		}

		public void ResetObject() { }
		public void Return() { }

		public void collisionedWith(Collider collider)
		{
			HurtBox hurtbox = collider.GetComponent<HurtBox>();
			hurtbox?.getHitBy(projectileDamage);
			Destroy(gameObject);
		}
	}

}
