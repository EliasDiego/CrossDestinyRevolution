using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

namespace CDR.AttackSystem
{
    public class HomingProjectile : Projectile
    {
        

		
		public override void Start()
		{
			base.Start();
		}

		public override void OnEnable()
		{
			base.OnEnable();

			

		}

		

		public virtual void RotateProjectile(){}

		
	}
}

