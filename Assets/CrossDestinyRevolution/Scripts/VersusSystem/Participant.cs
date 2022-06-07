using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;

namespace CDR.VersusSystem
{
    public struct Participant : IParticipant
    {
        IMech _Mech;

        public int score { get; set; }

        public Participant(IMech mech)
        {
            _Mech = mech;

            score = 0;
        }

        public void Reset()
        {
            _Mech.health.ModifyValue(_Mech.health.MaxValue);
            _Mech.boost.boostValue.ModifyValue(_Mech.boost.boostValue.MaxValue);
        }
    }
}