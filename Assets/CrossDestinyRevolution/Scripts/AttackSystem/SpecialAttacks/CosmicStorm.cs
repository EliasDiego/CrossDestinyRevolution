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
            Fire();
        }

        public override void End()
        {
            base.End();
        }

        private void Fire()
        {
            var targetDir = -activeCharacter.targetHandler.GetCurrentTarget().direction;
            var cluster = _pool[0].GetPoolable();
            if(cluster != null)
            {
                cluster.GetComponent<CosmicBulletCluster>().Init(bulletSpawnPoint[0].transform.position, targetDir);
            }
            End();
        }
    }
}
