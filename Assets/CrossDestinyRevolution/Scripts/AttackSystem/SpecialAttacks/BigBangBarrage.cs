using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.AnimationSystem;

namespace CDR.AttackSystem
{
    public class BigBangBarrage : SpecialAttack
    {
        [SerializeField] float secondsBeforeSplit;

        [SerializeField] float distanceFromFirstSplit;
        [SerializeField] float distanceFromSecondSplit;

        [SerializeField] int amountOfSplitBullet;

        [SerializeField] CDR.AnimationSystem.AnimationEvent _animationEvent;

        AnimationEventsManager _Manager;


        protected override void Awake()
        {
            if(_pool[0] != null)
                _pool[0].Initialize();

            _Manager = Character.animator.GetComponent<AnimationEventsManager>();

            var a = new CDR.AnimationSystem.AnimationEvent(0.1f, true, null, null, null);

            //_Manager.AddAnimationEvent("Special Attack 02", a); //SpecialAttack02
        }

        public override void Use()
        {
            base.Use();

            StartCoroutine(BBBSequence());

            End();
        }

        public override void End()
        {
            base.End();
        }

		public override void ForceEnd()
		{
			base.ForceEnd();

            StopAllCoroutines();
		}

		IEnumerator BBBSequence()
        {
            //1ST PHASE

            var targetDir = Character.targetHandler.GetCurrentTarget().activeCharacter.position - transform.position;

            var FirstPhaseBullet = _pool[0].GetPoolable();

            FirstPhaseBullet.GetComponent<BBBProjectile>().generalDirection = Quaternion.LookRotation(targetDir);
            FirstPhaseBullet.GetComponent<BBBProjectile>().transform.position = bulletSpawnPoint[0].transform.position;
            FirstPhaseBullet.GetComponent<BBBProjectile>().hasLifeTime = false;
            FirstPhaseBullet.GetComponent<BBBProjectile>().towardsSplitPoint = transform.position;

            FirstPhaseBullet.SetActive(true);

            yield return new WaitForSeconds(secondsBeforeSplit);

            //2ND PHASE

            var SecondPhaseBullets = new List<GameObject>();

            var FirstPhaseBulletPos = FirstPhaseBullet.transform;

            if(FirstPhaseBullet.activeInHierarchy)
			{
                for (int i = 1; i <= amountOfSplitBullet; i++)
                {
                    var SecondPhaseBulletSub = _pool[0].GetPoolable();

                    SecondPhaseBulletSub.transform.SetPositionAndRotation(FirstPhaseBulletPos.position, FirstPhaseBullet.transform.rotation);

                    SecondPhaseBulletSub.GetComponent<BBBProjectile>().towardsSplitPoint = 
                        CalculateSplitPosition(i, distanceFromFirstSplit, FirstPhaseBulletPos);

                    SecondPhaseBulletSub.GetComponent<BBBProjectile>().generalDirection = 
                        FirstPhaseBullet.GetComponent<BBBProjectile>().generalDirection;

                    SecondPhaseBulletSub.GetComponent<BBBProjectile>().hasLifeTime = false;

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
                        var ThirdPhaseBulletSub = _pool[0].GetPoolable();
                        
                        ThirdPhaseBulletSub.transform.SetPositionAndRotation(SecondPhaseBulletsPos.position, secondPhaseBullet.transform.rotation);

                        ThirdPhaseBulletSub.GetComponent<BBBProjectile>().towardsSplitPoint =
                            CalculateSplitPosition(i, distanceFromSecondSplit, SecondPhaseBulletsPos);

                        ThirdPhaseBulletSub.GetComponent<BBBProjectile>().generalDirection = 
                            secondPhaseBullet.GetComponent<BBBProjectile>().generalDirection;

                        ThirdPhaseBulletSub.GetComponent<BBBProjectile>().hasLifeTime = true;

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


