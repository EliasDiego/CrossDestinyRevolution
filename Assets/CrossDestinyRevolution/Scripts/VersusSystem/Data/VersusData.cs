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

        private List<IParticipantData> _ParticipantDatas = new List<IParticipantData>();
        
        public IMapData mapData { get; set; }
        public IVersusSettings settings { get; set; }
        public GameObject versusManagerPrefab => _VersusManagerPrefab;
        public GameObject versusMapPrefab => _VersusMap;
        public GameObject versusUIPrefab => _VersusUI;

        public IParticipantData[] participantDatas => throw new System.NotImplementedException();

        public void AddParticipantData(IParticipantData participantData)
        {
            if(!_ParticipantDatas.Contains(participantData))
                _ParticipantDatas.Add(participantData);
        }

        public void RemoveParticipantData(IParticipantData participantData)
        {
            if(_ParticipantDatas.Contains(participantData))
                _ParticipantDatas.Remove(participantData);
        }
    }
}