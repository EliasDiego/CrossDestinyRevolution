using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR
{
    [System.Serializable]
    public struct MinMaxRange
    {
        [field: SerializeField]
        public float minValue { get; private set; }
        [field: SerializeField]
        public float maxValue { get; private set; }

        public bool isWithinRange(float value)
        {
            return minValue >= value && value <= maxValue;
        }
    }
}