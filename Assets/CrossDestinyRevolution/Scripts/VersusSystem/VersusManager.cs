using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class VersusManager : MonoBehaviour
    {
        private IVersusSettings _VersusSettings;

        private bool _IsRoundOnPlay;

        private int _CurrentRound;

        private IParticipant[] _Participants;

        private Coroutine _RoundCoroutine;
        private Coroutine _EndRoundCoroutine;

        public int currentRound => _CurrentRound;

        public bool isRoundOnPlay => _IsRoundOnPlay;

        private IEnumerator RoundSequence(float roundTime)
        {
            float currentTime = roundTime;

            foreach(IParticipant p in _Participants)
                p.Start();

            while(currentRound > 0)
            {
                currentTime -= Time.deltaTime;
                currentTime = Mathf.Max(currentTime, 0);

                yield return null;
            }

            // EndRound();
        }

        private IEnumerator EndRoundSequence()
        {
            yield return null;

            foreach(IParticipant p in _Participants)
                p.Reset();
            
            if(currentRound >= _VersusSettings.rounds)
                ShowResults();
        }

        public void Initialize(IVersusSettings versusSettings, params IParticipant[] participants)
        {
            _VersusSettings = versusSettings;

            _Participants = participants;

            StartRound();
        }

        public void StartRound()
        {
            if(_RoundCoroutine != null)
                StopCoroutine(_RoundCoroutine);

            _IsRoundOnPlay = true;

            _RoundCoroutine = StartCoroutine(RoundSequence(_VersusSettings.roundTime));
        }

        public void EndRound()
        {
            if(_EndRoundCoroutine != null)
                StopCoroutine(_EndRoundCoroutine);

            _CurrentRound++;

            _IsRoundOnPlay = false;

            _EndRoundCoroutine = StartCoroutine(EndRoundSequence());
        }

        public void ShowResults()
        {
            StopVersus();
        }

        public void StopVersus()
        {
            
        }
    }
}