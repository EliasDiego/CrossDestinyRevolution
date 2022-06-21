using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ObjectPoolingSystem;

namespace CDR.StateSystem
{
    public class Stun : State, IStun, IPoolable
    {
        IPool _pool;
        [SerializeField] float _duration;

        public float duration => _duration;

        public IPool pool { get => _pool; set => _pool = value; }

        public override void StartState()
        {
            base.StartState();
            
            StartCoroutine(StunCoroutine(duration));
        }

        public override void EndState()
        {
            base.EndState();
            Return();
        }

        IEnumerator StunCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            EndState();
        }

        public void ResetObject(){}

        public void Return()
        {
            transform.parent = null;
            pool.ReturnObject(this);
        }
    }
}
