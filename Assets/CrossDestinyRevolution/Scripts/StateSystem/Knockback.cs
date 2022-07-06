using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ObjectPoolingSystem;
using CDR.AnimationSystem;

namespace CDR.StateSystem
{
    public class Knockback : State, IKnockback, IPoolable
    {
        IPool _pool;
        [SerializeField] float _distance;
        [SerializeField] float _duration;

        public float distance => _distance;
        public float duration => _duration;

        public IPool pool { get => _pool; set => _pool = value; }

        public override void StartState()
        {
            base.StartState();

            receiver.animator.SetInteger("StateType", (int)StateType.Knockback);
            StartCoroutine(KnockbackCoroutine(duration));
        }

        public override void EndState()
        {
            base.EndState();

            receiver.animator.SetInteger("StateType", (int)StateType.None);
            receiver.controller.SetVelocity(Vector3.zero);
            Return();
        }

        public override void ForceEndState()
        {
            base.ForceEndState();
            
            receiver.animator.SetInteger("StateType", (int)StateType.None);
            receiver.controller.SetVelocity(Vector3.zero);
            Return();
        }

        IEnumerator KnockbackCoroutine(float time)
        {
            Vector3 dir = (sender.position - receiver.position).normalized;
            dir.y = 0;

            while(time > 0)
            {
                //every 10 distance = 4.35f change in position
                time -= Time.fixedDeltaTime;

                //multiplied dir with 2.3f to make position = distance
                receiver.controller.AddRbForce((-dir * 2.3f) * distance / duration * Time.fixedDeltaTime);

                yield return new WaitForFixedUpdate();
            }

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