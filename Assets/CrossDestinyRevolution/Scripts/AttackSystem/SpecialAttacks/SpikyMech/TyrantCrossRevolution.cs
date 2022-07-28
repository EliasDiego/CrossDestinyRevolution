using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.AnimationSystem;
using CDR.VFXSystem;

namespace CDR.AttackSystem
{
    public class TyrantCrossRevolution : SpecialAttack
    {
        [SerializeField] int amountOfLeftBullets;
        [SerializeField] int amountOfRightBullets;

        [SerializeField] float bulletSpawnSpacing;
        [SerializeField] float bulletStaticPosSpacing;

        [SerializeField] CDR.AnimationSystem.AnimationEvent _animationEvent;

        AnimationEventsManager _Manager;

        [SerializeField] SFXAnimationEvent[] sfxAnimationEvents;

        [SerializeField] TyrantCrossRevolutionVFXHandler tyrantCrossRevolutionVFXHandler;

        protected override void Awake()
        {
            if (_pool[0] != null)
                _pool[0].Initialize();

            _Manager = Character.animator.GetComponent<AnimationEventsManager>();

            var a = new CDR.AnimationSystem.AnimationEvent(0.33f, true, () => StartCoroutine(TCRSequence()));
            var b = new CDR.AnimationSystem.AnimationEvent(1f, true, () => End());

            _Manager.AddAnimationEvent("SAttack3", a, b); //SpecialAttack01
            _Manager.AddAnimationEvent("SAttack3", sfxAnimationEvents); //SpecialAttack01
        }

        public override void Use()
        {
            base.Use();

            Character.animator.SetInteger("ActionType", (int)ActionType.SpecialAttack3);

        }

        public override void End()
        {
            base.End();
            //ForceEnd();

            Character.animator.SetInteger("ActionType", (int)ActionType.None);
        }

        public override void ForceEnd()
        {
            base.ForceEnd();

            Character.animator.SetInteger("ActionType", (int)ActionType.None);

            tyrantCrossRevolutionVFXHandler.Deactivate();

            StopAllCoroutines();
        }

        public override void Stop()
        {
            base.Stop();

            ForceEnd();
        }

        IEnumerator TCRSequence()
		{
            var target = Character.targetHandler.GetCurrentTarget();

            Vector3 middle = Vector3.Lerp(Character.position, target.activeCharacter.position, 0.5f);

            var FirstPhaseBullets = new List<GameObject>();

            //Left Bullets Initialize
            for (int i = 1; i <= amountOfLeftBullets; i++) 
            {
                Vector3 staticPositions = middle + (bulletStaticPosSpacing * i * transform.right) + (bulletStaticPosSpacing * i * transform.forward); //right

                var firstPhaseLeftBullets = _pool[0].GetPoolable();

                FirstPhaseBullets.Add(firstPhaseLeftBullets);  //Pool of NG Bullets

                firstPhaseLeftBullets.GetComponent<TCRProjectile>().targetPoint = staticPositions;

                firstPhaseLeftBullets.GetComponent<TCRProjectile>().transform.position = transform.position + (bulletSpawnSpacing * i * -transform.right);

                firstPhaseLeftBullets.GetComponent<TCRProjectile>().hasLifeTime = false;

                firstPhaseLeftBullets.SetActive(true);
            }
            
            //Right Bullets Initialize
            for (int i = 1; i <= amountOfRightBullets; i++) 
            {
                Vector3 staticPositions = middle + (bulletStaticPosSpacing * i * -transform.right) + (bulletStaticPosSpacing * i * transform.forward); //Left

                var firstPhaseLeftBullets = _pool[0].GetPoolable();

                FirstPhaseBullets.Add(firstPhaseLeftBullets);  //Pool of NG Bullets

                firstPhaseLeftBullets.GetComponent<TCRProjectile>().targetPoint = staticPositions;

                firstPhaseLeftBullets.GetComponent<TCRProjectile>().transform.position = transform.position + (bulletSpawnSpacing * i * transform.right);

                firstPhaseLeftBullets.GetComponent<TCRProjectile>().hasLifeTime = false;

                firstPhaseLeftBullets.SetActive(true);
            }

            tyrantCrossRevolutionVFXHandler.Activate();

            yield return new WaitUntil(() => CheckBulletPosition(FirstPhaseBullets));

            //2ND PHASE

            foreach (GameObject firstPhaseBullets in FirstPhaseBullets)
            {
                var targetDir = Character.targetHandler.GetCurrentTarget().activeCharacter.position - firstPhaseBullets.transform.position;

                if (firstPhaseBullets.activeInHierarchy)
                {
                    firstPhaseBullets.GetComponent<TCRProjectile>().targetPlayerDir = Quaternion.LookRotation(targetDir);
                    firstPhaseBullets.GetComponent<TCRProjectile>().secondPhaseStart = true;
                    firstPhaseBullets.GetComponent<TCRProjectile>().hasLifeTime = true;
                }
            }

            yield break;
		}

        bool CheckBulletPosition(List<GameObject> FirstPhaseBullets)
        {
            foreach (GameObject firstPhaseBullets in FirstPhaseBullets)
            {
                if (firstPhaseBullets.GetComponent<TCRProjectile>().isInPosition == false && firstPhaseBullets.activeInHierarchy)
                {
                    return false;
                }
            }

            return true;
        }
    }
}



