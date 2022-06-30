using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class RCRBullet : Projectile
    {
        [Header("Unique properties")]
        [SerializeField]
        private GameObject pivot;
        [SerializeField]
        private GameObject beam;
        [SerializeField]
        private float maxLength;
        [SerializeField]
        private float timeToScale;
        [SerializeField]
        private float rotationSpeed;

        private Vector3 beamPos;
        private bool enableRotation = false;
        private float targetY;
        private bool isHit = false;
        private MechSystem.Health hpToDamage;

        private void Awake()
        {
            HitBox.onHitEnter += OnHitEnter;
            HitBox.onHitExit += OnHitExit;

            beamPos = beam.transform.localPosition;
        }

        private void OnDestroy()
        {       
            HitBox.onHitEnter -= OnHitEnter;            
            HitBox.onHitExit -= OnHitExit;
        }

        public override void Update()
        {
            base.Update();
            Rotate();
            HitPlayer();
        }

        private void OnHitEnter(IHitData data)
        {
            if(!isHit)
            {
                isHit = true;
                hpToDamage = (MechSystem.Health)data.hurtShape.character.health;
            }
        }

        private void OnHitExit(IHitData data)
        {
            if(isHit)
            {
                isHit = false;
                hpToDamage = null;
            }
        }

        public void Init(Vector3 pos = default)
        {
            transform.position = pos;
            pivot.transform.localScale = Vector3.one;
            beam.transform.localScale = Vector3.zero;
            gameObject.SetActive(true);
            ScaleWithTime();
        }

        private void HitPlayer()
        {
            if(isHit && hpToDamage != null)
            {
                hpToDamage.TakeDamage(projectileDamage);
            }
        }

        public override void ResetObject()
        {
            base.ResetObject();
            enableRotation = false;
        }

        private void ScaleWithTime()
        {
            targetY = transform.localEulerAngles.y - 180f;

            LeanTween.scale(beam, Vector3.one, 0.45f)
            .setOnComplete(()=>
            {
                LeanTween.scaleZ(pivot, maxLength, timeToScale)
                .setOnComplete(() =>
                {
                    enableRotation = true;
                });
            });
        }

        private void Rotate()
        {
            if (enableRotation)
            {
                transform.RotateAround(transform.position, transform.up, -rotationSpeed * Time.deltaTime);
                AddRbForce(new Vector3(0f, 0f, 0.001f), ForceMode.Acceleration);
            }
            if (Mathf.Floor(targetY - transform.localEulerAngles.y) == 0f)
            {
                gameObject.SetActive(false);
                ResetObject();
            }
        }
    }
}
