using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.InputSystem
{
    [CreateAssetMenu(menuName = "InputSystem/Settings/Mech/Boost Input Settings")]
    public class BoostInputSettings : ScriptableObject, IBoostInputSettings
    {
        [SerializeField]
        float _MovementInputThreshold;
        [SerializeField]
        MinMaxRange _BoostUpRange;
        [SerializeField]
        float _BoostDownMinHeight;

        public float movementInputThreshold => _MovementInputThreshold;

        public IMinMaxRange boostUpHeightRange => _BoostUpRange;

        public float boostDownMinHeight => _BoostDownMinHeight;
    }
}