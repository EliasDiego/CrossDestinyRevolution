using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class NuclearGrenade : SpecialAttack
    {
        [SerializeField] int amountOfBullets;
        private Vector3[] staticPositions;

        GameObject[] FirstPhaseBullets;
        GameObject[] SecondPhaseBullets;

        [SerializeField] float minDistanceFromTarget = 5f;
        [SerializeField] float maxDistanceFromTarget = 25f;


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

            
            for (int i = 0; i < amountOfBullets; i++) //Set Random positions based on target's position
			{
                staticPositions[i] = new Vector3(
                    Random.Range(target.activeCharacter.position.x, maxDistanceFromTarget), 
                    Random.Range(target.activeCharacter.position.y, maxDistanceFromTarget), 
                    Random.Range(target.activeCharacter.position.z, maxDistanceFromTarget));

                FirstPhaseBullets[i] = _pool[0].GetPoolable();

                FirstPhaseBullets[i].GetComponent<NGProjectile>().targetPoint = staticPositions[i];
                FirstPhaseBullets[i].GetComponent<NGProjectile>().originPoint = bulletSpawnPoint[0].transform.position;
                
                FirstPhaseBullets[i].SetActive(true);
            }

            yield return new WaitUntil(() => CheckBulletPosition());

            End();

            //2ND PHASE

            for (int i = 0; i < amountOfBullets; i++) //Instantiate/GetFromPool bullets
            {
                SecondPhaseBullets[i].GetComponent<HomingBullet>().target = target.activeCharacter;
                SecondPhaseBullets[i].GetComponent<HomingBullet>().originPoint = FirstPhaseBullets[i].transform.position;

                SecondPhaseBullets[i].SetActive(true);

                FirstPhaseBullets[i].GetComponent<NGProjectile>().ResetObject();
                FirstPhaseBullets[i].GetComponent<NGProjectile>().Return();
            }
        }

        bool CheckBulletPosition()
		{
            for(int i = 0; i < amountOfBullets; i++)
			{
                if (FirstPhaseBullets[i].GetComponent<NGProjectile>().isInPosition == false)
				{
                    return false;
				}    
			}
            return true;
        }


    }
}


