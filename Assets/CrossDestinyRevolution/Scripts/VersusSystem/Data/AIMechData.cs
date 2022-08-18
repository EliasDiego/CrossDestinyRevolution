using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    [CreateAssetMenu(menuName = "InputSystem/AI Mech Data")]
    public class AIMechData : ScriptableObject, IMechData
    {
        [SerializeField]
        private MechData _MechData;
        [SerializeField]
        private AIMechInputSettings _Settings;

        public string mechName => _MechData.mechName;

        public GameObject mechPrefab => _MechData.mechPrefab;

        public AIMechInputSettings settings => _Settings;
    }
}