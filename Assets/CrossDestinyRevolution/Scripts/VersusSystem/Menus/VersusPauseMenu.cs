using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

using CDR.UISystem;
using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class VersusPauseMenu : PauseMenu, IVersusPauseMenu
    {
        [SerializeField]
        InputActionReference _PauseAction;
        [SerializeField]
        EventSystem _EventSystem;
        [SerializeField]
        GameObject _FirstSelect;
        
        public IPlayerMechBattleUI[] battleUIs { get; set; }
        public IVersusUI versusUI { get; set; }
        public AudioSource musicAudioSource { get; set; }

        public event Action returnToMainMenuEvent;

        public void OnToggleMusic(bool value)
        {
            Debug.Log("Music " + value + " " + musicAudioSource.clip);

            if(value)
                musicAudioSource.Play();

            else
                musicAudioSource.Stop();
        }

        public void OnToggleBattleUI(bool value)
        {
            Debug.Log("BattleUI " + value + " " + battleUIs?.Length);

            if(value)
            {
                foreach(BattleUI battleUI in battleUIs)
                    battleUI.Show();

                versusUI.roundTimeUIHandler.Show();
            }

            else
            {
                foreach(BattleUI battleUI in battleUIs)
                    battleUI.Hide();

                versusUI.roundTimeUIHandler.Hide();
            }
        }

        private void OnPause(InputAction.CallbackContext context)
        {
            if(isPaused)
                Deactivate();

            else
                Activate();
        }

        public void EnablePauseInput()
        {
            _PauseAction.action.Enable();
            _PauseAction.action.started += OnPause;
        }

        public void DisablePauseInput()
        {
            _PauseAction.action.Disable();
            _PauseAction.action.started -= OnPause;
        }

        public void ReturnToMainMenu()
        {
            returnToMainMenuEvent?.Invoke();
        }

        public override void Show()
        {
            base.Show();

            EnablePauseInput();

            _EventSystem.SetSelectedGameObject(_FirstSelect);
        }

        public override void Hide()
        {
            base.Hide();

            if(isPaused)
                DisablePauseInput();
        }
    }
}