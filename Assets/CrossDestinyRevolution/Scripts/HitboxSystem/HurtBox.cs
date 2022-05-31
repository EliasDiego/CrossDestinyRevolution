using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;

namespace CDR.HitboxSystem
{
    public class HurtBox : HitBoxBase
    {
		private void Awake()
		{
            inactiveColor = new Color(0, 76, 0, 1);
            collisionOpenColor = new Color(0, 255, 0, 1);
            collidingColor = new Color(0, 0, 255, 1);
        }

		public bool getHitBy(float damage)
        {
            this.gameObject.GetComponent<ActiveCharacter>().health.TakeDamage(damage);
            //Destroy(gameObject);
            return true;
        }
    }
}

