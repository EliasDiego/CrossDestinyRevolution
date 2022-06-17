using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ObjectPoolingSystem;

namespace CDR.AttackSystem
{
    public class CosmicStorm : SpecialAttack
    {
        [SerializeField]
        private float speed = 1f;

        protected override void Awake()
        {
            if (_pool[0] != null)
                _pool[0].Initialize(); 
        }
        public override void Use()
        {
            base.Use();
            //Fire();
            StartCoroutine(Test());
        }

        public override void End()
        {
            base.End();
        }

        IEnumerator Test()
        {
            var cluster = _pool[0].GetPoolable();
            cluster.GetComponent<CosmicBulletCluster>().transform.position = bulletSpawnPoint[0].transform.position;
            //cluster.transform.position = bulletSpawnPoint[0].transform.position;
            cluster.SetActive(true);
            End();
            yield break;
        }

        private void Fire()
        {
            var targetDir = -activeCharacter.targetHandler.GetCurrentTarget().direction;
            var cluster = _pool[0].GetPoolable();
            if(cluster != null)
            {
                //cluster.GetComponent<CosmicBulletCluster>().Init(spawn.position, targetDir);
                //cluster.transform.position = spawn.position;
                cluster.SetActive(true);
            }
            End();
        }
    }
}
