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
		Collider projectileHitbox;
		public float projectileLifetime;
		public float projectileDamage;

		IProjectileController projectileController;
		public IActiveCharacter target { get; set; }

		//Increments
		public Collider HitBox => projectileHitbox;
		public float Lifetime => projectileLifetime;
		public float Damage => projectileDamage;
		public IController controller => projectileController;
		public IPool pool => throw new System.NotImplementedException();

		public virtual void Start()
		{
			projectileHitbox = GetComponent<Collider>();
			projectileController = GetComponent<ProjectileController>();
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

		public void ResetObject(){}

		public void Return(){}

		protected virtual void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.CompareTag("Mech"))
			{
				other.gameObject.GetComponent<ActiveCharacter>().health.TakeDamage(projectileDamage);
			}

			//Wait for animation before destroy or return to pool
			Destroy(gameObject);
		}
	}

}
