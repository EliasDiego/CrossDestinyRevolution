using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VersusSystem
{
    [CreateAssetMenu(menuName = "VersusSystem/Mech Data")]
    public class MechData : ScriptableObject, IMechData
    {
        [SerializeField]
        private string _MechName;
        [SerializeField]
        private GameObject _MechPrefab;
        [SerializeField]
        private Sprite _MechProfile;

        public string mechName => _MechName;
        public GameObject mechPrefab => _MechPrefab;
    }
}