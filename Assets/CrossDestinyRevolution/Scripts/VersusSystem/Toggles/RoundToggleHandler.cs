using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VersusSystem
{
    public class RoundToggleHandler : ToggleHandler
    {
        [SerializeField]
        VersusSettingsMenu _SettingsMenu;
        [SerializeField]
        private int _Round;

        public int round => _Round;

        protected override void Awake()
        {
            base.Awake();

            toggle.onValueChanged.AddListener(OnValueChange);
        }

        private void OnDestroy() 
        {
            toggle.onValueChanged.RemoveListener(OnValueChange);
        }

        private void OnValueChange(bool value)
        {
            _SettingsMenu.SetRounds(this);
        }
    }
}