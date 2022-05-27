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
            _valueRange.ModifyValueWithoutEvent(value);
        }

        public void CheckHealthStatus()
        {
            if(CurrentValue < 0)
            {
                Death();
            }
        }

        public void TakeDamage(float damage)
        {
            float currentHealth = CurrentValue - damage;
            ModifyValue(currentHealth);
        }

        public void Death()
        {
            Debug.Log("Player died");
        }

        private void Update()
        {
            CheckHealthStatus();
        }
    }
}

