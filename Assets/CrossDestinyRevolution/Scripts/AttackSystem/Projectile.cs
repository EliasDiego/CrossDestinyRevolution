using CDR.MovementSystem;
using CDR.ObjectPoolingSystem;
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

		public IController controller => throw new System.NotImplementedException();
		public IPool pool => throw new System.NotImplementedException();

		public virtual void Start()
		{
			projectileHitbox = GetComponent<Collider>();
			
		}

		public virtual void Update()
		{
			//projectileTarget = GetComponent<RangeAttack>().TargetPoint.position;
			ProcessLifetime();
		}

		void ProcessLifetime()
		{
			float deltaTime = Time.deltaTime;

			if (LifetimeCountDown(deltaTime))
			{
				//wait for animation before destroy, or just disable for object pooling
				Destroy(this.gameObject);
			}
		}

		bool LifetimeCountDown(float deltaTime)
		{
			projectileLifetime = Mathf.Max(projectileLifetime - deltaTime, 0f);
			return projectileLifetime <= 0f;
		}

		public void ResetObject()
		{
			throw new System.NotImplementedException();
		}

		public void Return()
		{
			throw new System.NotImplementedException();
		}
	}

}
