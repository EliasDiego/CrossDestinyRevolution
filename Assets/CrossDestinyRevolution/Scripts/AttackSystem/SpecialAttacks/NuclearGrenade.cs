using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class NuclearGrenade : SpecialAttack
    {
        [SerializeField] int amountOfBullets;

        [SerializeField] float minDistanceFromTarget = 5f;
        [SerializeField] float maxDistanceFromTarget = 25f;


        protected override void Awake()
        {
            if (_pool[0] != null)
                _pool[0].Initialize();
        }

        public override void Use()
        {
            base.Use();

            StartCoroutine(NGSequence());
        }

        public override void End()
        {
            base.End();
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

            End();

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


