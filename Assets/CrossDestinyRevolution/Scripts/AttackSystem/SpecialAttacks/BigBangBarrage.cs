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

            FirstPhaseBullet.GetComponent<BBBProjectile>().originPoint = bulletSpawnPoint[0].transform.position;

            FirstPhaseBullet.SetActive(true);

            End();

            yield return new WaitForSeconds(secondsBeforeSplit);

            //2ND PHASE

            var SecondPhaseBullets = new List<GameObject>();

            var FirstPhaseBulletPos = FirstPhaseBullet.transform.position;

            var AngleStep = 360 / amountOfSplitBullet;

            if(FirstPhaseBullet.activeInHierarchy)
			{
                for (int i = 1; i <= amountOfSplitBullet; i++)
                {
                    var SecondPhaseBulletSub = _pool[0].GetPoolable();

                    float theta = i * 2 * Mathf.PI / amountOfSplitBullet;
                    float x = Mathf.Sin(theta) * distanceFromFirstSplit;
                    float y = Mathf.Cos(theta) * distanceFromFirstSplit;
                    float z = Mathf.Sin(theta) * distanceFromFirstSplit;

                    SecondPhaseBulletSub.GetComponent<BBBProjectile>().originPoint = new Vector3(FirstPhaseBulletPos.x + x, FirstPhaseBulletPos.y + y, FirstPhaseBulletPos.z + z); ;

                    SecondPhaseBulletSub.GetComponent<BBBProjectile>().generalDirection = FirstPhaseBullet.GetComponent<BBBProjectile>().generalDirection;
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
                var SecondPhaseBulletsPos = secondPhaseBullet.transform.position;

                if(secondPhaseBullet.activeInHierarchy)
				{
                    for (int i = 1; i <= amountOfSplitBullet; i++)
                    {
                        var ThirdPhaseBulletSub = _pool[1].GetPoolable();

                        float theta = i * 2 * Mathf.PI / amountOfSplitBullet;
                        float x = Mathf.Sin(theta) * distanceFromSecondSplit;
                        float y = Mathf.Cos(theta) * distanceFromSecondSplit;
                        float z = Mathf.Sin(theta) * distanceFromSecondSplit;

                        ThirdPhaseBulletSub.GetComponent<Bullet>().originPoint = new Vector3(SecondPhaseBulletsPos.x + x, SecondPhaseBulletsPos.y + y, SecondPhaseBulletsPos.z + z);

                        ThirdPhaseBulletSub.GetComponent<Bullet>().transform.rotation = secondPhaseBullet.GetComponent<BBBProjectile>().transform.rotation;
                        ThirdPhaseBulletSub.GetComponent<Bullet>().generalDirection = secondPhaseBullet.GetComponent<BBBProjectile>().generalDirection;
                        ThirdPhaseBulletSub.SetActive(true);
                    }

                    secondPhaseBullet.GetComponent<BBBProjectile>().ResetObject();
                    secondPhaseBullet.GetComponent<BBBProjectile>().Return();
                    secondPhaseBullet.GetComponent<BBBProjectile>().generalDirection = Quaternion.identity;
                }
            }

            yield break;
        }

        private void OnDrawGizmos()
        {
            if (testTarget != null)
            {
                
            }
        }
    }
}


