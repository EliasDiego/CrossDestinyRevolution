using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ObjectPoolingSystem;

namespace CDR.AttackSystem
{
    public class CosmicBulletCluster : Projectile
    {
        [SerializeField]
        private CosmicProjectile[] projectiles;
        [SerializeField]
        private float radius = 1f;
        [SerializeField]
        private float rotateSpeed = 5f;

        public override void OnEnable()
        {
            base.OnEnable();
            SetPosition();
        }

        public override void Update()
        {
            base.Update();
            Rotate();
        }

        private void SetPosition()
        {
            projectiles[0].transform.localPosition = transform.position + transform.forward * radius;
            projectiles[1].transform.localPosition = transform.position + transform.right * radius;
            projectiles[2].transform.localPosition = transform.position + -transform.forward * radius;
            projectiles[3].transform.localPosition = transform.position + -transform.right * radius;
        }


        private void Rotate()
        {
            transform.RotateAround(transform.position, transform.up, rotateSpeed);         
        }
    }
}
