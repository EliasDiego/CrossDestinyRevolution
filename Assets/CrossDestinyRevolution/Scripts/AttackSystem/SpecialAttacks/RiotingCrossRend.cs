using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class RiotingCrossRend : SpecialAttack
    {
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
            var bullet = _pool[0].GetPoolable();
            if (bullet != null)
            {
                Vector3 dir = -Character.targetHandler.GetCurrentTarget().direction;
                bullet.transform.forward = dir;
                bullet.gameObject.GetComponent<RCRBullet>().Init(Character.position);
                bullet.gameObject.SetActive(true);
            }
            End();
        }
    }
}
