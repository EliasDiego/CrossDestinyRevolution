using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ObjectPoolingSystem;

namespace CDR.VFXSystem
{
	public class HitGunVFXPoolable : MonoBehaviour, IPoolable
	{
		[SerializeField] HitVFXHandler hitVFXHandler;

		IPool _pool;

		public IPool pool { get => _pool; set => _pool = value; }

		public void ResetObject(){}

		public void Return(){}
	}
}

