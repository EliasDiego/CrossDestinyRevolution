using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VersusSystem
{
    [CreateAssetMenu(menuName = "VersusSystem/Map Data")]
    public class MapData : ScriptableObject, IMapData
    {
        [SerializeField]
        string _MapName;
        [SerializeField]
        GameObject _MapPrefab;

        public string mapName => _MapName;
        public GameObject mapPrefab => _MapPrefab;
    }
}