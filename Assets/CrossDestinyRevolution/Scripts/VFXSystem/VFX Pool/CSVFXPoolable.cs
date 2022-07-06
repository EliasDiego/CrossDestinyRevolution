using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ObjectPoolingSystem;

namespace CDR.VFXSystem
{
	public class CSVFXPoolable : HitGunVFXPoolable
	{
		[SerializeField] CosmicStormVFXHandler hitVFXHandler;

		public override void PlayVFX()
		{
			hitVFXHandler.Activate();
			StartCoroutine(EndVFX(activeTime));
		}
		IEnumerator EndVFX(float time)
		{
			yield return new WaitForSeconds(time);
			Return();
		}

		public override void ResetObject()
		{
			hitVFXHandler.Deactivate();
		}

		public override void Return()
		{
			ResetObject();
			transform.parent = null;
			_pool.ReturnObject(this);
		}
	}
}
