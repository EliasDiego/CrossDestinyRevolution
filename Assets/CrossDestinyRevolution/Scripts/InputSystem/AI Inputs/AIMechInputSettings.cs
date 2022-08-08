using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.InputSystem
{
    [CreateAssetMenu(menuName = "InputSystem/AI Mech Input Settings")]
    public class AIMechInputSettings : ScriptableObject
    {
        [field: SerializeField]
        public MinMaxRange specialAttack1Range { get; private set; }
        [field: SerializeField]
        public MinMaxRange specialAttack2Range { get; private set; }
        [field: SerializeField]
        public MinMaxRange specialAttack3Range { get; private set; }
    }
}