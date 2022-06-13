using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class NuclearGrenade : SpecialAttack
    {
        [SerializeField] int amountOfBullets;

        //[SerializeField] GameObject[] FirstPhaseBullets;
        //[SerializeField] GameObject[] SecondPhaseBullets;

        [SerializeField] GameObject testTarget;

        [SerializeField] float minDistanceFromTarget = 5f;
        [SerializeField] float maxDistanceFromTarget = 25f;


        protected override void Awake()
        {
            if (_pool[0] != null)
                _pool[0].Initialize();
            if (_pool[1] != null)
                _pool[1].Initialize();
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

            var FirstPhaseBullets = new GameObject[amountOfBullets];
            var SecondPhaseBullets = new GameObject[amountOfBullets];

            

			for (int i = 0; i < amountOfBullets; i++) //Set Random positions based on target's position
			{
                var randomPosMax = target.activeCharacter.position + Random.onUnitSphere * maxDistanceFromTarget;
                var randomPosMin = target.activeCharacter.position + Random.onUnitSphere * minDistanceFromTarget;

                /*Vector3 staticPositions = new Vector3(
                    Random.Range(target.activeCharacter.position.x + minDistanceFromTarget, target.activeCharacter.position.x + maxDistanceFromTarget), 
                    Random.Range(target.activeCharacter.position.y + minDistanceFromTarget, target.activeCharacter.position.y + maxDistanceFromTarget), 
                    Random.Range(target.activeCharacter.position.z + minDistanceFromTarget, target.activeCharacter.position.z + maxDistanceFromTarget));
                */

                Vector3 staticPositions = new Vector3(
                    Random.Range(randomPosMin.x, randomPosMax.x),
                    Random.Range(randomPosMin.y, randomPosMax.y),
                    Random.Range(randomPosMin.z, randomPosMax.z));

                FirstPhaseBullets[i] = _pool[0].GetPoolable(); //Pool of NG Bullets

                FirstPhaseBullets[i].GetComponent<NGProjectile>().targetPoint = staticPositions;

                FirstPhaseBullets[i].GetComponent<NGProjectile>().originPoint = bulletSpawnPoint[0].transform.position;
                
                FirstPhaseBullets[i].SetActive(true);
            }

            End();

            yield return new WaitUntil(() => CheckBulletPosition(FirstPhaseBullets));

            //2ND PHASE

            for (int i = 0; i < FirstPhaseBullets.Length; i++) //Instantiate/GetFromPool bullets
            {
                SecondPhaseBullets[i] = _pool[1].GetPoolable(); //Pool of Homing Bullets

                SecondPhaseBullets[i].GetComponent<HomingBullet>().target = target.activeCharacter;
                SecondPhaseBullets[i].GetComponent<HomingBullet>().originPoint = FirstPhaseBullets[i].transform.position;

                SecondPhaseBullets[i].SetActive(true);
            }

            foreach(GameObject firstPhaseBullets in FirstPhaseBullets)
			{
                firstPhaseBullets.GetComponent<NGProjectile>().ResetObject();
                firstPhaseBullets.GetComponent<NGProjectile>().Return();
                firstPhaseBullets.GetComponent<NGProjectile>().isInPosition = false;
            }

            yield break;
        }

        bool CheckBulletPosition(GameObject[] FirstPhaseBullets)
		{
            foreach(GameObject firstPhaseBullets in FirstPhaseBullets)
			{
                if (firstPhaseBullets.GetComponent<NGProjectile>().isInPosition == false)
                {
                    return false;
                }
            }

            /*for(int i = 0; i < FirstPhaseBullets.Length; i++)
			{
                if (FirstPhaseBullets[i].GetComponent<NGProjectile>().isInPosition == false)
				{
                    return false;
				}    
			}*/

            return true;
        }

		private void OnDrawGizmos()
		{
            if(testTarget != null)
			{
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(testTarget.transform.position, minDistanceFromTarget);

                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(testTarget.transform.position, maxDistanceFromTarget);
            }
        }
	}
}


