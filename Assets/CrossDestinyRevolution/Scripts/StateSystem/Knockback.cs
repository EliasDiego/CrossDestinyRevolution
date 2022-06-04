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


        IEnumerator StartKnockback(float duration)
        {
            EnemyKnockback();
            StartState();

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

