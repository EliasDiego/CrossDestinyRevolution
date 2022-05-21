using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MechSystem;

namespace CDR.TargetingSystem
{
    [System.Serializable]
    public class TargetData : ITargetData
    {  
        public TargetData(ActiveCharacter m_activeCharacter, float m_dist, Vector3 m_dir)
        {
            _activeCharacter = m_activeCharacter;
            _dist = m_dist;
            _dir = m_dir;
        }
        
        ActiveCharacter _activeCharacter;
        float _dist;
        Vector3 _dir;

        public ActiveCharacter activeCharacter => _activeCharacter;

        public float distance => _dist;

        public Vector3 direction => _dir;
    }
}