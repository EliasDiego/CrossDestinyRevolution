using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.AnimationSystem;

namespace CDR.AttackSystem
{
    public class CosmicStorm : SpecialAttack
    {
        [SerializeField]
        private SFXAnimationEvent[] sfx;
        [SerializeField]
        private VFXSystem.CosmicStormVFXHandler vfxHandler;

        protected override void Awake()
        {
            if (_pool[0] != null)
                _pool[0].Initialize();

            Character.animator.GetComponent<AnimationEventsManager>().AddAnimationEvent("SAttack3", sfx);
        }
        public override void Use()
        {
            base.Use();
            Character.animator.SetInteger("ActionType", (int)ActionType.SpecialAttack3);
            Fire();
        }

        public override void End()
        {
            base.End();
            vfxHandler.Deactivate();
            
            ForceEnd();
        }

        public override void ForceEnd()
        {
            base.ForceEnd();
            
            Character.animator.SetInteger("ActionType", (int)ActionType.None);
        }

        public override void Stop()
        {
            base.Stop();
            
            vfxHandler.Deactivate();
        }

        private void Fire()
        {
            var cluster = _pool[0].GetPoolable();
            if(cluster != null)
            {
                vfxHandler.Activate();
                LeanTween.delayedCall(0.24f, () =>
                {
                    var targetDir = -activeCharacter.targetHandler.GetCurrentTarget().direction;

                    cluster.GetComponent<CosmicBulletCluster>().Init(Character, bulletSpawnPoint[0].transform.position, targetDir);
                        End();
                });
            }
        }
    }
}
