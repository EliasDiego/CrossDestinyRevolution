using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VersusSystem
{
    public struct VersusSettings : IVersusSettings
    {
        int _Rounds;
        int _RoundTime;

        public int rounds => _Rounds;
        public int roundTime => _RoundTime;

        public VersusSettings(int rounds, int roundTime)
        {
            _Rounds = rounds;
            _RoundTime = roundTime;
        }
    }
}