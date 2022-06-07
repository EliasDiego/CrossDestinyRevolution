using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.StateSystem
{
    public class Knockback : State, IKnockback
    {
        [SerializeField] float _distance;
        [SerializeField] float _duration;

        public float distance => _distance;
        public float duration => _duration;

        public override void StartState()
        {
            base.StartState();
            Debug.Log("Start Knockback");

            StartCoroutine(KnockbackCoroutine(duration));
        }

        public override void EndState()
        {
            base.EndState();
            receiver.controller.SetVelocity(Vector3.zero);
            Debug.Log("End Knockback");
        }

        IEnumerator KnockbackCoroutine(float duration)
        {
            EnemyKnockback();

            yield return new WaitForSeconds(duration);
            EndState();
        }

        void EnemyKnockback()
        {
            Vector3 dir = (sender.position - receiver.position).normalized;
            dir.y = 0;

            //receiver.controller.SetVelocity(-dir * distance / duration);
            receiver.controller.AddRbForce(-dir * distance, ForceMode.Impulse);
        }
    }
}