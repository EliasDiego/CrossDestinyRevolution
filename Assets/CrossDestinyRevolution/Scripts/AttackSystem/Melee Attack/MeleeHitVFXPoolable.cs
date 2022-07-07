using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.ObjectPoolingSystem;

namespace CDR.VFXSystem
{
    public class MeleeHitVFXPoolable : MonoBehaviour, IPoolable
    {
        [SerializeField] HitVFXHandler _vfx;
        [SerializeField] float activeTime;
        IPool _pool;

        public IPool pool { get => _pool; set => _pool = value; }

        public void PlayVfx()
        {
            _vfx.Activate();
            StartCoroutine(EndVfx(activeTime));
        }

        IEnumerator EndVfx(float time)
        {
            yield return new WaitForSeconds(time);
            Return();
        }

        public void ResetObject(){
            _vfx.Deactivate();
        }

        public void Return()
        {
            ResetObject();
            transform.parent = null;
            pool.ReturnObject(this);
        }
    }
}