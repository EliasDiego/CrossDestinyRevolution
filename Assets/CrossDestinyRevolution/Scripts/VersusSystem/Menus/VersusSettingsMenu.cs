using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.SceneManagementSystem;

namespace CDR.VersusSystem
{
    public class VersusSettingsMenu : VersusMenu, IVersusSettingsMenu
    {
        [SerializeField]
        SceneLoader _SceneLoader;
        [SerializeField]
        InputActionReference _CancelAction;

        private void OnCancel(InputAction.CallbackContext context)
        {
            Back();
        }
        
        public override void Show()
        {
            base.Show();

            _CancelAction.action.Enable();
            _CancelAction.action.started += OnCancel;
        }

        public override void Hide()
        {
            _CancelAction.action.Disable();
            _CancelAction.action.started -= OnCancel;

            base.Hide();
        }
        
        public void SetSettings()
        {
            versusData.settings = new VersusSettings(5, 20);

            _SceneLoader.LoadSceneAsync(new VersusSceneTask(versusData));
        }
    }
}