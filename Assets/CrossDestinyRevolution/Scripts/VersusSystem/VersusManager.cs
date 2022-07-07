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

        private IEnumerator TempDelayStart()
        {
            yield return new WaitForSeconds(5);
            
            StartRound();
        }

        private IEnumerator RoundSequence(float roundTime)
        {
            _VersusUI.roundUIHandler.Show();

            yield return new WaitForSeconds(3);

            _VersusUI.pauseMenu.returnToMainMenuEvent += ExitVersus;
            _VersusUI.pauseMenu.EnablePauseInput();

            Debug.Log("Round Start!");

            _VersusUI.roundUIHandler.Hide();

            foreach(IParticipant p in _Participants)
                p.Start();

            _VersusUI.roundTimeUIHandler.Show();

            while(roundTime > 0)
            {
                roundTime -= Time.deltaTime;
                roundTime = Mathf.Max(roundTime, 0);

                _VersusUI.roundTimeUIHandler.roundTimeText = Mathf.RoundToInt(roundTime).ToString();

                yield return null;
            }
            
            _VersusUI.roundTimeUIHandler.Hide();

            EndRound();
        }

        private IEnumerator EndRoundSequence()
        {
            foreach(IParticipant p in _Participants)
                p.Stop();

            yield return new WaitForSeconds(3);
            
            if(currentRound >= _VersusSettings.rounds)
                ShowResults();

            else
            {
                foreach(IParticipant p in _Participants)
                    p.Reset();

                StartRound();
            }
        }

        private void OnParticipantDeath()
        {
            int aliveParticipants = _Participants.Count(p => p.mech.health.CurrentValue > 0);

            if(aliveParticipants <= 1)
            {
                IParticipant aliveParticipant = _Participants.FirstOrDefault(p => p.mech.health.CurrentValue > 0);

                Debug.Log("Alive! " + aliveParticipant.name);

                aliveParticipant.score += 1;

                EndRound();
            }
        }

        public void Initialize(IVersusSettings versusSettings, IVersusUI versusUI, params IParticipant[] participants)
        {
            _VersusSettings = versusSettings;

            _Participants = participants;

            _VersusUI = versusUI;

            _VersusUI.roundTimeUIHandler.Hide();
            _VersusUI.roundUIHandler.Hide();

            StartCoroutine(TempDelayStart());
        }

        public void StartRound()
        {
            if(_RoundCoroutine != null)
                StopCoroutine(_RoundCoroutine);

            foreach(IParticipant p in _Participants)
                p.mech.health.OnDeath += OnParticipantDeath;

            _IsRoundOnPlay = true;

            _RoundCoroutine = StartCoroutine(RoundSequence(_VersusSettings.roundTime));
        }

        public void EndRound()
        {
            if(_RoundCoroutine != null)
                StopCoroutine(_RoundCoroutine);

            if(_EndRoundCoroutine != null)
                StopCoroutine(_EndRoundCoroutine);

            foreach(IParticipant p in _Participants)
                p.mech.health.OnDeath -= OnParticipantDeath;

            _VersusUI.roundTimeUIHandler.Hide();
            _VersusUI.pauseMenu.returnToMainMenuEvent -= ExitVersus;
            _VersusUI.pauseMenu.DisablePauseInput();

            Debug.Log("End Round");

            _CurrentRound++;

            _IsRoundOnPlay = false;

            _EndRoundCoroutine = StartCoroutine(EndRoundSequence());
        }

        public void ShowResults()
        {
            _VersusUI.resultsMenu.rematchEvent += Rematch;
            _VersusUI.resultsMenu.returnToMainMenuEvent += ExitVersus;
            _VersusUI.resultsMenu.results = new VersusResults(_Participants.OrderByDescending(p => p.score).FirstOrDefault(), _Participants);
            _VersusUI.resultsMenu.Show();

            // ExitVersus();
        }

        public void Rematch()
        {
            _VersusUI.resultsMenu.Hide();
            _VersusUI.resultsMenu.rematchEvent -= Rematch;
            _VersusUI.resultsMenu.returnToMainMenuEvent -= ExitVersus;
            
            foreach(IParticipant p in _Participants)
            {
                p.score = 0;
                p.Reset();
            }

            _CurrentRound = 0;

            StartRound();
        }

        public void ExitVersus()
        {
            Debug.Log("Exit Versus");

            _SceneLoader.LoadSceneAsync(_AfterVersusSceneTask);
        }
    }
}