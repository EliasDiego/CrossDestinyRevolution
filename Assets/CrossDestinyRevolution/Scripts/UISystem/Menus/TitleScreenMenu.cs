using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

namespace CDR.UISystem
{
    public class TitleScreenMenu : Menu
    {
        [SerializeField]
        InputActionReference _StartActionReference;
        [SerializeField]
        MainMenu _MainMenu;

        private void OnStart(InputAction.CallbackContext context)
        {
            SwitchTo(_MainMenu);
        }

        public override void Show()
        {
            base.Show();

            _StartActionReference.action.Enable();

            _StartActionReference.action.started += OnStart;
        }

        public override void Hide()
        {
            base.Hide();

            _StartActionReference.action.Disable();

            _StartActionReference.action.started -= OnStart;
        }
    }
}