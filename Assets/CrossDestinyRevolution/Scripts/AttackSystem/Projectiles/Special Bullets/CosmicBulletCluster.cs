using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ObjectPoolingSystem;

namespace CDR.AttackSystem
{
    public class CosmicBulletCluster : Projectile
    {
        [SerializeField]
        private HitBox[] projectiles;
        [SerializeField]
        private Transform rotator;
        [SerializeField]
        private float radius = 1f;
        [SerializeField]
        private float rotateSpeed = 5f;
        [SerializeField]
        private float flightSpeed = 4f;

        private Vector3 flightDir;

        private void Awake()
        {
            SetPosition();
            SubscribeEvents();
        }

        public override void OnEnable()
        {
            base.OnEnable();           
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();   
        }

        public override void Update()
        {
            base.Update();
            Rotate();
            transform.Translate(flightDir * Time.deltaTime * flightSpeed);
        }

        private void FixedUpdate()
        {
            AddRbForce(Vector3.forward);
            ClampVelocity(0.01f);
        }

        public void Init(Vector3 spawnPos = default, Vector3 dir = default)
        {
            transform.position = spawnPos;
            flightDir = dir;
            gameObject.SetActive(true);
        }

        private void SetPosition()
        {
            projectiles[0].transform.localPosition = transform.localPosition + transform.up * radius;
            projectiles[1].transform.localPosition = transform.localPosition + transform.forward * radius;
            projectiles[2].transform.localPosition = transform.localPosition + -transform.up * radius;
            projectiles[3].transform.localPosition = transform.localPosition + -transform.forward * radius;
        }

        private void Rotate()
        {
            rotator.Rotate(Vector3.right, rotateSpeed);
        }

        public override void ResetObject()
        {
            gameObject.SetActive(false);
            base.ResetObject();
            SetVelocity(Vector3.zero);
        }

        private void SubscribeEvents()
        {
            projectiles[0].onHitEnter += OnHit1;
            projectiles[1].onHitEnter += OnHit2;
            projectiles[2].onHitEnter += OnHit3;
            projectiles[3].onHitEnter += OnHit4;
        }

        private void UnsubscribeEvents()
        {
            projectiles[0].onHitEnter -= OnHit1;
            projectiles[1].onHitEnter -= OnHit2;
            projectiles[2].onHitEnter -= OnHit3;
            projectiles[3].onHitEnter -= OnHit4;
        }

        private void OnHit1(IHitEnterData data)
        {
            projectiles[0].gameObject.SetActive(false);
            HitDamage((MechSystem.Health)data.hurtShape.character.health);
        }

        private void OnHit2(IHitEnterData data)
        {
            projectiles[1].gameObject.SetActive(false);
            HitDamage((MechSystem.Health)data.hurtShape.character.health);
        }

        private void OnHit3(IHitEnterData data)
        {
            projectiles[2].gameObject.SetActive(false);
            HitDamage((MechSystem.Health)data.hurtShape.character.health);
        }

        private void OnHit4(IHitEnterData data)
        {
            projectiles[3].gameObject.SetActive(false);
            HitDamage((MechSystem.Health)data.hurtShape.character.health);
        }

        private void HitDamage(MechSystem.Health hp)
        {
            hp.TakeDamage(projectileDamage);
            if(ActiveProjectiles() == 0)
            {
                ResetObject();
            }
        }

        private int ActiveProjectiles()
        {
            var count = 4;
            for(int i = 0; i < projectiles.Length; i++)
            {
                if(!projectiles[i].gameObject.activeInHierarchy)
                {
                    count--;
                }
            }
            return count;
        }
    }
}
