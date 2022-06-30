using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
	public class Bullet : Projectile
	{
		[SerializeField] public float BulletSpeed;

		public Quaternion generalDirection;

		public override void Start()
		{
			base.Start();
		}

		public override void OnEnable()
		{
			base.OnEnable();

			transform.rotation = generalDirection;
		}

		private void FixedUpdate()
		{
			MoveBullet();
		}

		void MoveBullet()
		{
			SetVelocity(transform.forward * BulletSpeed);
		}

		protected override void OnHitEnter(IHitData hitData)
		{
			base.OnHitEnter(hitData);

			hitData.hurtShape.character.health.TakeDamage(projectileDamage);

			ResetObject();

			projectileHitBox.onHitEnter -= OnHitEnter;

			Return();
		}
	}
}

