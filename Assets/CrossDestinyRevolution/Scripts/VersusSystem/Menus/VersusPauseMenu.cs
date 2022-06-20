using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.UISystem;
using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class VersusPauseMenu : PauseMenu, IVersusPauseMenu
    {
        [SerializeField]
        InputActionReference _PauseAction;

        public event Action returnToMainMenuEvent;

        private void OnPause(InputAction.CallbackContext context)
        {
            Debug.Log("Blah");
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
    }
}