using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class RCRBullet : Projectile
    {
        [Header("Unique properties")]
        [SerializeField]
        private GameObject pivot, beam;
        [SerializeField]
        private Transform start, end;

        public GameObject Pivot => pivot;
        public GameObject Beam => beam;

        private bool isHit = false;
        private MechSystem.Health hpToDamage;

        private void Awake()
        {
            HitBox.onHitEnter += OnHitEvent;
            HitBox.onHitExit += OnHitExit;
        }

        private void OnDestroy()
        {       
            HitBox.onHitEnter -= OnHitEvent;            
            HitBox.onHitExit -= OnHitExit;
        }

        public override void OnEnable()
        {
            return;
        }

        public override void Update()
        {
            base.Update();
            //HitPlayer();
        }

        private void OnHitEvent(IHitData data)
        {
            if(!isHit)
            {
                isHit = true;
                var enemyHp = (MechSystem.Health)data.hurtShape.character.health;
                enemyHp.TakeDamage(enemyHp.MaxValue * 0.3f);
                //hpToDamage = (MechSystem.Health)data.hurtShape.character.health;
            }
        }

        private void OnHitExit(IHitData data)
        {
            if(isHit)
            {
                isHit = false;
                hpToDamage = null;
            }
        }

        public void Init(Vector3 pos = default)
        {
            transform.position = pos;
            pivot.transform.localScale = Vector3.one;
            beam.transform.localScale = Vector3.zero;
        }

        private void HitPlayer()
        {
            if(isHit && hpToDamage != null)
            {
                hpToDamage.TakeDamage(projectileDamage);
            }
        }

        public override void ResetObject()
        {
            base.ResetObject();
            gameObject.SetActive(false);
            hpToDamage = null;
            isHit = false;
        }
    }
}
