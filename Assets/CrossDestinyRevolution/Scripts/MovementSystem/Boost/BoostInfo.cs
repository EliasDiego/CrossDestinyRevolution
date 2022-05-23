using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CDR.MovementSystem
{
    [CreateAssetMenu(fileName = "Movement Info", menuName = "CDR/MovementSystem/MovementInfo")]
    public class BoostInfo : ScriptableObject
    {
        [Header("HORIZONTAL BOOST")]
        [Tooltip("Distance traveled by boost usage.")]
        public float horzDashDistance = 5f;
        [Tooltip("Duration of boost.")]
        public float hDashUseTime = 3f;
        [Tooltip("Amount of boost consumed per boost usage.")]
        public float hDashConsRate = 0f;

        [Header("VERTICAL BOOST")]
        [Tooltip("Distance traveled by boost usage.")]
        public float vertDashDistance = 5f;
        [Tooltip("Duration of boost.")]
        public float vDashUseTime = 3f;
        [Tooltip("Amount of boost consumed per boost usage.")]
        public float vDashConsRate = 0f;        


        [Header("PARAMETERS")]
        [Tooltip("Amount of boost regen per sec.")]
        public float regenRate = 0f;
    }
}

