using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class BigBangBarrage : SpecialAttack
    {
        [SerializeField] GameObject testTarget; // to test the range

        [SerializeField] float secondsBeforeSplit;

        [SerializeField] float distanceFromFirstSplit;
        [SerializeField] float distanceFromSecondSplit;

        [SerializeField] int amountOfSplitBullet;


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

                    SecondPhaseBulletSub.GetComponent<BBBProjectile>().transform.rotation =
                            FirstPhaseBullet.GetComponent<BBBProjectile>().transform.rotation;

                    SecondPhaseBulletSub.GetComponent<BBBProjectile>().transform.position = 
                        CalculateSplitPosition(i,distanceFromFirstSplit,FirstPhaseBulletPos);

                    

                    SecondPhaseBulletSub.GetComponent<BBBProjectile>().generalDirection = 
                        FirstPhaseBullet.GetComponent<BBBProjectile>().generalDirection;

                    SecondPhaseBulletSub.SetActive(true);

                    SecondPhaseBullets.Add(SecondPhaseBulletSub);
                }

                FirstPhaseBullet.GetComponent<BBBProjectile>().ResetObject();
                FirstPhaseBullet.GetComponent<BBBProjectile>().Return();
                FirstPhaseBullet.GetComponent<BBBProjectile>().generalDirection = Quaternion.identity;
            }
            

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
                            CalculateSplitPosition(i, distanceFromSecondSplit, SecondPhaseBulletsPos);

                        

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

        Vector3 CalculateSplitPosition(int NumberofTimes, float distanceFromOriginal, Transform originalPos)
        {
            float theta = NumberofTimes * 2 * Mathf.PI / amountOfSplitBullet;
            float x = originalPos.position.x + Mathf.Sin(theta) * distanceFromOriginal;
            float y = originalPos.position.y + Mathf.Cos(theta) * distanceFromOriginal;
			float z = originalPos.position.z;

            return new Vector3(x,y,z);
		}            

        private void OnDrawGizmos()
        {
            if (testTarget != null)
            {
                
            }
        }
    }
}


