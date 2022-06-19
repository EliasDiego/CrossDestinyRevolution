using System;
using System.Collections;
using UnityEngine;

// This class handles value for boost and its methods.

namespace CDR.MovementSystem
{
    [Serializable]
    public class BoostValue : IBoostValue
    {
        [SerializeField]
        private ValueRange valueRange;
        [SerializeField]
        private float regenRate;
        [SerializeField]
        private float consumeRate;

        private bool isRegening = false;

        public float regenerationRate => regenRate;

        public float valueConsumption => consumeRate;

        public float CurrentValue => valueRange.CurrentValue;

        public float MaxValue => valueRange.MaxValue;

        public event Action OnModifyValue;

        public void ModifyValue(float value)
        {
            ModifyValueWithoutEvent(value);
            OnModifyValue?.Invoke();
        }

        public void ModifyValueWithoutEvent(float value)
        {
            valueRange.ModifyValueWithoutEvent(value);
        }

        public void SetIsRegening(bool boo)
        {
            isRegening = boo;
        }

        public bool CanUse()
        {
            return CurrentValue - consumeRate >= 0f;
        }

        public void Consume()
        {
            ModifyValueWithoutEvent(CurrentValue - consumeRate);
        }

        public IEnumerator Regenerate()
        {
            while(true)
            {
                if(isRegening)
                {
                    var add = CurrentValue + regenRate * Time.deltaTime;
                    ModifyValueWithoutEvent(add);

                    if(CurrentValue >= MaxValue)
                    {
                        ModifyValueWithoutEvent(MaxValue);
                        isRegening = false;
                    }
                }
                yield return null;
            }
        }
    }
}
