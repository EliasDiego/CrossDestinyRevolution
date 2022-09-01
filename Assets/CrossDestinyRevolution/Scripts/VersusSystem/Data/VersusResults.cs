using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VersusSystem
{
    public struct VersusResults : IVersusResults
    {
        public string winnerText { get; private set; }

        public IParticipant[] participants { get; }

        public VersusResults(IParticipant[] participants)
        {
            int maxScore = participants.Max(p => p.score);

            winnerText = participants.Where(p => p.score == maxScore).Count() > 1 ? "Draw" : participants.OrderByDescending(p => p.score)?.FirstOrDefault()?.name;

            this.participants = participants;
        }
    }
}