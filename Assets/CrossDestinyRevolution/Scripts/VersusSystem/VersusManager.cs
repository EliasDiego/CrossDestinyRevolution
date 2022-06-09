using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.InputSystem;
using CDR.SceneManagementSystem;

namespace CDR.VersusSystem
{
    public class VersusManager : MonoBehaviour
    {
        [SerializeField]
        SceneLoader _SceneLoader;
        [SerializeField]
        AfterVersusSceneTask _AfterVersusSceneTask;

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
            Debug.Log("Round Start!");

            yield return new WaitForSeconds(3);

            foreach(IParticipant p in _Participants)
                p.Start();

            while(roundTime > 0)
            {
                roundTime -= Time.deltaTime;
                roundTime = Mathf.Max(roundTime, 0);

                Debug.Log(roundTime);

                yield return null;
            }

            EndRound();
        }

        private IEnumerator EndRoundSequence()
        {
            yield return null;

            foreach(IParticipant p in _Participants)
                p.Reset();

            yield return new WaitForSeconds(3);
            
            if(currentRound >= _VersusSettings.rounds)
                ShowResults();

            else
                StartRound();
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

            Debug.Log("End Round");

            _CurrentRound++;

            _IsRoundOnPlay = false;

            _EndRoundCoroutine = StartCoroutine(EndRoundSequence());
        }

        public void ShowResults()
        {
            Debug.Log("Show Results!");

            StopVersus();
        }

        public void StopVersus()
        {
            Debug.Log("Stop Versus");

            _SceneLoader.LoadSceneAsync(_AfterVersusSceneTask);
        }
    }
}