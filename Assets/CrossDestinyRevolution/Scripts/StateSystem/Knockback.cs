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
    }
}