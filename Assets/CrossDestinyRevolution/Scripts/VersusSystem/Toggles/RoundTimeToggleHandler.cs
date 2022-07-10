using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VersusSystem
{
    public class RoundTimeToggleHandler : ToggleHandler
    {
        [SerializeField]
        VersusSettingsMenu _SettingsMenu;
        [SerializeField]
        private int _RoundTime;

        public int roundTime => _RoundTime;

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
            _SettingsMenu.SetRoundTime(this);
        }
    }
}