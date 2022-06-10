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

        private IVersusUI _VersusUI;

        private IParticipant[] _Participants;

        private Coroutine _RoundCoroutine;
        private Coroutine _EndRoundCoroutine;

        public int currentRound => _CurrentRound;
        public bool isRoundOnPlay => _IsRoundOnPlay;

        private IEnumerator RoundSequence(float roundTime)
        {
            Debug.Log("Round Start!");
            
            _VersusUI.roundUIHandler.Show();

            yield return new WaitForSeconds(3);
            _VersusUI.roundUIHandler.Hide();

            foreach(IParticipant p in _Participants)
                p.Start();

            _VersusUI.roundTimeUIHandler.Show();

            while(roundTime > 0)
            {
                roundTime -= Time.deltaTime;
                roundTime = Mathf.Max(roundTime, 0);

                _VersusUI.roundTimeUIHandler.roundTimeText = Mathf.RoundToInt(roundTime).ToString();

                Debug.Log(roundTime);

                yield return null;
            }
            
            _VersusUI.roundTimeUIHandler.Hide();

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

        public void Initialize(IVersusSettings versusSettings, IVersusUI versusUI, params IParticipant[] participants)
        {
            _VersusSettings = versusSettings;

            _Participants = participants;

            _VersusUI = versusUI;

            _VersusUI.Hide();

            StartRound();
        }

        public void StartRound()
        {
            if(_RoundCoroutine != null)
                StopCoroutine(_RoundCoroutine);

                
            foreach(IParticipant p in _Participants)
                p.mech.health.OnDeath += EndRound;


            _IsRoundOnPlay = true;

            _RoundCoroutine = StartCoroutine(RoundSequence(_VersusSettings.roundTime));
        }

        public void EndRound()
        {
            if (_RoundCoroutine != null)
                StopCoroutine(_RoundCoroutine);

            if (_EndRoundCoroutine != null)
                StopCoroutine(_EndRoundCoroutine);

            foreach(IParticipant p in _Participants)
                p.mech.health.OnDeath -= EndRound;

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