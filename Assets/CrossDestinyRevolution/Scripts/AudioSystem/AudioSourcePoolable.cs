using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ObjectPoolingSystem;
using CDR.AudioSystem;

namespace CDR.VFXSystem
{
	[RequireComponent(typeof(AudioSource))]
	public class AudioSourcePoolable : MonoBehaviour, IPoolable
	{
		[SerializeField] AudioSource audioSource;

		[SerializeField] protected float activeTime;

		protected IPool _pool;

		public IPool pool { get => _pool; set => _pool = value; }

		public virtual void PlayAudio(AudioClipPreset preset)
		{
			preset.PlayOneShot(audioSource);
			StartCoroutine(EndAudio(activeTime));
		}
		IEnumerator EndAudio(float time)
		{
			yield return new WaitForSeconds(time);
			Return();
		}

		public virtual void ResetObject()
		{
			//audioSourceHandler.Deactivate();
		}

		public virtual void Return()
		{
			ResetObject();
			transform.parent = null;
			_pool.ReturnObject(this);
		}
	}
}
