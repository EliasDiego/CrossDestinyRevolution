using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class BigBangBarrage : SpecialAttack
    {
        [SerializeField] float secondsBeforeSplit;

        [SerializeField] float distanceFromFirstSplit;
        [SerializeField] float distanceFromSecondSplit;

        [SerializeField] int amountOfSplitBullet;


        protected override void Awake()
        {
            if(_pool[0] != null)
                _pool[0].Initialize();
            if(_pool[1] != null)
                _pool[1].Initialize();
        }

        public override void Use()
        {
            base.Use();

            StartCoroutine(BBBSequence());
        }

        public override void End()
        {
            base.End();
        }

        IEnumerator BBBSequence()
        {
            //1ST PHASE

            var targetDir = Character.targetHandler.GetCurrentTarget().activeCharacter.position - transform.position;

            var FirstPhaseBullet = _pool[0].GetPoolable();

            FirstPhaseBullet.GetComponent<BBBProjectile>().generalDirection = Quaternion.LookRotation(targetDir);
            FirstPhaseBullet.GetComponent<BBBProjectile>().transform.position = bulletSpawnPoint[0].transform.position;

            FirstPhaseBullet.SetActive(true);

            End();

            yield return new WaitForSeconds(secondsBeforeSplit);

            //2ND PHASE

            var SecondPhaseBullets = new List<GameObject>();

            var FirstPhaseBulletPos = FirstPhaseBullet.transform;

            if(FirstPhaseBullet.activeInHierarchy)
			{
                for (int i = 1; i <= amountOfSplitBullet; i++)
                {
                    var SecondPhaseBulletSub = _pool[0].GetPoolable();

                    SecondPhaseBulletSub.transform.rotation = FirstPhaseBullet.transform.rotation;

                    SecondPhaseBulletSub.transform.position = 
                        CalculateSplitPosition(i, distanceFromFirstSplit, FirstPhaseBulletPos, targetDir);

                    SecondPhaseBulletSub.GetComponent<BBBProjectile>().towardsSplitPoint = 
                        CalculateSplitPosition(i, distanceFromFirstSplit, FirstPhaseBulletPos, targetDir);

                    SecondPhaseBulletSub.GetComponent<BBBProjectile>().generalDirection = 
                        FirstPhaseBullet.GetComponent<BBBProjectile>().generalDirection;

                    SecondPhaseBulletSub.SetActive(true);

                    SecondPhaseBullets.Add(SecondPhaseBulletSub);
                }
            }

            FirstPhaseBullet.GetComponent<BBBProjectile>().ResetObject();
            FirstPhaseBullet.GetComponent<BBBProjectile>().Return();
            FirstPhaseBullet.GetComponent<BBBProjectile>().generalDirection = Quaternion.identity;


            yield return new WaitForSeconds(secondsBeforeSplit);

            //3RD PHASE

            foreach (GameObject secondPhaseBullet in SecondPhaseBullets)
            {
                var SecondPhaseBulletsPos = secondPhaseBullet.transform;

                if(secondPhaseBullet.activeInHierarchy)
				{
                    for (int i = 1; i <= amountOfSplitBullet; i++)
                    {
                        var ThirdPhaseBulletSub = _pool[1].GetPoolable();

                        ThirdPhaseBulletSub.GetComponent<Bullet>().transform.rotation =
                            secondPhaseBullet.GetComponent<BBBProjectile>().transform.rotation;

                        ThirdPhaseBulletSub.GetComponent<Bullet>().transform.position =
                            CalculateSplitPosition(i, distanceFromSecondSplit, SecondPhaseBulletsPos, targetDir);

                        ThirdPhaseBulletSub.GetComponent<Bullet>().generalDirection = 
                            secondPhaseBullet.GetComponent<BBBProjectile>().generalDirection;

                        ThirdPhaseBulletSub.SetActive(true);
                    }

                    secondPhaseBullet.GetComponent<BBBProjectile>().ResetObject();
                    secondPhaseBullet.GetComponent<BBBProjectile>().Return();
                    secondPhaseBullet.GetComponent<BBBProjectile>().generalDirection = Quaternion.identity;
                }
            }

            yield break;
        }

        Vector3 CalculateSplitPosition(int NumberofTimes, float distanceFromOriginal, Transform originalPos, Vector3 targetDir)
        {
            /*float theta = NumberofTimes * 2 * Mathf.PI / amountOfSplitBullet;

            Debug.Log("Theta " + NumberofTimes + ": " + theta);

            float x = originalPos.position.x + Mathf.Sin(theta) * distanceFromOriginal;
            float y = originalPos.position.y + Mathf.Cos(theta) * distanceFromOriginal;
			float z = originalPos.position.z;


            return new Vector3(x,y,z);*?

            /*Vector3 randomPos = Random.onUnitSphere * distanceFromOriginal;
            randomPos += originalPos.position;

            targetDir.Normalize();

            float dotProduct = Vector3.Dot(originalPos.forward, targetDir);
            float dotProductAngle = Mathf.Acos(dotProduct / originalPos.forward.magnitude * targetDir.magnitude);

            randomPos.x = Mathf.Cos(dotProductAngle) * distanceFromOriginal + originalPos.position.x;
            randomPos.y = Mathf.Cos(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * distanceFromOriginal + originalPos.position.y;
            randomPos.z = originalPos.position.z;

            return randomPos;*/

            Vector3 newPos;

            switch(NumberofTimes)
			{
                case 1:
                    newPos = originalPos.position + (originalPos.right * distanceFromOriginal);
                    return newPos;

                case 2:
                    newPos = originalPos.position + (originalPos.up * distanceFromOriginal);
                    return newPos;
                case 3:
                    newPos = originalPos.position + (-originalPos.right * distanceFromOriginal);
                    return newPos;
                case 4:
                    newPos = originalPos.position + (-originalPos.up * distanceFromOriginal);
                    return newPos;
            }
            return Vector3.zero;
		}            
    }
}


