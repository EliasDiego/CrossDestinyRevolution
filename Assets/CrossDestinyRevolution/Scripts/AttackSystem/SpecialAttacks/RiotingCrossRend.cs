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
        [SerializeField]
        private VFXSystem.RCRLaserVFXHandler laserVFX;
        [SerializeField]
        private VFXSystem.RCRMuzzleFlashVFXHandler muzzleVFX;
        [Header("Unique properties")]
        [SerializeField]
        private Transform pivot;
        [SerializeField]
        private float rotTime = 5f;
        [SerializeField]
        private float maxLength = 30f;
        [SerializeField]
        private float scaleTime = 1f;

        private RCRBullet activeBullet;

        protected override void Awake()
        {
            if (_pool[0] != null)
                _pool[0].Initialize();

            Character.animator.GetComponent<AnimationEventsManager>().AddAnimationEvent("SAttack2", sfx);
            laserVFX.transform.parent = pivot;
            laserVFX.length = 1f;
        }

        public override void Use()
        {
            base.Use();            
            Character.animator.SetInteger("ActionType", (int)ActionType.SpecialAttack2);
            Fire();
        }

        public override void End()
        {
            base.End();
            laserVFX.Deactivate();
            muzzleVFX.Deactivate();
            Character.animator.SetInteger("ActionType", (int)ActionType.None);
        }

        public override void ForceEnd()
        {
            base.ForceEnd();
            laserVFX.Deactivate();
            muzzleVFX.Deactivate();
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
                ScaleAndRotateBeam();
            }
        }

        private void ScaleAndRotateBeam()
        {
            laserVFX.Activate();
            muzzleVFX.Activate();

            LeanTween.scale(activeBullet.Beam, Vector3.one, 0.35f).setOnComplete(
                () =>
                {
                    LeanTween.value(1f, 11f, scaleTime).setOnUpdate(
                    (float f) =>
                    {
                        laserVFX.length = f;
                    });
                    LeanTween.scaleZ(activeBullet.Pivot, maxLength, scaleTime).setOnComplete(
                        () =>
                        {
                            LeanTween.rotateAroundLocal(pivot.gameObject, transform.up, -180f, rotTime).setOnComplete(
                                ()=>
                                {
                                    End();
                                    LeanTween.delayedCall(2f, () =>
                                    {
                                        pivot.localEulerAngles = new Vector3(0f, 90f, 0f);
                                        laserVFX.length = 1f;
                                    });
                                });                            
                        }
                        );
                });
            
        }
    }
}
