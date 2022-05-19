using System;
using System.Collections;
using UnityEngine;

// This class is for the Boost system and its methods.

namespace CDR.MovementSystem
{
    public class Boost
    {
        public Action OnUse;
        public Action OnEnd;

        public ValueRange Value { get; private set; }
        private readonly BoostInfo info;
        // Monobehaviour to run regenerate coroutine on.
        private readonly MonoBehaviour mono;
        private readonly IEnumerator regenRoutine;

        private bool isRegening = false;
        private float timeUsed = 0f;
        private float useEndThreshold = 0.75f;

        public Boost(BoostInfo boostInfo, MonoBehaviour monoBehaviour, ValueRange boostValue)
        {
            info = boostInfo;
            Value = boostValue;
            mono = monoBehaviour;
            Value.ModifyValueWithoutEvent(Value.MaxValue);

            regenRoutine = RegenBoost();
            mono.StartCoroutine(regenRoutine);
        }

        public IEnumerator Use(Vector3 direction, Rigidbody rb)
        {
            if (Value.CurrentValue - info.consumeRate < 0f)
            {
                yield break;
            }
            OnUse?.Invoke();
            timeUsed = 0f;
            isRegening = false;

            var force = direction.normalized * info.dashDistance;
            rb.AddForce(force, ForceMode.VelocityChange);
            var primarySpeed = force.magnitude;

            float diffToPrimary;
            yield return new WaitForSeconds(Time.deltaTime * 5f);

            ConsumeBoost();

            while (true)
            {
                timeUsed += Time.deltaTime;
                yield return null;
                diffToPrimary = rb.velocity.magnitude / primarySpeed;
                if (diffToPrimary <= useEndThreshold)
                {
                    Debug.Log(timeUsed);
                    break;
                }
            }        
        }     

        private void ConsumeBoost()
        {
            LeanTween.value(Value.CurrentValue, Value.CurrentValue - info.consumeRate, 0.25f)
                .setOnUpdate((float val) =>
                    {
                        Value.ModifyValue(val);
                    })
                .setOnComplete(() =>
                    {
                        isRegening = true;
                    });
        }

        private IEnumerator RegenBoost()
        {
            while(true)
            {
                if(isRegening)
                {
                    var add = Value.CurrentValue + info.regenRate * Time.deltaTime;
                    Value.ModifyValue(add);
                }
                yield return null;
            }
        }
    }
}

