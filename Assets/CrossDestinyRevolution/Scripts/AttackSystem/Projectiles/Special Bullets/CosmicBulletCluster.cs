using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ObjectPoolingSystem;
using CDR.VFXSystem;

namespace CDR.AttackSystem
{
    public class CosmicBulletCluster : Projectile
    {
        [SerializeField]
        private HitShape[] projectiles;
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
            RotateToSelf();
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

        public void Init(MechSystem.IActiveCharacter owner, Vector3 spawnPos = default, Vector3 dir = default)
        {
            transform.position = spawnPos;
            flightDir = dir;
            gameObject.SetActive(true);
            this.owner = owner;
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
            rotator.Rotate(Vector3.right, rotateSpeed * Time.timeScale);
        }

        private void RotateToSelf()
        {
            rotator.right = flightDir;
        }

        public override void ResetObject()
        {
            gameObject.SetActive(false);
            base.ResetObject();
            SetVelocity(Vector3.zero);
            for(int i = 0; i < projectiles.Length; i++)
            {
                projectiles[i].gameObject.SetActive(true);
                SetPosition();
            }
        }

        private void SubscribeEvents()
        {
            for(int i = 0; i < projectiles.Length; i++)
            {
                projectiles[i].onHitEnter += OnHitEvent;
            }
        }

        private void UnsubscribeEvents()
        {
            for (int i = 0; i < projectiles.Length; i++)
            {
                projectiles[i].onHitEnter -= OnHitEvent;
            }
        }

        private void OnHitEvent(IHitData data)
        {
            if (ProjectileHitVFX != null)
            {
                var projectileHitVFX = ProjectileHitVFX.GetPoolable();
                projectileHitVFX.transform.position = data.hitShape.collider.transform.position;
                projectileHitVFX.SetActive(true);
                projectileHitVFX.GetComponent<HitGunVFXPoolable>().PlayVFX();
            }

            var audioClip = audioClipPool.GetPoolable();
            audioClip.SetActive(true);
            audioClip.GetComponent<AudioSourcePoolable>().PlayAudio(audioClipPreset);

            data.hitShape.collider.gameObject.SetActive(false);
            data.hurtShape.character.health.TakeDamage(projectileDamage);
            if (ActiveProjectiles() == 0)
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
