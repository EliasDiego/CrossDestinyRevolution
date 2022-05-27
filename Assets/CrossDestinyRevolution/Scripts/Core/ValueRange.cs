using System;
using UnityEngine;

namespace CDR
{
    [System.Serializable]
    public class ValueRange : IValueRange
    {
        [SerializeField] float currentValue;
        [SerializeField] float maxValue;
    
        public float CurrentValue { get => currentValue; }

        public float MaxValue => maxValue;

        public event Action<IValueRange> OnModifyValue;

        public void ModifyValue(float value)
        {
            ModifyValueWithoutEvent(value);
            OnModifyValue?.Invoke(this);
        }

        public void ModifyValueWithoutEvent(float value)
        {
            currentValue = value;
        }

        
    }
}
