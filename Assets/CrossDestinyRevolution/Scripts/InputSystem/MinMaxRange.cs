using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.InputSystem
{
    [System.Serializable]
    public struct MinMaxRange : IMinMaxRange
    {
        [SerializeField]
        float _MinValue;
        [SerializeField]
        float _MaxValue;

        public float minValue => _MinValue;

        public float maxValue => _MaxValue;

        public bool IsWithinRange(float value)
        {
            return minValue <= value && value <= maxValue;
        }
    }
}