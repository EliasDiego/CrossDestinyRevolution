using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.InputSystem;
using CDR.UISystem;

namespace CDR.VersusSystem
{
    public class MapSelectMenu : VersusMenu, IMapSelectMenu
    {
        [SerializeField]
        InputActionReference _CancelAction;
        [SerializeField]
        VersusSettingsMenu _VersusSettingsMenu;

        private void OnCancel(InputAction.CallbackContext context)
        {
            Back();
        }

        public void PickMap(IMapData mapData)
        {
            versusData.mapData = mapData;

            SwitchTo(_VersusSettingsMenu);
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
    }
}