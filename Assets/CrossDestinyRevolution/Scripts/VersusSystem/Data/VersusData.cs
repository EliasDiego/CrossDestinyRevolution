using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VersusSystem
{
    [CreateAssetMenu(menuName = "VersusSystem/Versus Data")]
    public class VersusData : ScriptableObject, IVersusData
    {
        [SerializeField]
        GameObject _VersusManagerPrefab;
        [SerializeField]
        GameObject _VersusMap;
        
        public IParticipantData player1Data { get; set; }
        public IParticipantData player2Data { get; set; }
        public IMapData mapData { get; set; }
        public IVersusSettings settings { get; set; }
        public GameObject versusManagerPrefab => _VersusManagerPrefab;

        public GameObject versusMap => _VersusMap;
    }
}