using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ObjectPoolingSystem;

namespace CDR.AttackSystem
{
    public class CosmicBulletCluster : Projectile
    {
        [SerializeField]
        private GameObject[] projectiles;
        [SerializeField]
        private Transform rotator;
        [SerializeField]
        private float radius = 1f;
        [SerializeField]
        private float rotateSpeed = 5f;
        [SerializeField]
        private float flightSpeed = 4f;

        public override void OnEnable()
        {
            base.OnEnable();
            transform.parent = null;
            SetPosition();          
        }

        public override void Update()
        {
            base.Update();
            Rotate();
            transform.Translate(transform.forward * Time.deltaTime * flightSpeed);
        }

        protected override void OnHitEnter(IHitEnterData hitData)
        {
            Debug.Log("HIT");
        }

        public void Init(Vector3 spawnPos = default, Vector3 dir = default)
        {
            transform.position = spawnPos;
            transform.forward = dir;
            gameObject.SetActive(true);
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
            rotator.Rotate(Vector3.up, rotateSpeed);
        }

        public override void ResetObject()
        {
            base.ResetObject();
            SetVelocity(Vector3.zero);
        }
    }
}
