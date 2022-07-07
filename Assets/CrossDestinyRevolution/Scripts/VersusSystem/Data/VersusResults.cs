using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VersusSystem
{
    public struct VersusResults : IVersusResults
    {
        public IParticipant winner { get; }

        public IParticipant[] participants { get; }

        public VersusResults(IParticipant winner, IParticipant[] participants)
        {
            this.winner = winner;
            this.participants = participants;
        }
    }
}