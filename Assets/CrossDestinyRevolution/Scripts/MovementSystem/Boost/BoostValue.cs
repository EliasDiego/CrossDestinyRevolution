using System;
using System.Collections;
using UnityEngine;

// This class handles value for boost and its methods.

namespace CDR.MovementSystem
{
    [Serializable]
    public class BoostValue : ValueRange, IBoostValue
    {
        [SerializeField]
        private float regenRate;
        [SerializeField]
        private float consumeRate;

        private bool isRegening = false;

        public float regenerationRate => regenRate;

        public float valueConsumption => consumeRate;

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
            ModifyValue(CurrentValue - consumeRate);
        }

        public IEnumerator Regenerate()
        {
            while(true)
            {
                if(isRegening)
                {
                    var add = CurrentValue + regenRate * Time.deltaTime;
                    ModifyValue(add);

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
