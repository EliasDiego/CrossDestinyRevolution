using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class HomingBullet : Projectile
    {
        [SerializeField] public float bulletSpeed = 15f;
        [SerializeField] public float rotateSpeed = 5f;

        public Vector3 originPoint;

        protected bool isHoming = true;

        float originDistanceFromProjectile; //From origin point to the current position of the projectile
        float originDistanceFromTarget; // From origin point to the current position of the target

        public override void OnEnable()
		{
            base.OnEnable();

            isHoming = true;

            if (playerAttackRange < originDistanceFromTarget)
			{
                isHoming = false;
			}

            if (target != null)
            {
                transform.LookAt(target.position);
            }
        }

        public void FixedUpdate()
        {
            if (target != null)
                distanceFromTarget = Vector3.Distance(transform.position, target.position);

            MoveProjectile();

            if (target != null)
            {
                originDistanceFromProjectile = Vector3.Distance(transform.position, originPoint);
                originDistanceFromTarget = Vector3.Distance(target.position, originPoint);
            }

            if (isHoming)
            {
                RotateProjectile();
            }

            if (originDistanceFromProjectile > originDistanceFromTarget || originDistanceFromTarget > playerAttackRange)
            {
                isHoming = false;
            }
        }

		public override void Start()
		{
            base.Start();
        }

        public void MoveProjectile()
        {
            SetVelocity(transform.forward * bulletSpeed);
		}

		public void RotateProjectile()
		{
			var heading = target.position - transform.position;
			var rotation = Quaternion.LookRotation(heading);
			Rotate(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
		}

        protected override void OnHitEnter(IHitEnterData hitData)
        {
            base.OnHitEnter(hitData);

            hitData.hurtShape.character.health.TakeDamage(projectileDamage);

            ResetObject();

            projectileHitBox.onHitEnter -= OnHitEnter;

            Return();
        }
    }
}

