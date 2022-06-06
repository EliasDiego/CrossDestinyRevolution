using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

namespace CDR.UISystem
{
    public class TitleScreenMenu : Menu
    {
        [SerializeField]
        InputActionAsset _Asset;
        [SerializeField]
        MainMenu _MainMenu;
        InputActionMap _ActionMap;

        private void Awake() 
        {
            _ActionMap = _Asset.FindActionMap("UI", true);
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            SwitchTo(_MainMenu);
        }

        public override void Show()
        {
            base.Show();

            _ActionMap.Enable();

            _ActionMap.actionTriggered += OnActionTriggered;
        }

        public override void Hide()
        {
            base.Hide();

            _ActionMap.Disable();

            _ActionMap.actionTriggered -= OnActionTriggered;
        }
    }
}