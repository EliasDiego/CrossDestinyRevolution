using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using CDR.InputSystem;
using CDR.SceneManagementSystem;

namespace CDR.VersusSystem
{
    public class VersusSettingsMenu : VersusMenu, IVersusSettingsMenu, IMenuCancelHandler
    {
        [SerializeField]
        GameObject _FirstSelected;
        [SerializeField]
        EventSystem _EventSystem;
        [SerializeField]
        MechSelectMenu _MechSelectMenu;
        [SerializeField]
        RoundToggleHandler _FirstRoundToggle;
        [SerializeField]
        RoundTimeToggleHandler _FirstRoundTimeToggle;
        
        RoundToggleHandler _CurrentRoundToggle;
        RoundTimeToggleHandler _CurrentRoundTimeToggle;

        public void OnCancel()
        {
            if(previousMenu != null)
                Back();
        }

        protected override IEnumerator HideAnimatedSequence()
        {
            yield return base.HideAnimatedSequence();

            _CurrentRoundToggle.toggle.SetIsOnWithoutNotify(false);
            _CurrentRoundTimeToggle.toggle.SetIsOnWithoutNotify(false);
        }

        public void SetRounds(RoundToggleHandler roundToggle)
        {
            if(_CurrentRoundToggle == roundToggle)
            {
                roundToggle.toggle.SetIsOnWithoutNotify(true);

                return;
            }

            if(_CurrentRoundToggle)
            {
                _CurrentRoundToggle.toggle.SetIsOnWithoutNotify(false);
            }

            _CurrentRoundToggle = roundToggle;
        }

        public void SetRoundTime(RoundTimeToggleHandler roundTimeToggle)
        {
            if(_CurrentRoundTimeToggle == roundTimeToggle)
            {
                roundTimeToggle.toggle.SetIsOnWithoutNotify(true);

                return;
            }

            if(_CurrentRoundTimeToggle)
            {
                _CurrentRoundTimeToggle.toggle.SetIsOnWithoutNotify(false);
            }
            
            _CurrentRoundTimeToggle = roundTimeToggle;
        }
        
        public void SetSettings()
        {
            versusData.settings = new VersusSettings(_CurrentRoundToggle.round, _CurrentRoundTimeToggle.roundTime);

            SwitchTo(_MechSelectMenu);
        }

        public override void Show()
        {
            base.Show();

            _EventSystem?.SetSelectedGameObject(_FirstSelected.gameObject);
            _FirstRoundToggle.toggle.isOn = true;
            _FirstRoundTimeToggle.toggle.isOn = true;
        }
    }
}