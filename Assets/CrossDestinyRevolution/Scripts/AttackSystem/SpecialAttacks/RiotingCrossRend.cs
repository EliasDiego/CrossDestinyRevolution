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
        [SerializeField]
        private float animationTimeScale = 0.15f;

        private RCRBullet activeBullet;

        protected override void Awake()
        {
            if (_pool[0] != null)
                _pool[0].Initialize();

            var a = new AnimationSystem.AnimationEvent(1f, true, AA);

            Character.animator.GetComponent<AnimationEventsManager>().AddAnimationEvent("SAttack2", a);
            Character.animator.GetComponent<AnimationEventsManager>().AddAnimationEvent("SAttack2", sfx);
            laserVFX.length = 1f;
        }

        void AA()
        {
            Debug.Log("AA");
        }

        public override void Use()
        {
            base.Use();
            Character.animator.SetFloat("ActionSMultiplier", animationTimeScale);
            Character.animator.SetInteger("ActionType", (int)ActionType.SpecialAttack2);
            Fire();
        }

        public override void End()
        {
            base.End();
            laserVFX.length = 1f;
            laserVFX.Deactivate();
            muzzleVFX.Deactivate();            
            activeBullet.ResetObject();          
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
                bullet.transform.parent = laserVFX.transform;
                bullet.transform.localScale = Vector3.one;
                bullet.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
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
                    LeanTween.scaleZ(activeBullet.Pivot, maxLength, scaleTime);
                    LeanTween.value(1f, maxLength, scaleTime).setOnUpdate(
                    (float f) =>
                    {
                        laserVFX.length = f;
                    });
                });

            LeanTween.delayedCall(5f, () =>
            {
                End();
                LeanTween.value(Character.animator.GetFloat("ActionSMultiplier"), 1f, 1f).setOnUpdate((float f)=>
                {
                    Character.animator.SetFloat("ActionSMultiplier", f);
                })           
                .setOnComplete(()=>
                { 
                    Character.animator.SetInteger("ActionType", (int)ActionType.None);
                });
            });
        }

    }
}
