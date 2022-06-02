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
		[SerializeField] bool m_attack;
		[SerializeField] HitBox projectileHitbox;
		public float projectileDamage;

		public float projectileLifetime;
		

		IProjectileController projectileController;
		public IActiveCharacter target { get; set; }

		//Increments
		public HitBox HitBox => projectileHitbox;
		public float Lifetime => projectileLifetime;
		float IProjectile.Damage => projectileDamage;
		public IController controller => projectileController;
		public IPool pool => throw new System.NotImplementedException();

		float IHitResponder.Damage => projectileDamage;

		public virtual void Start()
		{
			projectileController = GetComponent<ProjectileController>();
			projectileHitbox.HitResponder = this;
			//projectileHitbox.setResponder(this);
		}

		public virtual void Update()
		{
			ProcessLifetime();
			projectileHitbox.CheckHit();
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

		//Hitbox CheckHit and Response
		public bool CheckHit(HitData data)
		{
			return true;
		}

		public void Response(HitData data)
		{
			Destroy(gameObject); //To Change in Object pooling
			//throw new System.NotImplementedException();
		}
	}

}
