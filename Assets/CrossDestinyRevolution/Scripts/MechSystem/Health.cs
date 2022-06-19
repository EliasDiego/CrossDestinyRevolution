using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MechSystem
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] ValueRange _valueRange;


        public float CurrentValue => _valueRange.CurrentValue;
        public float MaxValue => _valueRange.MaxValue;
        public event Action OnModifyValue;

        public void ModifyValue(float value)
        {
            ModifyValueWithoutEvent(value);
            OnModifyValue?.Invoke();
        }

        public void ModifyValueWithoutEvent(float value)
        {

        }
    }
}

