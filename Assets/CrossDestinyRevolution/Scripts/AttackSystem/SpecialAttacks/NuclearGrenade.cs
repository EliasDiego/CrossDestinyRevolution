using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.AnimationSystem;

namespace CDR.AttackSystem
{
    public class NuclearGrenade : SpecialAttack
    {
        [SerializeField] int amountOfBullets;

        [SerializeField] float minDistanceFromTarget = 5f;
        [SerializeField] float maxDistanceFromTarget = 25f;

        AnimationEventsManager _Manager;


        protected override void Awake()
        {
            if (_pool[0] != null)
                _pool[0].Initialize();

            _Manager = Character.animator.GetComponent<AnimationEventsManager>();

            var a = new CDR.AnimationSystem.AnimationEvent(0.09f, true, () => StartCoroutine(NGSequence()), null, null) ;
            var b = new CDR.AnimationSystem.AnimationEvent(0.32f, true, () => End(), null, null);

            _Manager.AddAnimationEvent("SAttack1", a, b); // SpecialAttack01
        }

        public override void Use()
        {
            base.Use();

            Character.animator.SetBool("IsSAttack1", true);
        }

        public override void End()
        {
            base.End();

            Character.animator.SetBool("IsSAttack1", false);
            //ForceEnd();
        }

		public override void ForceEnd()
		{
			base.ForceEnd();

            StopAllCoroutines();
		}

		IEnumerator NGSequence()
		{
            //1ST PHASE

            var target = Character.targetHandler.GetCurrentTarget();

            var FirstPhaseBullets = new List<GameObject>();

			for (int i = 0; i < amountOfBullets; i++) //Set Random positions based on target's position
			{
                var randomPosMax = target.activeCharacter.position + Random.onUnitSphere * maxDistanceFromTarget;
                var randomPosMin = target.activeCharacter.position + Random.onUnitSphere * minDistanceFromTarget;

                Vector3 staticPositions = new Vector3(
                    Random.Range(randomPosMin.x, randomPosMax.x),
                    Random.Range(randomPosMin.y, randomPosMax.y),
                    Random.Range(randomPosMin.z, randomPosMax.z));

                var firstPhaseBullets = _pool[0].GetPoolable();

                FirstPhaseBullets.Add(firstPhaseBullets);  //Pool of NG Bullets

                firstPhaseBullets.GetComponent<NGProjectile>().targetPoint = staticPositions;
                firstPhaseBullets.GetComponent<NGProjectile>().transform.position = bulletSpawnPoint[0].transform.position;
                firstPhaseBullets.GetComponent<NGProjectile>().hasLifeTime = false;

                firstPhaseBullets.SetActive(true);
            }

            yield return new WaitUntil(() => CheckBulletPosition(FirstPhaseBullets));

            //2ND PHASE

            foreach (GameObject firstPhaseBullets in FirstPhaseBullets)
			{
                var targetDir = Character.targetHandler.GetCurrentTarget().activeCharacter.position - firstPhaseBullets.transform.position;

                if (firstPhaseBullets.activeInHierarchy)
				{
                    firstPhaseBullets.GetComponent<NGProjectile>().targetPlayerDir = Quaternion.LookRotation(targetDir);
                    firstPhaseBullets.GetComponent<NGProjectile>().secondPhaseStart = true;
                    firstPhaseBullets.GetComponent<NGProjectile>().hasLifeTime = true;

                    //firstPhaseBullets.GetComponent<NGProjectile>().ResetObject();
                    //firstPhaseBullets.GetComponent<NGProjectile>().Return();
                }
            }

            yield break;
        }

        bool CheckBulletPosition(List<GameObject> FirstPhaseBullets)
		{
            foreach(GameObject firstPhaseBullets in FirstPhaseBullets)
			{
                if (firstPhaseBullets.GetComponent<NGProjectile>().isInPosition == false && firstPhaseBullets.activeInHierarchy)
                {
                    return false;
                }
            }

            return true;
        }
	}
}


