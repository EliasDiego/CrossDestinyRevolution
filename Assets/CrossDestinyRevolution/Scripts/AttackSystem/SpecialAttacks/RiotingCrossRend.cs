using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.AnimationSystem;

namespace CDR.AttackSystem
{
    public class RiotingCrossRend : SpecialAttack
    {
        [SerializeField]
        private SFXAnimationEvent[] sfx;
        //[SerializeField]
        //private VFXSystem.RiotCrossRendVFXHandler vfxHandler;
        [Header("Unique properties")]
        [SerializeField]
        private Transform pivot;
        [SerializeField]
        private float rotationSpeed = 40f;
        [SerializeField]
        private float maxLength = 30f;
        [SerializeField]
        private float scaleTime = 1f;

        private RCRBullet activeBullet;
        private float targetY;
        private bool enableRotation = false;

        protected override void Awake()
        {
            if (_pool[0] != null)
                _pool[0].Initialize();

            Character.animator.GetComponent<AnimationEventsManager>().AddAnimationEvent("SAttack3", sfx);
        }

        public override void Use()
        {
            base.Use();
            //vfxHandler.Activate();
            Character.animator.SetInteger("ActionType", (int)ActionType.SpecialAttack3);
            Fire();
        }

        public override void End()
        {
            base.End();
            //vfxHandler.Deactivate();
            Character.animator.SetInteger("ActionType", (int)ActionType.None);
        }

        public override void ForceEnd()
        {
            base.ForceEnd();
            //vfxHandler.Deactivate();
            Character.animator.SetInteger("ActionType", (int)ActionType.None);
        }

        private void Fire()
        {
            var bullet = _pool[0].GetPoolable();
            if (bullet != null)
            {
                Vector3 dir = -Character.targetHandler.GetCurrentTarget().direction;
                bullet.transform.forward = dir;
                bullet.GetComponent<RCRBullet>().Init(Character.position);
                bullet.transform.parent = pivot;
                bullet.SetActive(true);
                activeBullet = bullet.GetComponent<RCRBullet>();
                StartCoroutine(RiotRoutine());
            }
        }

        private IEnumerator RiotRoutine()
        {
            targetY = pivot.localEulerAngles.y - 180f;
            var startRotate = false;

            LeanTween.scale(activeBullet.Beam, Vector3.one, 0.35f).setOnComplete(
                () =>
                {
                    LeanTween.scaleZ(activeBullet.Pivot, maxLength, scaleTime).setOnComplete(
                        () =>
                        {
                            startRotate = true;
                        }
                        );
                });

            while (true)
            {
                if(startRotate)
                {
                    pivot.RotateAround(transform.position, transform.up, -rotationSpeed * Time.deltaTime);

                    if (Mathf.Round(pivot.localEulerAngles.y) == 270f)
                    {
                        End();
                        activeBullet.ResetObject();
                        break;
                    }                    
                    yield return null;
                }

                yield return null;
            }
        }
    }
}
