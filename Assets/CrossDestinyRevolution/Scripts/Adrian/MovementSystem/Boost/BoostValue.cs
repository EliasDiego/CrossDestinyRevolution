using System.Collections;
using UnityEngine;

// This class handles value for boost and its methods.

namespace CDR.MovementSystem
{
    public class BoostValue : MonoBehaviour
    {
        [SerializeField]
        private ValueRange value;
        public ValueRange Value => value;

        private bool isRegening = false;

        private void Start()
        {
            value.ModifyValueWithoutEvent(value.MaxValue);
        }

        public void Consume(float amount)
        {
            LeanTween.value(Value.CurrentValue, Value.CurrentValue - amount, 0.25f)
                .setOnUpdate((float val) =>
                {
                    Value.ModifyValueWithoutEvent(val);
                });
        }

        // If current value - consumption rate < 0, boost can't be used.
        public bool CanUse(float useRate)
        {
            return value.CurrentValue - useRate >= 0f;
        }

        public void SetIsRegening(bool boo)
        {
            isRegening = boo;
        }

        // Regenerate value by rate of regeneration every frame.
        public IEnumerator Regenerate(float regenRate)
        {
            var regen = regenRate;
            while(true)
            {
                if(isRegening)
                {
                    var add = value.CurrentValue + regen * Time.deltaTime;
                    value.ModifyValueWithoutEvent(add);

                    if(value.CurrentValue >= value.MaxValue)
                    {
                        value.ModifyValueWithoutEvent(value.MaxValue);
                        isRegening = false;
                    }
                }
                yield return null;
            }
        }

    }
}
