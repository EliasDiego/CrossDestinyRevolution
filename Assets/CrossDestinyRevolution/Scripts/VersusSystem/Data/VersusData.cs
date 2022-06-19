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
        [SerializeField]
        GameObject _VersusUI;
        [SerializeField]
        GameObject _CameraManager;

        public List<IParticipantData> participantDataList { get; set; } = new List<IParticipantData>();
        
        public IMapData mapData { get; set; }
        public IVersusSettings settings { get; set; }
        public GameObject versusManagerPrefab => _VersusManagerPrefab;
        public GameObject versusMapPrefab => _VersusMap;
        public GameObject versusUIPrefab => _VersusUI;

        public IParticipantData[] participantDatas => participantDataList.ToArray();

        public GameObject cameraManagerPrefab => _CameraManager;
    }
}