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
            StartCoroutine(StartKnockback(duration));
        }

        public override void EndState()
        {
            base.EndState();
        }

        IEnumerator StartKnockback(float duration)
        {
            EnemyKnockback();

            yield return new WaitForSeconds(duration);

            EndState();
        }

        void EnemyKnockback()
        {
            Vector3 dir = sender.targetHandler.GetCurrentTarget().direction;
            dir.y = 0;

            receiver.controller.Translate(dir, distance);
        }
    }
}