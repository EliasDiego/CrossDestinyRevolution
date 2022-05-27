using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MechSystem
{
    [System.Serializable]
    public class Health : ValueRange, IHealth
    {
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
            CheckHealthStatus();
        }

        public void Death()
        {
            Debug.Log("Player died");
        }
    }
}