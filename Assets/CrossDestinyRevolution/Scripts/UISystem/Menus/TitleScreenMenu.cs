using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace CDR.UISystem
{
    public class TitleScreenMenu : Menu
    {
        [SerializeField]
        GameObject _Environment;
        [SerializeField]
        InputActionReference _StartActionReference;
        [SerializeField]
        EventSystem _EventSystem;
        [SerializeField]
        MainMenu _MainMenu;

        private void Awake()
        {
            _EventSystem.gameObject.SetActive(false);
        }

        private void OnStart(InputAction.CallbackContext context)
        {
            SwitchTo(_MainMenu);
        }

        public override void Show()
        {
            base.Show();

            _StartActionReference.action.Enable();

            _StartActionReference.action.started += OnStart;

            _Environment.gameObject.SetActive(true);
        }

        public override void Hide()
        {
            base.Hide();

            _StartActionReference.action.Disable();

            _StartActionReference.action.started -= OnStart;

            _Environment.gameObject.SetActive(false);

            _EventSystem.gameObject.SetActive(true);
        }
    }
}