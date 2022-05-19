using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
	public class Projectile : MonoBehaviour, IProjectile
	{
		Collider projectileHitbox;
		public float projectileLifetime;

		public Collider HitBox => projectileHitbox;

		public float Lifetime => projectileLifetime;

		public void Start()
		{
			projectileHitbox = GetComponent<Collider>();
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
				//Destroy(this);
			}
		}

		bool LifetimeCountDown(float deltaTime)
		{
			projectileLifetime = Mathf.Max(projectileLifetime - deltaTime, 0f);
			return projectileLifetime <= 0f;
		}
	}

}
