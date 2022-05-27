using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VersusSystem
{
    [CreateAssetMenu(menuName = "VersusSystem/Mech Data")]
    public class MechData : ScriptableObject, IMechData
    {
        [SerializeField]
        string _MechName;
        [SerializeField]
        GameObject _MechPrefab;
        [SerializeField]
        GameObject _UIPrefab;

        public string mechName => _MechName;
        public GameObject mechPrefab => _MechPrefab;
        public GameObject UIPrefab => _UIPrefab;
    }
}