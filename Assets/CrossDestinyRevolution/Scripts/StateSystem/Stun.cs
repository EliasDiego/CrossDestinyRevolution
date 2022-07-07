using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ObjectPoolingSystem;
using CDR.AnimationSystem;

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

            receiver.animator.SetInteger("StateType", (int)StateType.Stun);
            StartCoroutine(StunCoroutine(duration));
        }

        public override void EndState()
        {
            base.EndState();

            receiver.animator.SetInteger("StateType", (int)StateType.None);
            Return();
        }

        public override void ForceEndState()
        {
            base.ForceEndState();

            receiver.animator.SetInteger("StateType", (int)StateType.None);
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
