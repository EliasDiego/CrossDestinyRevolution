using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.AnimationSystem;
using CDR.VFXSystem;

namespace CDR.AttackSystem
{
    public class NightmareStalker : SpecialAttack
	{
        [SerializeField] float attackRange = 100f;

		[SerializeField] float checkDistanceInterval;
        [SerializeField] float bulletTrailSpawnInterval; // Interval between spawning bullet trail
        [SerializeField] float bulletTrailDeSpawnInterval; // Interval between despawning bullet trail

        [SerializeField] float AfterDistanceCheckInterval; // Interval between despawning bullet trail

		AnimationEventsManager _Manager;

        [SerializeField] SFXAnimationEvent[] sfxAnimationEvents;

        [SerializeField] NightmareStalkerVFXHandler nightmareStalkerVFXHandler;

        protected override void Awake()
        {
            if (_pool[0] != null)
                _pool[0].Initialize();
            if (_pool[1] != null)
                _pool[1].Initialize();

            _Manager = Character.animator.GetComponent<AnimationEventsManager>();

            var a = new CDR.AnimationSystem.AnimationEvent(0.33f, true, () => StartCoroutine(NSSequence()));
            var b = new CDR.AnimationSystem.AnimationEvent(1f, true, () => End());

            _Manager.AddAnimationEvent("SAttack1", a,b); //SpecialAttack01
            _Manager.AddAnimationEvent("SAttack1", sfxAnimationEvents);
        }

        public override void Use()
        {
            base.Use();

            Character.animator.SetInteger("ActionType", (int)ActionType.SpecialAttack1);

        }

        public override void End()
		{
			base.End();

            Character.animator.SetInteger("ActionType", (int)ActionType.None);

            //ForceEnd();
        }

		public override void ForceEnd()
        {
            base.ForceEnd();

            Character.animator.SetInteger("ActionType", (int)ActionType.None);

            nightmareStalkerVFXHandler.Deactivate();

            StopAllCoroutines();
        }

        public override void Stop()
        {
            base.Stop();

            ForceEnd();
        }

		public override void Update()
		{
            base.Update();
		}

		IEnumerator NSSequence()
		{
            //1ST PHASE

            Debug.Log("Nightmare Stalker Started");

            var target = Character.targetHandler.GetCurrentTarget();

            GameObject firstBullet; //bullet that homes and leaves a trail after despawn

            var bulletTrailSpawn = new List<Vector3>();

            firstBullet = _pool[0].GetPoolable();

			firstBullet.GetComponent<HomingBullet>().target = target.activeCharacter;
            firstBullet.GetComponent<HomingBullet>().playerAttackRange = attackRange;
            firstBullet.GetComponent<HomingBullet>().transform.position = bulletSpawnPoint[0].transform.position;
            firstBullet.GetComponent<HomingBullet>().originPoint = bulletSpawnPoint[0].transform.position;
            firstBullet.GetComponent<HomingBullet>().owner = Character;
			firstBullet.SetActive(true);

            nightmareStalkerVFXHandler.Activate();

            yield return new WaitForSeconds(checkDistanceInterval);

            StartCoroutine(CheckDistanceSpawn(firstBullet, bulletTrailSpawn));

            yield return new WaitWhile(() => !firstBullet.GetComponent<HomingBullet>().CheckDistanceFromTarget());

            yield return new WaitForSecondsRealtime(AfterDistanceCheckInterval);

            firstBullet.GetComponent<HomingBullet>().ResetObject();
            firstBullet.GetComponent<HomingBullet>().Return();

            yield return new WaitUntil(() => !firstBullet.activeInHierarchy);
            //2nd PHASE
            
            var bulletTrailBullets = new List<GameObject>();

            var bulletTrailSpawnArray = bulletTrailSpawn.ToArray();

            for(int i = 0; i < bulletTrailSpawnArray.Length; i++)
			{
                var bulletTrailBullet = _pool[1].GetPoolable();

                bulletTrailBullet.transform.position = bulletTrailSpawnArray[i];
                bulletTrailBullet.GetComponent<Projectile>().owner = Character;
                

                bulletTrailBullet.SetActive(true);

                if (i < bulletTrailSpawnArray.Length - 1)
                {
                    bulletTrailBullet.transform.LookAt(bulletTrailSpawnArray[i + 1]);
                }

                if(i == bulletTrailSpawnArray.Length - 1)
				{
                    bulletTrailBullet.transform.LookAt(bulletTrailSpawnArray[i - 1]);
                }

                bulletTrailBullets.Add(bulletTrailBullet);

                

                yield return new WaitForSeconds(bulletTrailSpawnInterval);
            }

            /*foreach (Vector3 bulletTrailSpawnPoint in bulletTrailSpawn)
			{
                var bulletTrailBullet = _pool[1].GetPoolable();

                bulletTrailBullet.transform.position = bulletTrailSpawnPoint;

                bulletTrailBullet.SetActive(true);

                bulletTrailBullets.Add(bulletTrailBullet);

                yield return new WaitForSecondsRealtime(bulletTrailSpawnInterval);
            }*/

            //yield return new WaitUntil(() => CheckIfAllActive(bulletTrailBullets));

            //3rd Phase

            foreach (GameObject bullet in bulletTrailBullets)
            {
                bullet.SetActive(false);
                bullet.GetComponent<Bullet>().ResetObject();
                bullet.GetComponent<Bullet>().Return();
                yield return new WaitForSeconds(bulletTrailDeSpawnInterval);
            }

            yield break;
		}

        IEnumerator CheckDistanceSpawn(GameObject firstBullet, List<Vector3> bulletTrailSpawn)
		{
            while (firstBullet.activeInHierarchy)
            {
                bulletTrailSpawn.Add(firstBullet.transform.position);
                yield return new WaitForSeconds(checkDistanceInterval);
            }
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


