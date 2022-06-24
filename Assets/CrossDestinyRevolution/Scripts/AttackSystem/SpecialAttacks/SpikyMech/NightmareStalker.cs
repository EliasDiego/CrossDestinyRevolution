using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.AnimationSystem;

namespace CDR.AttackSystem
{
    public class NightmareStalker : SpecialAttack
	{
        [SerializeField] float attackRange = 100f;

		[SerializeField] float checkDistanceInterval;
        [SerializeField] float bulletTrailSpawnInterval; // Interval between spawning bullet trail
        [SerializeField] float bulletTrailDeSpawnInterval; // Interval between despawning bullet trail

        [SerializeField] CDR.AnimationSystem.AnimationEvent _animationEvent;

        AnimationEventsManager _Manager;

        protected override void Awake()
        {
            if (_pool[0] != null)
                _pool[0].Initialize();
            if (_pool[1] != null)
                _pool[1].Initialize();

            _Manager = Character.animator.GetComponent<AnimationEventsManager>();

            var a = new CDR.AnimationSystem.AnimationEvent(0.1f, true, null, null, null);

            //_Manager.AddAnimationEvent("SpecialAttack01", a); //SpecialAttack01
        }

        public override void Use()
        {
            base.Use();

            //Character.animator.SetBool("IsSAttack1", true);

            StartCoroutine(NSSequence());

            End();
        }

        public override void End()
        {
            base.End();

            //ForceEnd();
        }

        public override void ForceEnd()
        {
            base.ForceEnd();

            StopAllCoroutines();
        }

		public override void Update()
		{
            base.Update();
		}

		IEnumerator NSSequence()
		{
            //1ST PHASE

            var target = Character.targetHandler.GetCurrentTarget();

            GameObject firstBullet; //bullet that homes and leaves a trail after despawn

            var bulletTrailSpawn = new List<Vector3>();

            firstBullet = _pool[0].GetPoolable();

			firstBullet.GetComponent<HomingBullet>().target = target.activeCharacter;
            firstBullet.GetComponent<HomingBullet>().playerAttackRange = attackRange;
            firstBullet.GetComponent<HomingBullet>().transform.position = bulletSpawnPoint[0].transform.position;
            firstBullet.GetComponent<HomingBullet>().originPoint = bulletSpawnPoint[0].transform.position;
			firstBullet.SetActive(true);

            yield return new WaitForSecondsRealtime(checkDistanceInterval);

            while (firstBullet.activeInHierarchy)
            {
                bulletTrailSpawn.Add(firstBullet.transform.position);
                yield return new WaitForSecondsRealtime(checkDistanceInterval);
            }

            yield return new WaitUntil(() => !firstBullet.activeInHierarchy);

            //2nd PHASE
            
            var bulletTrailBullets = new List<GameObject>(); 

            foreach (Vector3 bulletTrailSpawnPoint in bulletTrailSpawn)
			{
                var bulletTrailBullet = _pool[1].GetPoolable();

                bulletTrailBullet.transform.position = bulletTrailSpawnPoint;

                bulletTrailBullet.SetActive(true);

                bulletTrailBullets.Add(bulletTrailBullet);

                yield return new WaitForSecondsRealtime(bulletTrailSpawnInterval);
            }

            yield return new WaitUntil(() => CheckIfAllActive(bulletTrailBullets));

            //3rd Phase

            foreach (GameObject bullet in bulletTrailBullets)
            {
                bullet.SetActive(false);
                bullet.GetComponent<Bullet>().ResetObject();
                bullet.GetComponent<Bullet>().Return();
                yield return new WaitForSecondsRealtime(bulletTrailDeSpawnInterval);
            }

            yield break;
		}

        bool CheckIfAllActive(List<GameObject> bulletTrail)
		{
            foreach(GameObject bullet in bulletTrail)
			{
                if (!bullet.activeInHierarchy)
				{
                    return false;
                }
			}
            return true;
		}
    }
}


