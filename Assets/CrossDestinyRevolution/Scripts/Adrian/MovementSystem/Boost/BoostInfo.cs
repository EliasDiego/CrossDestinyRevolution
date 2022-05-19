using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CDR.MovementSystem
{
    [CreateAssetMenu(fileName = "Movement Info", menuName = "CDR/MovementSystem/MovementInfo")]
    public class BoostInfo : ScriptableObject
    {
        [Tooltip("Distance traveled by boost usage.")]
        public float dashDistance = 5f;
        [Tooltip("Amount of boost regen per sec.")]
        public float regenRate = 0f;
        [Tooltip("Amount of boost consumed per boost usage.")]
        public float consumeRate = 0f;
        
    }
}

