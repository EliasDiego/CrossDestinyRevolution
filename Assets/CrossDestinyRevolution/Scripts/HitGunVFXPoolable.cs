using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ObjectPoolingSystem;

namespace CDR.VFXSystem
{
	public class HitGunVFXPoolable : MonoBehaviour, IPoolable
	{
		[SerializeField] HitVFXHandler hitVFXHandler;

		[SerializeField] float activeTime;

		IPool _pool;

		public IPool pool { get => _pool; set => _pool = value; }

		public void PlayVFX()
		{
			hitVFXHandler.Activate();
			StartCoroutine(EndVFX(activeTime));
		}
		IEnumerator EndVFX(float time)
		{
			yield return new WaitForSeconds(time);
			Return();
		}

		public void ResetObject()
		{
			hitVFXHandler.Deactivate();
		}

		public void Return()
		{
			ResetObject();
			transform.parent = null;
			_pool.ReturnObject(this);
		}
	}
}

