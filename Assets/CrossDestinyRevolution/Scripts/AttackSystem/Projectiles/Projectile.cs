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
		string _id;
		[SerializeField] HitBox projectileHitbox;
		public float projectileDamage;
		
		float projectileLifetime;
		public float projectileMaxLifetime;

		IProjectileController projectileController;
		public IActiveCharacter target { get; set; }

		public float playerAttackRange;
		protected float distanceFromTarget;

		public Vector3 originPoint;

		//Increments
		public HitBox HitBox => projectileHitbox;
		public float Lifetime => projectileMaxLifetime;
		float IProjectile.Damage => projectileDamage;
		public IController controller => projectileController;
		float IHitResponder.Damage => projectileDamage;

		public string ID { get => _id; set => _id = value; }

		public virtual void Start()
		{
			projectileController = GetComponent<ProjectileController>();
			projectileHitbox.HitResponder = this;
			projectileLifetime = projectileMaxLifetime;
		}

		public virtual void OnEnable()
		{
			transform.position = originPoint;
			projectileLifetime = projectileMaxLifetime;

			if(target != null)
			{
				transform.rotation = Quaternion.LookRotation(target.position);
			}
			
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
				//Destroy(gameObject);
				gameObject.SetActive(false);
			}
		}

		bool LifetimeCountDown(float deltaTime)
		{
			projectileLifetime = Mathf.Max(projectileLifetime - deltaTime, 0f);
			return projectileLifetime <= 0f;
		}

		//public void ResetObject() { }
		//public void Return() { }

		//Hitbox CheckHit and Response
		public bool CheckHit(HitData data)
		{
			return true;
		}

		public void Response(HitData data)
		{
			gameObject.SetActive(false);
			//Destroy(gameObject); //To Change in Object pooling
			//throw new System.NotImplementedException();
		}

		public void ResetObject()
		{
			
		}

		public void Return()
		{
			
		}
	}

}
